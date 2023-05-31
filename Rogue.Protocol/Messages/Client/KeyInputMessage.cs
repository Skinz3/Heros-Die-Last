using LiteNetLib.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Rogue.Core.Network.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Protocol.Messages.Client
{
    public class KeyInputMessage : Message
    {
        public const ushort Id = 47;

        public override ushort MessageId
        {
            get
            {
                return Id;// l'id du message (unique)
            }
        }

        public Keys key;

        public Point mousePosition;

        public Vector2 playerPosition;

        public KeyInputMessage()
        {

        }
        public KeyInputMessage(Keys key, Point mousePosition, Vector2 playerPosition)
        {
            this.key = key;
            this.mousePosition = mousePosition;
            this.playerPosition = playerPosition;
        }
        public override void Deserialize(LittleEndianReader reader) // permet de convertir les objets binaire en paramètres
        {
            this.key = (Keys)reader.GetInt();
            this.mousePosition = reader.GetPoint();
            this.playerPosition = reader.GetVector2();

        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.Put((int)key);
            writer.Put(mousePosition);
            writer.Put(playerPosition);
        }
    }
}
