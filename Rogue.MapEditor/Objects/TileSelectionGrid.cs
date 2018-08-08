using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoFramework.Input;
using MonoFramework.Objects;
using MonoFramework.Objects.Abstract;
using MonoFramework.Scenes;
using MonoFramework.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.MapEditor.Objects
{
    public class TileSelectionGrid : GGrid
    {
        private int SpriteIndex
        {
            get
            {
                return m_spriteIndex;
            }
            set
            {
                if (value < 0)
                {
                    m_spriteIndex = 0;
                }
                else if (value >= Sprites.Length - (GridSize.X * GridSize.Y) + GridSize.Y)
                {
                    return;
                }
                else
                    m_spriteIndex = value;
            }
        }

        private int m_spriteIndex = 0;

        public GCursor Cursor
        {
            get;
            private set;
        }
        public Sprite SelectedSprite
        {
            get
            {
                return Cursor.Sprite;
            }
        }
        private Sprite[] Sprites
        {
            get;
            set;
        }
        public TileSelectionGrid(Vector2 position, Point gridSize, int cellSize, Color color, int thickness) : base(position, gridSize, cellSize, color, thickness)
        {
            this.OnMouseEnterCell += TileSelectionGrid_OnMouseEnter;
            this.OnMouseLeaveCell += TileSelectionGrid_OnMouseLeave;
            this.OnMouseLeftClickCell += TileSelectionGrid_OnMouseLeftClick;
            this.OnMouseRightClickCell += TileSelectionGrid_OnMouseRightClick;
            this.Cursor = new GCursor(Color.White, null, new Point(cellSize, cellSize));
        }


        public override void OnInitialize()
        {
            this.Sprites = SpriteManager.GetSprites();
            this.AddChild(Cursor);

            base.OnInitialize();
        }

        public override void OnInitializeComplete()
        {
            DisplaySprite();
        }
        private void DisplaySprite()
        {
            for (int i = SpriteIndex; i < SpriteIndex + Cells.Length; i++)
            {
                if (i < Sprites.Length)
                {
                    if (!Sprites[i].Loaded)
                        Sprites[i].Load();

                    Cells[i - SpriteIndex].BackColor = Color.White;
                    Cells[i - SpriteIndex].AddSprite(Sprites[i], LayerEnum.First);

                }
                else
                {
                    Cells[i - SpriteIndex].RemoveSprites();
                    break;
                }
            }
            for (int i = 0; i < SpriteIndex + Cells.Length; i++)
            {
                if (i >= Sprites.Length && i - SpriteIndex > 0)
                    Cells[i - SpriteIndex].RemoveSprites();
            }

        }

        private void TileSelectionGrid_OnMouseLeftClick(GCell obj)
        {
            if (obj.Sprites.ContainsKey(LayerEnum.First))
            {
                Cursor.Sprite = obj.Sprites[LayerEnum.First];
            }
        }
        private void TileSelectionGrid_OnMouseRightClick(GCell obj)
        {
            Cursor.Sprite = null;
        }

        private void TileSelectionGrid_OnMouseEnter(GCell obj)
        {
            obj.FillColor = Color.Red;
            obj.FillColor.A = 50;
        }
        private void TileSelectionGrid_OnMouseLeave(GCell obj)
        {
            obj.FillColor = Color.Transparent;
        }
        public override void OnUpdate(GameTime time)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                SpriteIndex += GridSize.Y;
                DisplaySprite();
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                SpriteIndex -= GridSize.Y;
                DisplaySprite();
            }
            base.OnUpdate(time);
        }
    }
}
