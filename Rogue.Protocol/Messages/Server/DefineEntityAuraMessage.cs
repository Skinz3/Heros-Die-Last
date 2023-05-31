using LiteNetLib.Utils;
using Microsoft.Xna.Framework;
using Rogue.Core.Network.Protocol;
using Rogue.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Protocol.Messages.Server
{
    public class DefineEntityAuraMessage : Message
    {
        public const ushort Id = 49;

        public override ushort MessageId
        {
            get
            {
                return Id;
            }
        }

        public int targetId;

        public ProtocolAura aura;

        public DefineEntityAuraMessage()
        {

        }
        public DefineEntityAuraMessage(int targetId, ProtocolAura aura)
        {
            this.targetId = targetId;
            this.aura = aura;
        }

        public override void Deserialize(LittleEndianReader reader)
        {
            this.targetId = reader.GetInt();
            this.aura = new ProtocolAura();
            this.aura.Deserialize(reader);
        }
        public override void Serialize(LittleEndianWriter writer)
        {
            writer.Put(targetId);
            aura.Serialize(writer);
        }
    }
}
