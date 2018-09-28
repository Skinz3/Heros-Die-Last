using LiteNetLib.Utils;
using Microsoft.Xna.Framework;
using MonoFramework.Collisions;
using MonoFramework.Network.Protocol;
using Rogue.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Protocol.Messages.Client
{
    public class ClickMessage : Message
    {
        public const ushort Id = 33;

        public override ushort MessageId
        {
            get
            {
                return Id;// l'id du message (unique)
            }
        }

        public Point position;

        public Vector2 playerPosition;

        public ClickTypeEnum clickType;

        public ClickMessage()
        {

        }
        public ClickMessage(Point position ,Vector2 playerPosition, ClickTypeEnum clickType)
        {
            this.position = position;
            this.playerPosition = playerPosition;
            this.clickType = clickType;
        }
        public override void Deserialize(LittleEndianReader reader) // permet de convertir les objets binaire en paramètres
        {
            this.position = Extensions.GetPoint(reader);
            this.playerPosition = Extensions.GetVector2(reader);
            this.clickType = (ClickTypeEnum)reader.GetByte();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.Put(position);
            writer.Put(playerPosition);
            writer.Put((byte)clickType);
        }

    }
}
