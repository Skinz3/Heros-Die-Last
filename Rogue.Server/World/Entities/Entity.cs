using Microsoft.Xna.Framework;
using Rogue.Protocol.Types;
using Rogue.Server.World.Entities.Scripts;
using Rogue.Server.World.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Server.World.Entities
{
    public abstract class Entity : ServerObject, IProtocolable<ProtocolEntity>
    {
        public abstract int Id
        {
            get;
        }
        public abstract string Name
        {
            get;
        }

        private List<IScript> Scripts
        {
            get;
            set;
        }
        public MapInstance MapInstance
        {
            get;
            protected set;
        }
        public bool WaitingForDispose
        {
            get;
            set;
        }

        public Entity(Vector2 position, Point size) : base(position, size)
        {
            this.Scripts = new List<IScript>();
        }
        public void AddScript(IScript script)
        {
            script.Initialize(this);
            this.Scripts.Add(script);
        }
        public void RemoveScript(IScript script)
        {
            script.OnRemove();
            this.Scripts.Remove(script);
        }
        public T GetScript<T>() where T : IScript
        {
            return Scripts.OfType<T>().FirstOrDefault();
        }
        public virtual void DefineMapInstance(MapInstance instance)
        {
            this.MapInstance = instance;
            this.MapInstance.AddEntity(this);
        }

        public void LeaveMapInstance()
        {
            if (MapInstance != null)
            {
                MapInstance.RemoveEntity(this);
                MapInstance = null;
            }
        }

        public abstract Rectangle GetHitBox();

        public abstract ProtocolEntity GetProtocolObject();

        public virtual void Update(long deltaTime)
        {
            foreach (var script in Scripts.ToArray())
            {
                script.Update(deltaTime);
            }

            OnUpdate(deltaTime);

        }

        public abstract void OnUpdate(long deltaTime);

        public virtual void Dispose()
        {
            MapInstance = null;

            foreach (var script in Scripts.ToArray())
            {
                script.Dispose();
            }
        }


    }
}
