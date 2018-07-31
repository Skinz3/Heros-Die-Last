using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoFramework.Cameras;
using MonoFramework.IO.Maps;
using MonoFramework.Objects.Abstract;
using MonoFramework.Scenes;
using MonoFramework.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFramework.Objects
{
    /// <summary>
    /// Représente une grille en 2D , elle est utilisé pour afficher une carte.
    /// </summary>
    public class GGrid : TextureOwnerObject
    {
        public event Action<GCell> OnMouseEnter;
        public event Action<GCell> OnMouseLeave;

        public event Action<GCell> OnMouseRightClick;
        public event Action<GCell> OnMouseLeftClick;

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
            private set;
        }
        public float CellSize
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
        public GGrid(Vector2 position, Point gridSize, float cellSize, Color color, int thickness) : base(position, new Point((int)(gridSize.X * cellSize), (int)(gridSize.Y * cellSize)), color)
        {
            this.GridSize = gridSize;
            this.CellSize = cellSize;
            this.Thickness = thickness;
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
        /// <summary>
        /// Require OnInitializationComplete
        /// </summary>
        public GCell GetCell(int x, int y)
        {
            return Cells.FirstOrDefault(w => w.RelativePosition == new Point(x, y));
        }
        public GCell GetCell(int id)
        {
            return Cells.FirstOrDefault(x => x.Id == id);
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
                    Cells[id] = new GCell(new Vector2(x, y), new Point(relativeX, relativeY), id, CellSize, Color, Thickness);
                    Cells[id].Initialize();
                    id++;
                    relativeY++;
                }
                relativeX++;
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
                MouseState state = Mouse.GetState();

                Point location = new Point(state.Position.X, state.Position.Y);

                if (Layer != LayerEnum.UI)
                {
                    float tX = location.X / Camera2D.MainCamera.Zoom;
                    float tY = location.Y / Camera2D.MainCamera.Zoom;

                    tX += Camera2D.MainCamera.Position.X;
                    tY += Camera2D.MainCamera.Position.Y;

                    location = new Point((int)tX, (int)tY);
                }
                if (cell.GRectangle.Rectangle.Intersects(new Rectangle(location, Debug.CURSOR_SIZE)))
                {
                    if (!cell.MouseIn)
                    {
                        OnMouseEnter?.Invoke(cell);
                    }
                    cell.MouseIn = true;

                    if (state.LeftButton == ButtonState.Pressed)
                    {
                        OnMouseLeftClick?.Invoke(cell);
                    }
                    if (state.RightButton == ButtonState.Pressed)
                    {
                        OnMouseRightClick?.Invoke(cell);
                    }

                }
                else if (cell.MouseIn)
                {
                    OnMouseLeave?.Invoke(cell);
                    cell.MouseIn = false;
                }

                cell.Update(time);
            }
        }

    }
    public class GCell : TextureOwnerObject
    {
        public int Id
        {
            get;
            private set;
        }
        private float CellSize
        {
            get;
            set;
        }
        public bool MouseIn
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
            private set;
        }

        public Color FillColor;

        public Color BackColor;

        public int Thickness
        {
            get;
            private set;
        }
        public Dictionary<LayerEnum, Sprite> Sprites
        {
            get;
            private set;
        }
        public GCell(Vector2 position, Point relativePosition, int id, float size, Color color, int thickness) : base(position, new Point((int)size, (int)size), color)
        {
            this.Id = id;
            this.CellSize = size;
            this.RelativePosition = relativePosition;
            this.Sprites = new Dictionary<LayerEnum, Sprite>();
            this.FillColor = Color.Transparent;
            this.BackColor = Color.Transparent;
            this.Thickness = thickness;
            this.DrawRectangle = true;
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
        public override void OnDraw(GameTime time)
        {
            Fill(BackColor);

            foreach (var sprite in Sprites.Values)
            {
                sprite.Draw(GRectangle.Rectangle, Color.White);
            }

            Fill(FillColor);

            if (DrawRectangle)
                this.GRectangle.Draw(time);

        }
        private void Fill(Color color)
        {
            if (color != Color.Transparent)
                Debug.FillRectangle(GRectangle.Rectangle, color);
        }
        public void AddSprite(Sprite sprite, LayerEnum layer)
        {
            if (Sprites.ContainsKey(layer))
                this.Sprites[layer] = sprite;
            else
                this.Sprites.Add(layer, sprite);
        }
        public void RemoveSprites()
        {
            this.Sprites.Clear();
        }
        public void RemoveSprite(LayerEnum layer)
        {
            this.Sprites.Remove(layer);
        }
        public override void OnUpdate(GameTime time)
        {

        }
    }
}
