using LiteNetLib.Utils;
using Rogue.Core.Network.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Protocol.Messages.Server
{
    public class DefinePlayerWeaponMessage : Message
    {
        public const ushort Id = 50;

        public override ushort MessageId => Id;


        public int targetId;

        public string animationName;


        public DefinePlayerWeaponMessage()
        {

        }

        public DefinePlayerWeaponMessage(int targetId, string animationName)
        {
            this.targetId = targetId;
            this.animationName = animationName;
        }

        public override void Deserialize(LittleEndianReader reader)
        {
            this.targetId = reader.GetInt();
            this.animationName = reader.GetString();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.Put(targetId);
            writer.Put(animationName);
        }
    }
}
