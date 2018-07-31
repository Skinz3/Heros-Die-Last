using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoFramework.IO.Maps;
using MonoFramework.Sprites;

namespace MonoFramework.Objects
{
    public class GMap : GGrid
    {
        public const float MAP_CELL_SIZE = 50f;

        public GMap(Point size) : base(new Vector2(), size, MAP_CELL_SIZE, Color.Black, 1)
        {

        }
        public void Load(MapTemplate template)
        {
            foreach (var cell in template.Cells)
            {
                GCell gCell = GetCell(cell.Id);

                foreach (var sprite in cell.Sprites)
                {
                    var target = SpriteManager.GetSprite(sprite.SpriteName);

                    if (sprite.FlippedVertically || sprite.FlippedHorizontally)
                    {
                        target = Sprite.Flip(target, sprite.FlippedVertically, sprite.FlippedHorizontally);
                    }
                    gCell.AddSprite(target, sprite.Layer);
                }
            }
        }
        public MapTemplate Export()
        {
            MapTemplate result = new MapTemplate();

            result.Width = GridSize.X;
            result.Height = GridSize.Y;
            result.Cells = new CellTemplate[Cells.Length];

            for (int i = 0; i < Cells.Length; i++)
            {
                result.Cells[i] = new CellTemplate()
                {
                    Id = Cells[i].Id,
                };

                result.Cells[i].Sprites = new SpriteTemplate[Cells[i].Sprites.Count];

                for (int j = 0; j < Cells[i].Sprites.Count; j++)
                {
                    result.Cells[i].Sprites[j] = new SpriteTemplate();
                    result.Cells[i].Sprites[j].SpriteName = Cells[i].Sprites.ElementAt(j).Value.Name;
                    result.Cells[i].Sprites[j].Layer = Cells[i].Sprites.ElementAt(j).Key;
                    result.Cells[i].Sprites[j].FlippedVertically = Cells[i].Sprites.ElementAt(j).Value.FlippedVertically;
                    result.Cells[i].Sprites[j].FlippedHorizontally = Cells[i].Sprites.ElementAt(j).Value.FlippedHorizontally;
                }
            }
            return result;
        }
        public override void OnInitializeComplete()
        {
            // load sprites
            base.OnInitializeComplete();
        }
    }
}
