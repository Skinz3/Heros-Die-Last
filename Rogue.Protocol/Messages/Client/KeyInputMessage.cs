using LiteNetLib.Utils;
using Microsoft.Xna.Framework.Input;
using MonoFramework.Network.Protocol;
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

        public KeyInputMessage()
        {

        }
        public KeyInputMessage(Keys key)
        {
            this.key = key;
        }
        public override void Deserialize(LittleEndianReader reader) // permet de convertir les objets binaire en paramètres
        {
            this.key = (Keys)reader.GetInt();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.Put((int)key);
        }
    }
}
