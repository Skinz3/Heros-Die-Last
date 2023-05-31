using LiteNetLib.Utils;
using Rogue.Core.Network.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Protocol.Messages.Server
{
    public class DebugClearHighlightCellsMessage : Message
    {
        public const ushort Id = 54;

        public override ushort MessageId
        {
            get
            {
                return Id;
            }
        }

        public DebugClearHighlightCellsMessage()
        {

        }

        public override void Deserialize(LittleEndianReader reader)
        {

        }
        public override void Serialize(LittleEndianWriter writer)
        {

        }
    }
}
