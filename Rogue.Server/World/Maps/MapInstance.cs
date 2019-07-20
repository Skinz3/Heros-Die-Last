using LiteNetLib;
using Rogue.Protocol.Messages.Server;
using Rogue.Protocol.Types;
using Rogue.Server.Records;
using Rogue.Server.World.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rogue.Core.Utils;
using Rogue.Core.Network.Protocol;
using Rogue.Core.Time;
using System.Diagnostics;
using System.Collections.Concurrent;
using Microsoft.Xna.Framework;
using Rogue.Server.World.Maps.Triggers;
using Rogue.Core.Collisions;
using Rogue.Server.World.Entities.Projectiles;
using Rogue.Core.Objects;

namespace Rogue.Server.World.Maps
{
    public class MapInstance : IDisposable
    {
        /// <summary>
        /// Theorically 60fps
        /// Aprox equal to (16.666)
        /// </summary>
        public const float REFRESH_RATE = (1000f / 60f);

        protected Logger logger = new Logger();

        public event Action<Entity> OnEntityJoin;
        public event Action<Entity> OnEntityLeave;

        protected Dictionary<int, Entity> Entities = new Dictionary<int, Entity>();
        protected Dictionary<int, Projectile> Projectiles = new Dictionary<int, Projectile>();

        private ReversedUniqueIdProvider uniqueIdProvider = new ReversedUniqueIdProvider(0);
        private ReversedUniqueIdProvider uniqueIdProviderProjectile = new ReversedUniqueIdProvider(0);

        public int Id
        {
            get;
            private set;
        }
        public int EntitiesCount
        {
            get
            {
                return Entities.Count;
            }
        }
        public int PlayersCount
        {
            get
            {
                return Entities.Values.OfType<Player>().Count();
            }
        }
        private bool WaitForDispose
        {
            get;
            set;
        }
        public MapRecord Record
        {
            get;
            private set;
        }

        private HighResolutionTimer Timer
        {
            get;
            set;
        }
        private Stopwatch Stopwatch
        {
            get;
            set;
        }
        /// <summary>
        /// ConcurrentStack<T> is a threadsafe object.
        /// </summary>
        private ConcurrentStack<Action> SynchronizedActions
        {
            get;
            set;
        }
        private List<MapInteractiveRecord> MapObjects
        {
            get;
            set;
        }
        public MapInstance(int id, MapRecord record)
        {
            this.Id = id;
            this.Record = record;
            this.LoadMapObjects();
            this.Timer = new HighResolutionTimer((int)REFRESH_RATE); // cast ? :3
            this.SynchronizedActions = new ConcurrentStack<Action>();
            StartCallback();
        }
        private void LoadMapObjects()
        {
            this.MapObjects = Record.MapObjects.ToList();

            foreach (var mapObject in MapObjects)
            {
                mapObject.Trigger = TriggerManager.GetTrigger(this, mapObject);

            }
        }
        public void AddMapElement(int cellId, LayerEnum layer, bool walkable, string visualData, MapObjectType type)
        {
            var cell = Record.Grid.GetCell(cellId);
            Send(new AddLayerElementMessage(new ProtocolLayerElement(cellId, layer, type, visualData, walkable)));
        }
        public void RemoveMapElement(int cellId, LayerEnum layer, bool isCellWalkable)
        {
            var cell = Record.Grid.GetCell(cellId);
            cell.Walkable = isCellWalkable;
            Send(new RemoveLayerElementMessage(cell.Id, layer, isCellWalkable));
        }
        public void UpdateElementVisual(int cellId, LayerEnum layer, string visualData, MapObjectType type, bool walkable)
        {
            Send(new AddLayerElementMessage(new ProtocolLayerElement(cellId, layer, type, visualData, walkable)));
        }
        /// <summary>
        /// Care about layer!
        /// </summary>
        public MapInteractiveRecord GetFirstMapObject(int cellId)
        {
            return MapObjects.FirstOrDefault(x => x.CellId == cellId);
        }
        public MapInteractiveRecord GetMapObject(int id)
        {
            return MapObjects.FirstOrDefault(x => x.Id == id);
        }

