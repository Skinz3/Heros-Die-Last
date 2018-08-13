using LiteNetLib.Utils;
using MonoFramework.Collisions;
using MonoFramework.Network.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Protocol.Types
{
    public class DirectionalAnimation : MessageType
    {
        public static short Id = 9;
        public override short TypeIdProp => Id;

        public DirectionEnum Direction
        {
            get;
            set;
        }
        public string[] Sprites
        {
            get;
            set;
        }
        public float Delay
        {
            get;
            set;
        }
        public bool FlipVertically
        {
            get;
            set;
        }
        public bool FlipHorizontally
        {
            get;
            set;
        }
        public DirectionalAnimation()
        {

        }
        public DirectionalAnimation(DirectionEnum direction, string[] sprites, float delay, bool flipVertically = false, bool flipHorizontally = false)
        {
            this.Direction = direction;
            this.Sprites = sprites;
            this.Delay = delay;
            this.FlipVertically = flipVertically;
            this.FlipHorizontally = flipHorizontally;
        }
        public override void Serialize(NetDataWriter writer)
        {
            writer.Put((byte)Direction);

            writer.Put(Sprites.Length);

            foreach (var sprite in Sprites)
            {
                writer.Put(sprite);
            }

            writer.Put(Delay);

            writer.Put(FlipVertically);
            writer.Put(FlipHorizontally);
        }

        public override void Deserialize(NetDataReader reader)
        {
            this.Direction = (DirectionEnum)reader.GetByte();

            Sprites = new string[reader.GetInt()];

            for (int i = 0; i < Sprites.Length; i++)
            {
                Sprites[i] = reader.GetString();
            }

            this.Delay = reader.GetFloat();

            this.FlipVertically = reader.GetBool();
            this.FlipHorizontally = reader.GetBool();
        }
    }
}
