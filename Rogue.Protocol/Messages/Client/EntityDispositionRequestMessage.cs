using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiteNetLib.Utils;
using Microsoft.Xna.Framework;
using Rogue.Core.Network.Protocol;
using Rogue.Protocol;
using Rogue.Core.Collisions;

namespace Rogue.Protocol.Messages.Client
{
    public class EntityDispositionRequestMessage : Message
    {
        public const ushort Id = 7;

        public override ushort MessageId
        {
            get
            {
                return Id;
            }
        }

        public Vector2 position;

        public DirectionEnum direction;

        public float mouseRotation;

        public EntityDispositionRequestMessage()
        {

        }
        public EntityDispositionRequestMessage(Vector2 position, DirectionEnum direction, float mouseRotation)
        {
            this.position = position;
            this.direction = direction;
            this.mouseRotation = mouseRotation;
        }
        public override void Deserialize(LittleEndianReader reader)
        {
            this.position = Extensions.GetVector2(reader);
            this.direction = (DirectionEnum)reader.GetByte();
            this.mouseRotation = reader.GetFloat();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.Put(position);
            writer.Put((byte)direction);
            writer.Put(mouseRotation);
        }
    }


}
