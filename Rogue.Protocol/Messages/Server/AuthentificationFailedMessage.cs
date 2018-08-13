﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiteNetLib.Utils;
using MonoFramework.Network.Protocol;
using Rogue.Protocol.Enums;

namespace Rogue.Protocol.Messages.Server
{
    public class AuthentificationFailedMessage : Message
    {
        public const ushort Id = 3;

        public override ushort MessageId
        {
            get
            {
                return Id;
            }
        }

        public AuthentificationFailureEnum reason;
        
        public AuthentificationFailedMessage()
        {

        }
        public AuthentificationFailedMessage(AuthentificationFailureEnum reason)
        {
            this.reason = reason;
        }
        public override void Deserialize(NetDataReader reader)
        {
            this.reason = (AuthentificationFailureEnum)reader.GetByte();
        }

        public override void Serialize(NetDataWriter writer)
        {
            writer.Put((byte)reason);
        }
    }
}
