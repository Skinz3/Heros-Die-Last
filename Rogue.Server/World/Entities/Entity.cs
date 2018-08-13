using Microsoft.Xna.Framework;
using Rogue.Protocol.Types;
using Rogue.Server.World.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Server.World.Entities
{
    public abstract class Entity : IProtocolable<ProtocolEntity>
    {
        public abstract int Id
        {
            get;
        }
        public abstract string Name
        {
            get;
        }
        public Vector2 Position
        {
            get;
            protected set;
        }
        public float Rotation
        {
            get;
            protected set;
        }
        public MapInstance MapInstance
        {
            get;
            protected set;
        }
        public virtual void DefineMapInstance(MapInstance instance) // FightTeamEnum ?
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
        public abstract ProtocolEntity GetProtocolObject();
    }
}
