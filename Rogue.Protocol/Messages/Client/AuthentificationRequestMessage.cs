using LiteNetLib.Utils;
using MonoFramework.Network.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;

namespace Rogue.Protocol.Messages.Client
{
    public class AuthentificationRequestMessage : Message // la classe hérité est message
    {
        public const ushort Id = 1;

        public override ushort MessageId
        {
            get
            {
                return Id;// l'id du message (unique)
            }
        }
        public string Username; // les deux paramètres
        public string Password;

        public AuthentificationRequestMessage()
        {

        }
        public AuthentificationRequestMessage(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }
        public override void Deserialize(NetDataReader reader) // permet de convertir les objets binaire en paramètres
        {
            this.Username = reader.GetString();
            this.Password = reader.GetString();
        }

        public override void Serialize(NetDataWriter writer)
        {
            writer.Put(Username);
            writer.Put(Password);
        }
       

    }
}