        public void StartCallback()
        {
            Stopwatch = Stopwatch.StartNew();
            Timer.Elapsed += CallBack;
            Timer.Start();
        }
        public void Invoke(Action action)
        {
            SynchronizedActions.Push(action);
        }
        private void CallBack()
        {
            long deltaTime = Stopwatch.ElapsedMilliseconds;

            if (deltaTime > 0)
            {
                Console.Title = "Rogue Server FPS :" + (int)(deltaTime * (60 / REFRESH_RATE));

                Update(deltaTime);
                Stopwatch = Stopwatch.StartNew();
            }
        }
        public IEnumerable<T> Raycast<T>(Ray2D ray) where T : Entity
        {
            foreach (var entity in GetEntities<T>())
            {
                if (ray.Intersects(entity.GetHitBox()))
                    yield return entity;
            }
        }

        public void Dispose()
        {
            WaitForDispose = true;
        }
        private void Update(long deltaTime)
        {
            if (WaitForDispose)
            {
                Timer.Stop();
                Timer = null;
                Entities = null;
                SynchronizedActions = null;
                return;
            }


            foreach (var action in SynchronizedActions)
            {
                action();
            }
            SynchronizedActions?.Clear();

            foreach (var mapObject in MapObjects)
            {
                mapObject.Trigger?.Update(deltaTime);
            }
            foreach (var entity in Entities.ToArray())
            {
                entity.Value.Update(deltaTime);

                if (entity.Value.WaitingForDispose)
                {
                    RemoveEntity(entity.Value);
                }
            }


        }
        public T[] GetEntities<T>() where T : Entity
        {
            return Entities.Values.OfType<T>().ToArray();
        }
        public int GetNextEntityId()
        {
            return uniqueIdProvider.GetNextId();
        }
        public bool Contains(Entity entity)
        {
            return Entities.ContainsKey(entity.Id);
        }

        public void Send(Message message, SendOptions sendOptions = SendOptions.ReliableOrdered)
        {
            foreach (var player in Entities.Values.OfType<Player>().ToArray())
            {
                player.Client.Send(message, sendOptions);
            }
        }
        public void Send(Message message, int exceptId, SendOptions sendOptions)
        {
            foreach (var player in Entities.Values.OfType<Player>().ToArray())
            {
                if (player.Id != exceptId)
                    player.Client.Send(message, sendOptions);
            }
        }

        protected ProtocolEntity[] GetProtocolEntities()
        {
            return Array.ConvertAll(Entities.Values.ToArray(), x => x.GetProtocolObject());
        }
        public void RemoveEntity(Entity entity)
        {
            if (Entities.ContainsKey(entity.Id))
            {
                Send(new RemoveEntityMessage(entity.Id), SendOptions.ReliableOrdered);
                Entities.Remove(entity.Id);
                entity.Dispose();
                OnEntityLeave?.Invoke(entity);
            }
            else
            {
                logger.Write("Unable to remove unknown entity from map instance " + Id);
            }

            if (EntitiesCount == 0)
            {
                MapsManager.RemoveMapInstance(this);
            }
        }
        public GameEntitiesMessage GetGameEntitiesMessage()
        {
            return new GameEntitiesMessage(GetProtocolEntities(),
                Configuration.Self.PositionUpdateFrameCount, Configuration.Self.UseInterpolation);
        }
        /// <summary>
        /// add a entity? Entity.DefineMapInstance(x)
        /// </summary>
        /// <param name="entity"></param>
        public virtual void AddEntity(Entity entity)
        {
            if (!Entities.ContainsKey(entity.Id))
            {
                var protocolEntity = entity.GetProtocolObject();
                Send(new ShowEntityMessage(protocolEntity), SendOptions.ReliableOrdered);
                this.Entities.Add(entity.Id, entity);
                OnEntityJoin?.Invoke(entity);
            }
            else
            {
                logger.Write("Unable to add entity " + entity.Id + " to the scene, the entity already exist.", MessageState.WARNING);
            }
        }
    }
}
