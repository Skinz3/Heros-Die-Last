using MonoFramework.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFramework.IO.Maps
{
    public class MapTemplate
    {
        public int Width;

        public int Height;

        public CellTemplate[] Cells;

        public void Deserialize(LittleEndianReader reader)
        {
            this.Width = reader.ReadInt();
            this.Height = reader.ReadInt();

            this.Cells = new CellTemplate[reader.ReadInt()];

            for (int i = 0; i < Cells.Length; i++)
            {
                Cells[i] = new CellTemplate();
                Cells[i].Deserialize(reader);
            }
        }
        public void Serialize(LittleEndianWriter writer)
        {
            writer.WriteInt(Width);
            writer.WriteInt(Height);

            writer.WriteInt(Cells.Length);

            foreach (var cell in Cells)
            {
                cell.Serialize(writer);
            }
        }
    }
    public class CellTemplate
    {
        public int Id;

        public SpriteTemplate[] Sprites;

        public void Serialize(LittleEndianWriter writer)
        {
            writer.WriteInt(Id);

            writer.WriteInt(Sprites.Length);

            foreach (var sprite in Sprites)
            {
                sprite.Serialize(writer);
            }
        }
        public void Deserialize(LittleEndianReader reader)
        {
            Id = reader.ReadInt();

            Sprites = new SpriteTemplate[reader.ReadInt()];

            for (int i = 0; i < Sprites.Length; i++)
            {
                Sprites[i] = new SpriteTemplate();
                Sprites[i].Deserialize(reader);
            }
        }
    }
    public class SpriteTemplate
    {
        public string SpriteName;

        public LayerEnum Layer;

        public bool FlippedVertically;

        public bool FlippedHorizontally;


        public void Serialize(LittleEndianWriter writer)
        {
            writer.WriteUTF(SpriteName);
            writer.WriteByte((byte)Layer);
            writer.WriteBoolean(FlippedVertically);
            writer.WriteBoolean(FlippedHorizontally);
        }
        public void Deserialize(LittleEndianReader reader)
        {
            this.SpriteName = reader.ReadUTF();
            this.Layer = (LayerEnum)reader.ReadByte();
            this.FlippedVertically = reader.ReadBoolean();
            this.FlippedHorizontally = reader.ReadBoolean();
        }

    }
}
