using Rogue.Core.Collisions;
using Rogue.Core.IO.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Core.IO.Animations
{
    public class AnimationElementTemplate
    {
        public bool FlipHorizontal;

        public bool FlipVertical;

        public string[] SpriteNames;

        public bool Loop;

        public short Delay;

        public bool IsDefine => SpriteNames.Length > 0;

        public AnimationElementTemplate()
        {
            SpriteNames = new string[0];
        }

        public void Serialize(LittleEndianWriter writer)
        {
            writer.WriteBoolean(FlipHorizontal);
            writer.WriteBoolean(FlipVertical);
            writer.WriteBoolean(Loop);
            writer.WriteShort(Delay);


            writer.WriteInt(SpriteNames.Length);

            foreach (var spriteName in SpriteNames)
            {
                writer.WriteUTF(spriteName);
            }
        }
        public void Deserialize(LittleEndianReader reader)
        {
            this.FlipHorizontal = reader.ReadBoolean();
            this.FlipVertical = reader.ReadBoolean();
            this.Loop = reader.ReadBoolean();
            this.Delay = reader.ReadShort();

            SpriteNames = new string[reader.ReadInt()];

            for (int i = 0; i < SpriteNames.Length; i++)
            {
                SpriteNames[i] = reader.ReadUTF();
            }
        }
    }
    public class AnimationTemplate : Template
    {
        public string AnimationName;

        public Dictionary<DirectionEnum, AnimationElementTemplate> Elements;

        public AnimationTemplate()
        {
            Elements = new Dictionary<DirectionEnum, AnimationElementTemplate>();

        }

        public override void Deserialize(LittleEndianReader reader)
        {
            this.AnimationName = reader.ReadUTF();

            int count = reader.ReadInt();
            for (int i = 0; i < count; i++)
            {
                DirectionEnum direction = (DirectionEnum)reader.ReadByte();
                AnimationElementTemplate element = new AnimationElementTemplate();
                element.Deserialize(reader);
                Elements.Add(direction, element);
            }

        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.WriteUTF(AnimationName);

            writer.WriteInt(Elements.Count);

            foreach (var element in Elements)
            {
                writer.WriteByte((byte)element.Key);
                element.Value.Serialize(writer);
            }
        }
        public static AnimationTemplate New()
        {
            AnimationTemplate template = new AnimationTemplate();
            template.Elements.Add(DirectionEnum.None, new AnimationElementTemplate());
            template.Elements.Add(DirectionEnum.Down, new AnimationElementTemplate());
            template.Elements.Add(DirectionEnum.Up, new AnimationElementTemplate());
            template.Elements.Add(DirectionEnum.Left, new AnimationElementTemplate());
            template.Elements.Add(DirectionEnum.Right, new AnimationElementTemplate());
            return template;
        }
    }
}
