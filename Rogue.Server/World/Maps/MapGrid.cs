using Microsoft.Xna.Framework;
using MonoFramework.DesignPattern;
using MonoFramework.IO.Maps;
using MonoFramework.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Server.World.Maps
{
    public class MapGrid
    {
        private MapCell[] Cells
        {
            get;
            set;
        }
        private MapTemplate Template
        {
            get;
            set;
        }
        private Point GridSize
        {
            get
            {
                return new Point(Template.Width, Template.Height);
            }
        }
        private int CellSize
        {
            get;
            set;
        }
        [InDeveloppement(InDeveloppementState.TODO)]
        private Point Position
        {
            get;
            set;
        }
        public MapGrid(MapTemplate template)
        {
            this.Template = template;
            this.CellSize = MapTemplate.MAP_CELL_SIZE;
        }
        public void Load()
        {
            Cells = new MapCell[GridSize.X * GridSize.Y];

            int id = 0;
            int relativeX = 0;
            for (float x = Position.X; x < Position.X + GridSize.X * CellSize; x += CellSize)
            {
                int relativeY = 0;

                for (float y = Position.Y; y < Position.Y + GridSize.Y * CellSize; y += CellSize)
                {
                    Cells[id] = new MapCell(new Vector2(x, y), new Point(relativeX, relativeY), id, CellSize);
                    Cells[id].Walkable = Template.Cells[id].Walkable;
                    id++;
                    relativeY++;
                    relativeX++;

                }
            }
        }

        public MapCell GetCell(int cellId)
        {
            return Cells[cellId];
        }
        public MapCell GetCell(Point relativePosition)
        {
            return Cells.FirstOrDefault(x => x.RelativePosition == relativePosition);
        }

      
    }
    public class MapCell
    {
        public int Id
        {
            get;
            set;
        }
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle(Position.ToPoint(), new Point(Size));
            }
        }
        public Point RelativePosition
        {
            get;
            set;
        }
        public Vector2 Position
        {
            get;
            set;
        }
        public bool Walkable
        {
            get;
            set;
        }
        public int Size
        {
            get;
            set;
        }
        public MapCell(Vector2 position, Point relativePosition, int id, int size)
        {
            this.Position = position;
            this.RelativePosition = relativePosition;
            this.Id = id;
            this.Size = size;
        }
    }
}
