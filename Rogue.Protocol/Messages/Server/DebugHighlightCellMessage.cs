using LiteNetLib.Utils;
using Microsoft.Xna.Framework;
using Rogue.Core.Network.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Protocol.Messages.Server
{
    public class DebugHighlightCellMessage : Message
    {
        public const ushort Id = 53;

        public override ushort MessageId
        {
            get
            {
                return Id;
            }
        }

        public int cellId;

        public Color color;

        public DebugHighlightCellMessage()
        {

        }
        public DebugHighlightCellMessage(int cellId,Color color)
        {
            this.cellId = cellId;
            this.color = color;
        }

        public override void Deserialize(LittleEndianReader reader)
        {
            this.cellId = reader.GetInt();
            this.color = reader.GetColor();
        }
        public override void Serialize(LittleEndianWriter writer)
        {
            writer.Put(cellId);
            writer.Put(color);
        }
    }
}
