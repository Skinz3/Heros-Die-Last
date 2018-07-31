using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
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
                else
                    m_spriteIndex = value;
            }
        }

        private int m_spriteIndex = 0;

        private GCursor Cursor
        {
            get;
            set;
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
        public TileSelectionGrid(Vector2 position, Point gridSize, int cellSize, Color color, int thickness) : base(position, gridSize, cellSize, color,thickness)
        {
            this.OnMouseEnter += TileSelectionGrid_OnMouseEnter;
            this.OnMouseLeave += TileSelectionGrid_OnMouseLeave;
            this.OnMouseLeftClick += TileSelectionGrid_OnMouseLeftClick;
            this.OnMouseRightClick += TileSelectionGrid_OnMouseRightClick;
            this.Cursor = new GCursor(new Vector2(), Color.White, null, new Point(cellSize, cellSize));
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
            // Console.WriteLine(SpriteIndex);
            for (int i = SpriteIndex; i < SpriteIndex + Cells.Length; i++)
            {
                if (i < Sprites.Length)
                {
                    if (!Sprites[i].Loaded)
                        Sprites[i].Load();
                    
                    // check if the cell sprite is used in map, if not, dispose it
                    Cells[i - SpriteIndex].BackColor = Color.White;
                    Cells[i - SpriteIndex].AddSprite(Sprites[i], LayerEnum.FIRST);

                }
                else
                {
                    Cells[i - SpriteIndex].RemoveSprites();
                    break;
                }
            }
           
        }

        private void TileSelectionGrid_OnMouseLeftClick(GCell obj)
        {
            if (obj.Sprites.ContainsKey(LayerEnum.FIRST))
            {
                Cursor.Sprite = obj.Sprites[LayerEnum.FIRST];
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
                SpriteIndex += 1;
                DisplaySprite();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                SpriteIndex -= 1;
                DisplaySprite();
            }
            base.OnUpdate(time);
        }
    }
}
