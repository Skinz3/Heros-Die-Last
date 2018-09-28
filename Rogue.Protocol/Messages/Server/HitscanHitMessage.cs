using LiteNetLib.Utils;
using Microsoft.Xna.Framework;
using MonoFramework.Network.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Protocol.Messages.Server
{
    public class HitscanHitMessage : Message
    {
        public const ushort Id = 46;

        public override ushort MessageId
        {
            get
            {
                return Id;
            }
        }

        public int sourceId;

        public Vector2 targetPoint;

        public HitscanHitMessage()
        {

        }
        public HitscanHitMessage(int sourceId, Vector2 targetPoint)
        {
            this.sourceId = sourceId;
            this.targetPoint = targetPoint;
        }

        public override void Deserialize(LittleEndianReader reader)
        {
            this.sourceId = reader.GetInt();
            this.targetPoint = reader.GetVector2();
        }
        public override void Serialize(LittleEndianWriter writer)
        {
            writer.Put(sourceId);
            writer.Put(this.targetPoint);
        }
    }
}
