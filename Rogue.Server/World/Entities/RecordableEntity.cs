using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Rogue.Protocol.Types;
using Rogue.Server.Records;

namespace Rogue.Server.World.Entities
{
    public abstract class RecordableEntity : MovableEntity
    {
        protected EntityRecord Record
        {
            get;
            private set;
        }

        public override string Name => Record.Name;

        public RecordableEntity(EntityRecord record, Vector2 position) : base(position, new Point(record.Width, record.Height), record.GetStats())
        {
            this.Record = record;
        }
    }
}
