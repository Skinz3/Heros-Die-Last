using Rogue.Core.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Core.IO.Maps
{
    public class MapTemplate : Template
    {
        public const int MAP_CELL_SIZE = 50;

        public int Width;

        public int Height;

        public CellTemplate[] Cells;

        public float Zoom;

        public override void Deserialize(LittleEndianReader reader)
        {
            this.Width = reader.ReadInt();
            this.Height = reader.ReadInt();

            this.Cells = new CellTemplate[reader.ReadInt()];

            for (int i = 0; i < Cells.Length; i++)
            {
                Cells[i] = new CellTemplate();
                Cells[i].Deserialize(reader);
            }

            this.Zoom = reader.ReadFloat();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.WriteInt(Width);
            writer.WriteInt(Height);

            writer.WriteInt(Cells.Length);

            foreach (var cell in Cells)
            {
                cell.Serialize(writer);
            }

            writer.WriteFloat(Zoom);
        }
        public SpriteTemplate GetSpriteTemplate(int cellId, LayerEnum layer)
        {
            var cell = Cells.FirstOrDefault(x => x.Id == cellId);
            return cell.Sprites.FirstOrDefault(x => x.Layer == layer);
        }
        public CellTemplate GetCellTemplate(int id)
        {
            return Cells.FirstOrDefault(x => x.Id == id);
        }
    }
    public class CellTemplate
    {
        public int Id;

        public SpriteTemplate[] Sprites;

        public LightTemplate Light;

        public bool Walkable;

        public void Serialize(LittleEndianWriter writer)
        {
            writer.WriteInt(Id);

            writer.WriteInt(Sprites.Length);

            foreach (var sprite in Sprites)
            {
                sprite.Serialize(writer);
            }

            writer.WriteBoolean(Light != null);

            Light?.Serialize(writer);

            writer.WriteBoolean(Walkable);

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

            if (reader.ReadBoolean())
            {
                Light = new LightTemplate();
                Light.Deserialize(reader);
            }

            Walkable = reader.ReadBoolean();
        }
    }
    public class LightTemplate
    {
        public short Radius;

        public byte A;

        public byte R;

        public byte G;

        public byte B;

        public float Sharpness;

        public void Serialize(LittleEndianWriter writer)
        {
            writer.WriteShort(Radius);
            writer.WriteByte(A);
            writer.WriteByte(R);
            writer.WriteByte(G);
            writer.WriteByte(B);
            writer.WriteFloat(Sharpness);
        }
        public void Deserialize(LittleEndianReader reader)
        {
            this.Radius = reader.ReadShort();
            this.A = reader.ReadByte();
            this.R = reader.ReadByte();
            this.G = reader.ReadByte();
            this.B = reader.ReadByte();
            this.Sharpness = reader.ReadFloat();
        }
    }
    public class SpriteTemplate
    {
        public string VisualData;

        public LayerEnum Layer;

        public bool FlippedVertically;

        public bool FlippedHorizontally;

        public bool IsAnimation;

        public MapObjectType Type
        {
            get
            {
                return IsAnimation ? MapObjectType.Animation : MapObjectType.Sprite;
            }
        }
        public void Serialize(LittleEndianWriter writer)
        {
            writer.WriteUTF(VisualData);
            writer.WriteByte((byte)Layer);
            writer.WriteBoolean(FlippedVertically);
            writer.WriteBoolean(FlippedHorizontally);
            writer.WriteBoolean(IsAnimation);
        }
        public void Deserialize(LittleEndianReader reader)
        {
            this.VisualData = reader.ReadUTF();
            this.Layer = (LayerEnum)reader.ReadByte();
            this.FlippedVertically = reader.ReadBoolean();
            this.FlippedHorizontally = reader.ReadBoolean();
            this.IsAnimation = reader.ReadBoolean();
        }

    }

}
