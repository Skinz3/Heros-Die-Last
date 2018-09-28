﻿using MonoFramework.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFramework.IO.Maps
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
        public CellTemplate GetCellTemplate(int id)
        {
            return Cells.FirstOrDefault(x => x.Id == id);
        }

    }
    public class CellTemplate
    {
        public int Id;

        public SpriteTemplate[] Sprites;

        public bool Walkable;

        public void Serialize(LittleEndianWriter writer)
        {
            writer.WriteInt(Id);

            writer.WriteInt(Sprites.Length);

            foreach (var sprite in Sprites)
            {
                sprite.Serialize(writer);
            }

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

            Walkable = reader.ReadBoolean();
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
