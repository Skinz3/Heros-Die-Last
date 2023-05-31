using Microsoft.Xna.Framework;
using Rogue.Core;
using Rogue.Core.Collisions;
using Rogue.Core.DesignPattern;
using Rogue.Core.Geometry;
using Rogue.Core.IO.Maps;
using Rogue.Core.Objects;
using Rogue.Core.Pathfinding;
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
        public MapCell[] GetCells(Predicate<MapCell> predicate)
        {
            return Array.FindAll(Cells, predicate);
        }
        public MapCell[] GetCells()
        {
            return Cells;
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
                    Cells[id] = new MapCell(this, new Vector2(x, y), new Point(relativeX, relativeY), id, CellSize);
                    Cells[id].Walkable = Template.Cells[id].Walkable;
                    id++;
                    relativeY++;
                }
                relativeX++;

            }


            foreach (var cell in Cells)
            {
                cell.Adjacents = cell.GetAdjacentCells();
            }

        }

        public T GetCell<T>(int cellId) where T : ICell
        {
            return (T)GetCell(cellId);
        }
        public ICell GetCell(int cellId)
        {
            if (cellId >= Cells.Length || cellId < 0)
                return null;
            else
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
        public MapCell GetCell(Func<MapCell, bool> predicate)
        {
            return Cells.FirstOrDefault(predicate);
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
        private IGrid Grid
        {
            get;
            set;
        }
        public MapCell(IGrid grid, Vector2 position, Point relativePosition, int id, int size) : base(position, new Point(size, size))
        {
            this.Grid = grid;
            this.RelativePosition = relativePosition;
            this.Id = id;
        }
        public Vector2 GetCenterPosition(int width, int height)
        {
            return new Vector2(Rectangle.Center.X - width / 2, Rectangle.Center.Y - height / 2);
        }

        public ICell[] GetNextCells(DirectionEnum direction, int length)
        {
            List<ICell> cells = new List<ICell>();

            var vector = direction.GetInputVector().ToPoint();

            vector.X = vector.X == 0.5f ? 1 : vector.X;
            vector.Y = vector.Y == 0.5f ? 1 : vector.Y;

            for (int i = 1; i < length + 1; i++)
            {
                cells.Add(Grid.GetCell(RelativePosition.X + vector.X * i, RelativePosition.Y + vector.Y * i));
            }
            return cells.ToArray();
        }
        public ICell GetAdjacentCell(DirectionEnum direction)
        {
            switch (direction)
            {
                case DirectionEnum.Right:
                    return Grid.GetCell(Id + Grid.GridSize.Y);
                case DirectionEnum.Left:
                    return Grid.GetCell(Id - Grid.GridSize.Y);
                case DirectionEnum.Up:
                    return Grid.GetCell(Id - 1);
                case DirectionEnum.Down:
                    return Grid.GetCell(Id + 1);
                case DirectionEnum.Right | DirectionEnum.Up:
                    return GetAdjacentCell(DirectionEnum.Right)?.GetAdjacentCell(DirectionEnum.Up);
                case DirectionEnum.Right | DirectionEnum.Down:
                    return GetAdjacentCell(DirectionEnum.Right)?.GetAdjacentCell(DirectionEnum.Down);
                case DirectionEnum.Left | DirectionEnum.Up:
                    return GetAdjacentCell(DirectionEnum.Left)?.GetAdjacentCell(DirectionEnum.Up);
                case DirectionEnum.Left | DirectionEnum.Down:
                    return GetAdjacentCell(DirectionEnum.Left)?.GetAdjacentCell(DirectionEnum.Down);
            }
            throw new Exception("unable to find adjacent cell.");
        }
        public ICell[] GetAdjacentCells()
        {
            ICell[] cells = new ICell[4];

            cells[0] = GetAdjacentCell(DirectionEnum.Right);
            cells[1] = GetAdjacentCell(DirectionEnum.Left);
            cells[2] = GetAdjacentCell(DirectionEnum.Up);
            cells[3] = GetAdjacentCell(DirectionEnum.Down);

            return cells.Where(x => x != null).ToArray();
        }
        public bool IntersectsPoint(Point point)
        {
            return Rectangle.Intersects(new Rectangle(point, new Point(1)));
        }
        public bool IntersectsRectangle(Rectangle rectangle)
        {
            return Rectangle.Intersects(rectangle);
        }
        public bool IntersectsCircle(Vector2 center, float radius)
        {
            return GeometryExtensions.CircleRectangleCollide(center, radius, Rectangle);
        }

        public override MapCell GetCell()
        {
            return this;
        }
        public override MapInstance GetMapInstance()
        {
            throw new NotImplementedException();
        }
        public override void DefineMapInstance(MapInstance instance)
        {
            throw new NotImplementedException();
        }

        public override void LeaveMapInstance()
        {
            throw new NotImplementedException();
        }


    }
}
