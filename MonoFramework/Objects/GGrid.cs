using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Rogue.Core.Cameras;
using Rogue.Core.Collisions;
using Rogue.Core.DesignPattern;
using Rogue.Core.Geometry;
using Rogue.Core.IO.Maps;
using Rogue.Core.Lightning;
using Rogue.Core.Objects.Abstract;
using Rogue.Core.Scenes;
using Rogue.Core.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Core.Objects
{
    public interface IGrid
    {
        Point GridSize
        {
            get;
            set;
        }
        ICell GetCell(int x, int y);
        /// <summary>
        /// Vector2 = Screen position
        /// </summary>
        ICell GetCell(Vector2 position);

        ICell GetCell(int id);
    }
    public interface ICell
    {
        int Id
        {
            get;
            set;
        }
        bool Walkable
        {
            get;
            set;
        }
        Point RelativePosition
        {
            get;
            set;
        }
        Vector2 Center
        {
            get;
        }

        Rectangle Rectangle
        {
            get;
        }
        ICell[] Adjacents
        {
            get;
            set;
        }

        ICell GetAdjacentCell(DirectionEnum direction);
    }
    /// <summary>
    /// Représente une grille en 2D , elle est utilisé pour afficher une carte.
    /// </summary>
    public class GGrid : SingleTextureObject, IGrid
    {
        public event Action<GCell> OnMouseEnterCell;
        public event Action<GCell> OnMouseLeaveCell;

        public event Action<GCell> OnMouseRightClickCell;
        public event Action<GCell> OnMouseLeftClickCell;

        /// <summary>
        /// Require OnInitializationComplete
        /// </summary>
        public GCell[] Cells
        {
            get;
            private set;
        }
        public Point GridSize
        {
            get;
            set;
        }
        public int CellSize
        {
            get;
            private set;
        }
        public Color FillColor
        {
            get;
            private set;
        }
        public int Thickness
        {
            get;
            private set;
        }
        public LayerEnum DisplayedLayer
        {
            get;
            private set;
        }
        /// <summary>
        /// A 2D Tiled Grid
        /// </summary>
        /// <param name="position">The position (top,left) of the grid</param>
        /// <param name="gridSize">The grid relative size (Width,Height)</param>
        /// <param name="cellSize">The cell size in pixel</param>
        /// <param name="color">The grid stroke color</param>
        /// <param name="thickness">The grid stroke thickness</param>
        /// <param name="ignoreMouseEvents">optimize?</param>
        public GGrid(Vector2 position, Point gridSize, int cellSize, Color color, int thickness, bool ignoreMouseEvents) : base(position, new Point((int)(gridSize.X * cellSize), (int)(gridSize.Y * cellSize)), color)
        {
            this.GridSize = gridSize;
            this.CellSize = cellSize;
            this.Thickness = thickness;
            this.DisplayedLayer = LayerEnum.First | LayerEnum.Second | LayerEnum.Third;
            this.IgnoreMouseEvents = ignoreMouseEvents;
        }
        /// <summary>
        /// Require OnInitializationComplete
        /// </summary>
        public void ToogleDrawRectangles(bool draw)
        {
            foreach (var cell in Cells)
            {
                cell.DrawRectangle = draw;
            }
        }

        public void DefineDisplayedLayer(LayerEnum displayedLayer)
        {
            foreach (var cell in Cells)
            {
                cell.DisplayedLayers = displayedLayer;
            }
            this.DisplayedLayer = displayedLayer;
        }
        /// <summary>
        /// (x,y) = Relative Position
        /// Require OnInitializationComplete
        /// </summary>
        public ICell GetCell(int x, int y)
        {
            return Cells.FirstOrDefault(w => w.RelativePosition == new Point(x, y));
        }
        /// <summary>
        /// Vector2 = Screen position
        /// </summary>
        public ICell GetCell(Vector2 position)
        {
            return Cells.FirstOrDefault(x => x.IntersectsPoint(position.ToPoint()));
        }
        public T GetCell<T>(int id) where T : ICell
        {
            return (T)GetCell(id);
        }
        public ICell GetCell(int cellId)
        {
            if (cellId >= Cells.Length || cellId < 0)
                return null;
            else
                return Cells[cellId];
        }
        public ICell[] GetCells(Rectangle intersectRectangle)
        {
            return Cells.Where(x => x.Rectangle.Intersects(intersectRectangle)).ToArray();
        }

        public override Texture2D CreateTexture(GraphicsDevice graphicsDevice)
        {
            return Debug.DummyTexture;
        }
        public override void OnInitialize()
        {
            Cells = new GCell[GridSize.X * GridSize.Y];

            int id = 0;
            int relativeX = 0;
            for (float x = Position.X; x < Position.X + GridSize.X * CellSize; x += CellSize)
            {
                int relativeY = 0;

                for (float y = Position.Y; y < Position.Y + GridSize.Y * CellSize; y += CellSize)
                {
                    Cells[id] = new GCell(this, new Vector2(x, y), new Point(relativeX, relativeY), id, CellSize, Color, Layer, Thickness);
                    Cells[id].IgnoreMouseEvents = this.IgnoreMouseEvents;

                    if (this.IgnoreMouseEvents == false)
                    {
                        Cells[id].OnMouseEnter += Cell_OnMouseEnter;
                        Cells[id].OnMouseLeave += Cell_OnMouseLeave;
                        Cells[id].OnMouseLeftClick += Cell_OnMouseLeftClick;
                        Cells[id].OnMouseRightClick += Cell_OnMouseRightClick;
                    }

                    Cells[id].Initialize();

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
        public override void OnInitializeComplete()
        {
            base.OnInitializeComplete();
        }

        private void Cell_OnMouseRightClick(PositionableObject obj)
        {
            OnMouseRightClickCell?.Invoke((GCell)obj);
        }

        private void Cell_OnMouseLeftClick(PositionableObject obj)
        {
            OnMouseLeftClickCell?.Invoke((GCell)obj);
        }

        private void Cell_OnMouseLeave(PositionableObject obj)
        {
            OnMouseLeaveCell?.Invoke((GCell)obj);
        }

        private void Cell_OnMouseEnter(PositionableObject obj)
        {
            OnMouseEnterCell?.Invoke((GCell)obj);
        }
        public void DrawLayer(GameTime time, LayerEnum layer)
        {
            foreach (var cell in Cells)
            {
                cell.DrawLayer(time, layer);
            }
        }
        public void DrawLights(GameTime time)
        {
            foreach (var cell in Cells)
            {
                cell.DrawLight(time);
            }
        }

        public override void OnDraw(GameTime time)
        {
            foreach (var cell in Cells)
            {
                cell.Draw(time);
            }
        }
        public override void OnUpdate(GameTime time)
        {
            foreach (var cell in Cells)
            {
                cell.Update(time);
            }
        }
        public void Clean()
        {
            foreach (var cell in Cells)
            {
                cell.Clean();
            }
        }

        public override void OnDispose()
        {

        }


    }
    public interface ILayerElement
    {
        void Draw(Rectangle rectangle, Color color, float rotation = 0f, Vector2 origin = new Vector2());

        void Update(GameTime time);

        void Dispose();
    }
    public class GCell : SingleTextureObject, ICell
    {
        public int Id
        {
            get;
            set;
        }
        private int CellSize
        {
            get;
            set;
        }
        public bool Clicked
        {
            get;
            set;
        }
        public GRectangle GRectangle
        {
            get;
            private set;
        }
        public bool DrawRectangle
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

        public Color FillColor;

        public Color BackColor;

        public int Thickness
        {
            get;
            private set;
        }
        public LayerEnum DisplayedLayers
        {
            get;
            set;
        }
        public Dictionary<LayerEnum, Color> SpriteColors
        {
            get;
            private set;
        }
        public Dictionary<LayerEnum, ILayerElement> Sprites
        {
            get;
            private set;
        }
        public PointLight Light
        {
            get;
            private set;
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
        public Dictionary<LayerEnum, T> GetElements<T>() where T : ILayerElement
        {
            return Sprites.Where(x => x.Value is T).ToDictionary(x => x.Key, x => (T)x.Value);
        }
        public T GetElement<T>(LayerEnum layer) where T : ILayerElement
        {
            var value = Sprites[layer];

            if (value is T)
            {
                return (T)value;
            }
            else
                return default(T);
        }
        public GCell(IGrid grid, Vector2 position, Point relativePosition, int id, int size, Color color, LayerEnum layer, int thickness) : base(position, new Point((int)size, (int)size), color)
        {
            this.Grid = grid;
            this.Id = id;
            this.CellSize = size;
            this.RelativePosition = relativePosition;
            this.Sprites = new Dictionary<LayerEnum, ILayerElement>();
            this.FillColor = Color.Transparent;
            this.BackColor = Color.Transparent;
            this.Thickness = thickness;
            this.DrawRectangle = true;
            this.Walkable = true;
            this.Layer = layer;
            this.DisplayedLayers = LayerEnum.First | LayerEnum.Second | LayerEnum.Third;
            this.SpriteColors = new Dictionary<LayerEnum, Color>()
            {
                {LayerEnum.First,Color.White },
                {LayerEnum.Second,Color.White },
                {LayerEnum.Third,Color.White },
            };
        }


        public override void OnInitialize()
        {
            this.GRectangle = new GRectangle(Position, Size, Color, Thickness);
            this.GRectangle.Initialize();
        }
        public override Texture2D CreateTexture(GraphicsDevice graphicsDevice)
        {
            return Debug.DummyTexture;
        }
        public void SetLight(Color color, int radius, float sharpness)
        {
            this.Light = new PointLight(this, radius, color, sharpness);
        }
        public void RemoveLight()
        {
            this.Light = null;
        }

        public override void OnDraw(GameTime time)
        {
            Fill(BackColor);

            foreach (var pair in Sprites)
            {
                if (DisplayedLayers.HasFlag(pair.Key))
                    pair.Value.Draw(GRectangle.Rectangle, SpriteColors[pair.Key]);
            }

            Fill(FillColor);

            if (DrawRectangle)
                this.GRectangle.Draw(time);

            if (Text != null)
                Text.Draw(time);
        }
        public void DrawLayer(GameTime time, LayerEnum layer)
        {
            if (layer == LayerEnum.First)
            {
                Fill(BackColor);
            }
            if (Sprites.ContainsKey(layer))
            {
                Sprites[layer].Draw(GRectangle.Rectangle, SpriteColors[layer]);
            }
            if (layer == LayerEnum.Third)
            {

                Fill(FillColor);

                if (DrawRectangle)
                    this.GRectangle.Draw(time);

                if (Text != null)
                    Text.Draw(time);


            }
        }
        public void DrawLight(GameTime time)
        {
            if (Light != null)
            {
                Light.Draw(time);
            }
        }
        private void Fill(Color color)
        {
            if (color != Color.Transparent)
                Debug.FillRectangle(GRectangle.Rectangle, color);
        }
        public void SetSprite(ILayerElement sprite, LayerEnum layer)
        {
            if (Sprites.ContainsKey(layer))
                this.Sprites[layer] = sprite;
            else
                this.Sprites.Add(layer, sprite);

            OrderSprites();

        }
        public void OrderSprites()
        {
            Sprites = Sprites.OrderByDescending(x => (int)x.Key).Reverse().ToDictionary(x => x.Key, x => x.Value);
        }
        public void RemoveSprites()
        {
            this.Sprites.Clear();
        }
        public void RemoveSprite(LayerEnum layer)
        {
            this.Sprites.Remove(layer);
        }
        public override void Update(GameTime time)
        {
            base.Update(time);
        }
        public override void OnUpdate(GameTime time)
        {
            GRectangle.Position = Position;

            Light?.Update(time);
            Text?.Update(time);

            foreach (var pair in Sprites)
            {
                if (pair.Value != null)
                {
                    pair.Value.Update(time);
                }
            }
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
        public ICell[] GetNextCells(GGrid grid, DirectionEnum direction, int length)
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
        public void Clean()
        {
            RemoveSprites();
            Walkable = true;
            RemoveText();
            BackColor = Color.Transparent;
            FillColor = Color.Transparent;
        }
        public Vector2 GetCenterPosition(int width, int height)
        {
            return new Vector2(Rectangle.Center.X - width / 2, Rectangle.Center.Y - height / 2);
        }
        public override string ToString()
        {
            return GetType().Name + " Id:" + Id + " Relative Position:" + RelativePosition;
        }

        public override void OnDispose()
        {
            Clean();

            foreach (var sprite in Sprites)
            {
                sprite.Value.Dispose();
            }
        }


    }
}
