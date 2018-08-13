using Microsoft.Xna.Framework;
using MonoFramework.Objects.Abstract;
using Rogue.Protocol.Types;
using Rogue.World.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFramework.Objects.Entities
{
    public abstract class Entity : ColorableObject
    {
        public int Id
        {
            get;
            set;
        }
        public MapInstance MapInstance
        {
            get;
            private set;
        }

        public Entity(ProtocolEntity protocolEntity) : base(protocolEntity.Position, protocolEntity.Size, Color.White)
        {
            this.Id = protocolEntity.EntityId;
        }

        public void DefineMapInstance(MapInstance instance)
        {
            this.MapInstance = instance;
        }

    }
}
