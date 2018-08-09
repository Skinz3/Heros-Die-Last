﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoFramework.Cameras;
using MonoFramework.Collisions;
using MonoFramework.Geometry;
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
    public class GGrid : SingleTextureObject
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
        public LayerEnum DisplayedLayer
        {
            get;
            private set;
        }
        public GGrid(Vector2 position, Point gridSize, float cellSize, Color color, int thickness) : base(position, new Point((int)(gridSize.X * cellSize), (int)(gridSize.Y * cellSize)), color)
        {
            this.GridSize = gridSize;
            this.CellSize = cellSize;
            this.Thickness = thickness;
            this.DisplayedLayer = LayerEnum.First | LayerEnum.Second | LayerEnum.Third;
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
        public GCell GetCell(int x, int y)
        {
            return Cells.FirstOrDefault(w => w.RelativePosition == new Point(x, y));
        }
        /// <summary>
        /// Vector2 = Screen position
        /// </summary>
        public GCell GetCell(Vector2 position)
        {
            return Cells.FirstOrDefault(x => x.IntersectsPoint(position.ToPoint()));
        }
        public GCell GetCell(int id)
        {
            return Cells.FirstOrDefault(x => x.Id == id);
        }
        public GCell[] GetCells(Rectangle intersectRectangle)
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
                    Cells[id] = new GCell(new Vector2(x, y), new Point(relativeX, relativeY), id, CellSize, Color, Layer, Thickness);
                    Cells[id].OnMouseEnter += Cell_OnMouseEnter;
                    Cells[id].OnMouseLeave += Cell_OnMouseLeave;
                    Cells[id].OnMouseLeftClick += Cell_OnMouseLeftClick;
                    Cells[id].OnMouseRightClick += Cell_OnMouseRightClick;
                    AddChild(Cells[id]); // !!
                    id++;
                    relativeY++;


                }
                relativeX++;

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
        public override void OnDraw(GameTime time)
        {

        }
        public override void OnUpdate(GameTime time)
        {

        }
    }
    public class GCell : SingleTextureObject
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
        public bool Walkable
        {
            get;
            set;
        }

        private GText GText
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
        public Dictionary<LayerEnum, Sprite> Sprites
        {
            get;
            private set;
        }
        public GCell(Vector2 position, Point relativePosition, int id, float size, Color color, LayerEnum layer, int thickness) : base(position, new Point((int)size, (int)size), color)
        {
            this.Id = id;
            this.CellSize = size;
            this.RelativePosition = relativePosition;
            this.Sprites = new Dictionary<LayerEnum, Sprite>();
            this.FillColor = Color.Transparent;
            this.BackColor = Color.Transparent;
            this.Thickness = thickness;
            this.DrawRectangle = true;
            this.Walkable = true;
            this.Layer = layer;
            this.DisplayedLayers = LayerEnum.First | LayerEnum.Second | LayerEnum.Third;
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

        public void RemoveText()
        {
            GText = null;
        }

        public override void OnDraw(GameTime time)
        {
            Fill(BackColor);

            foreach (var pair in Sprites)
            {
                if (DisplayedLayers.HasFlag(pair.Key))
                    pair.Value.Draw(GRectangle.Rectangle, Color.White);
            }

            Fill(FillColor);

            if (DrawRectangle)
                this.GRectangle.Draw(time);

            if (GText != null)
                GText.Draw(time);
        }
        public void SetText(string text, Color color, Alignment alignment = Alignment.Center, float scale = 1f)
        {
            GText = new GText(Position, SceneManager.CurrentScene.TextRenderer.GetDefaultSpriteFont(),
                text, color, scale);
            GText.Align(Rectangle, alignment);

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
            if (GText != null)
            {
                GText.Update(time);
            }
        }
        public GCell[] GetAdjacentCells(GGrid grid)
        {
            GCell[] cells = new GCell[4];

            cells[0] = grid.GetCell(RelativePosition.X + 1, RelativePosition.Y);
            cells[1] = grid.GetCell(RelativePosition.X - 1, RelativePosition.Y);
            cells[2] = grid.GetCell(RelativePosition.X, RelativePosition.Y + 1);
            cells[3] = grid.GetCell(RelativePosition.X, RelativePosition.Y - 1);

            return cells;
        }
        public GCell[] GetNextCells(GGrid grid, DirectionEnum direction, int length)
        {
            List<GCell> cells = new List<GCell>();

            var vector = direction.GetInputVector().ToPoint();

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
            GText = null;
            BackColor = Color.Transparent;
            FillColor = Color.Transparent;
        }
        public override string ToString()
        {
            return GetType().Name + " Id:" + Id + " Relative Position:" + RelativePosition;
        }
    }
}
