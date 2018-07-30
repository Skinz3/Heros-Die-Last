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

        public GMap(Point size) : base(new Vector2(), size, MAP_CELL_SIZE, Color.Black, 1f)
        {

        }
        public void Load(MapTemplate template)
        {
            foreach (var cell in template.Cells)
            {
                GCell gCell = GetCell(cell.Id);

                foreach (var sprite in cell.Sprites)
                {
                    gCell.AddSprite(SpriteManager.GetSprite(sprite.SpriteName), sprite.Layer);
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
