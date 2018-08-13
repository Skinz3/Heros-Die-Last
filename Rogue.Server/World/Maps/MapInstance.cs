using LiteNetLib;
using MonoFramework.Network.Protocol;
using MonoFramework.Utils;
using Rogue.Protocol.Messages.Server;
using Rogue.Protocol.Types;
using Rogue.Server.Records;
using Rogue.Server.World.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Server.World.Maps
{
    public class MapInstance
    {
        protected Logger logger = new Logger();

        protected Dictionary<int, Entity> Entities = new Dictionary<int, Entity>();

        private ReversedUniqueIdProvider uniqueIdProvider = new ReversedUniqueIdProvider(0);

        public int Id
        {
            get;
            private set;
        }

        public int PlayersCount
        {
            get
            {
                return Entities.Values.OfType<Player>().Count();
            }
        }
        public MapRecord Record
        {
            get;
            private set;
        }


        public MapInstance(int id, MapRecord record)
        {
            this.Id = id;
            this.Record = record;

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
            }
            else
            {
                logger.Write("Unable to remove unknown entity from map instance " + Id);
            }

            if (PlayersCount == 0)
            {
                MapsManager.RemoveMapInstance(this);
            }
        }
        public GameEntitiesMessage GetGameEntitiesMessage()
        {
            return new GameEntitiesMessage(GetProtocolEntities(), Configuration.Self.PositionUpdateFrameCount, Configuration.Self.UseInterpolation);
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
            }
            else
            {
                logger.Write("Unable to add entity " + entity.Id + " to the scene, the entity already exist.", MessageState.WARNING);
            }
        }
    }
}
