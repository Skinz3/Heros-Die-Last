using Microsoft.Xna.Framework;
using MonoFramework;
using MonoFramework.Collisions;
using MonoFramework.DesignPattern;
using MonoFramework.Geometry;
using MonoFramework.IO.Maps;
using MonoFramework.Objects;
using MonoFramework.Pathfinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Server.World.Maps
{
    public class MapGrid : IGrid
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
        public Point GridSize
        {
            get;
            set;
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
            this.Position = new Point();
            this.GridSize = new Point(Template.Width, Template.Height);
        }
        public MapCell[] GetCells(Rectangle intersectRectangle)
        {
            return Cells.Where(x => x.Rectangle.Intersects(intersectRectangle)).ToArray();
        }
        public MapCell RandomWalkableCell()
        {
            return Cells.Where(x => x.Walkable).Random();
        }

        [Obsolete("Really slow")]
        public MapCell RandomPathableCell(int start)
        {
            foreach (var cell in Cells.Where(x => x.Walkable).Shuffle())
            {
                if (new AStar(this, start, cell.Id).FindPath().Length > 0)
                    return cell;
            }
            return null;
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
                }
                relativeX++;

            }
            foreach (var cell in Cells)
            {
                cell.Adjacents = cell.GetAdjacentCells(this);
            }
        }

        public T GetCell<T>(int cellId) where T : ICell
        {
            return (T)GetCell(cellId);
        }
        public ICell GetCell(int cellId)
        {
            return Cells[cellId];
        }
        public ICell GetCell(int x, int y)
        {
            return Cells.FirstOrDefault(w => w.RelativePosition == new Point(x, y));
        }
        public ICell GetCell(Vector2 intersectPoint)
        {
            return Cells.FirstOrDefault(x => x.IntersectsPoint(intersectPoint.ToPoint()));
        }


    }
    public class MapCell : ServerObject, ICell
    {
        public int Id
        {
            get;
            set;
        }
        public Point RelativePosition
        {
            get;
            set;
        }
        public bool Walkable
        {
            get;
            set;
        }
        public ICell[] Adjacents
        {
            get;
            set;
        }

        public MapCell(Vector2 position, Point relativePosition, int id, int size) : base(position, new Point(size, size))
        {
            this.RelativePosition = relativePosition;
            this.Id = id;
        }
        public Vector2 GetCenterPosition(int width, int height)
        {
            return new Vector2(Rectangle.Center.X - width / 2, Rectangle.Center.Y - height / 2);
        }
        public ICell[] GetAdjacentCells(MapGrid grid)
        {
            ICell[] cells = new ICell[4];

            cells[0] = grid.GetCell(RelativePosition.X + 1, RelativePosition.Y);
            cells[1] = grid.GetCell(RelativePosition.X - 1, RelativePosition.Y);
            cells[2] = grid.GetCell(RelativePosition.X, RelativePosition.Y + 1);
            cells[3] = grid.GetCell(RelativePosition.X, RelativePosition.Y - 1);

            return cells.Where(x => x != null).ToArray();
        }
        public ICell[] GetNextCells(MapGrid grid, DirectionEnum direction, int length)
        {
            List<ICell> cells = new List<ICell>();

            var vector = direction.GetInputVector().ToPoint();

            vector.X = vector.X == 0.5f ? 1 : vector.X;
            vector.Y = vector.Y == 0.5f ? 1 : vector.Y;

            for (int i = 1; i < length + 1; i++)
            {
                cells.Add(grid.GetCell(RelativePosition.X + vector.X * i, RelativePosition.Y + vector.Y * i));
            }
            return cells.ToArray();
        }
        public bool IntersectsPoint(Point point)
        {
            return Rectangle.Intersects(new Rectangle(point, new Point(1)));
        }

        public override MapInstance GetMapInstance()
        {
            throw new NotImplementedException();
        }

        public override MapCell GetCell()
        {
            return this;
        }
    }
}
