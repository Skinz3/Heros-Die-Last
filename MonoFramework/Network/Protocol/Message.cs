using LiteNetLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFramework.Network.Protocol
{
    public abstract class Message
    {
        public abstract ushort MessageId
        {
            get;
        }
        public void Unpack(NetDataReader reader)
        {
            this.Deserialize(reader);
        }
        public void Pack(NetDataWriter writer)
        {
            writer.Put((short)MessageId);
            Serialize(writer);
        }
        public abstract void Serialize(NetDataWriter writer);
        public abstract void Deserialize(NetDataReader reader);

        public override string ToString()
        {
            return base.GetType().Name;
        }
    }
    public class MessageHandlerAttribute : Attribute
    {

    }
}
