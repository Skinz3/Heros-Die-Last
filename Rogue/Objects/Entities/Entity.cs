using Microsoft.Xna.Framework;
using Rogue.Core.Objects;
using Rogue.Core.Objects.Abstract;
using Rogue.Protocol.Types;
using Rogue.World.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Objects.Entities
{
    public abstract class Entity : ColorableObject
    {
        public int Id
        {
            get;
            set;
        }
        public string Name
        {
            get;
            private set;
        }
       
        public MapInstance MapInstance
        {
            get;
            private set;
        }
        public bool InMapInstance
        {
            get
            {
                return MapInstance != null;
            }
        }
        public Entity(ProtocolEntity protocolEntity) : base(protocolEntity.Position, protocolEntity.Size, Color.White)
        {
            this.Id = protocolEntity.EntityId;
            this.Name = protocolEntity.Name;
        }
        public override void Dispose()
        {
            MapInstance = null;
            base.Dispose();
        }
        public void DefineMapInstance(MapInstance instance)
        {
            this.MapInstance = instance;
        }

        public abstract GCell GetCell();

    }
}
