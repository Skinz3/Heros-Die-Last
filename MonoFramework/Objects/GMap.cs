using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Rogue.Core.Animations;
using Rogue.Core.DesignPattern;
using Rogue.Core.IO.Maps;
using Rogue.Core.Scenes;
using Rogue.Core.Sprites;

namespace Rogue.Core.Objects
{
    public class GMap : GGrid
    {
        public GMap(Point size, bool ignoreMouseEvents) : base(new Vector2(), size, MapTemplate.MAP_CELL_SIZE, Color.Black, 1, ignoreMouseEvents)
        {

        }
        public void Load(MapTemplate template)
        {
            Clean();

            foreach (var cell in template.Cells)
            {
                GCell gCell = (GCell)GetCell(cell.Id);

                gCell.Walkable = cell.Walkable;

                foreach (var sprite in cell.Sprites)
                {
                    if (sprite.IsAnimation)
                    {
                        gCell.SetSprite(AnimationManager.GetAnimation(sprite.VisualData), sprite.Layer);
                    }
                    else
                    {
                        var target = SpriteManager.GetSprite(sprite.VisualData);

                        if (sprite.FlippedVertically || sprite.FlippedHorizontally)
                        {
                            target = Sprite.Flip(target, sprite.FlippedVertically, sprite.FlippedHorizontally);
                        }
                        gCell.SetSprite(target, sprite.Layer);
                    }
                }

                if (cell.Light != null)
                {
                    gCell.SetLight(new Color(cell.Light.R, cell.Light.G, cell.Light.B, cell.Light.A), cell.Light.Radius, cell.Light.Sharpness);
                }
            }
        }
        [InDeveloppement(InDeveloppementState.TEMPORARY, "Not updated since new map editor")]
        public MapTemplate Export(float zoom)
        {
            MapTemplate result = new MapTemplate();

            result.Width = GridSize.X;
            result.Height = GridSize.Y;
            result.Cells = new CellTemplate[Cells.Length];
            result.Zoom = zoom;

            for (int i = 0; i < Cells.Length; i++)
            {
                result.Cells[i] = new CellTemplate()
                {
                    Id = Cells[i].Id,
                    Walkable = Cells[i].Walkable,
                };

                result.Cells[i].Sprites = new SpriteTemplate[Cells[i].Sprites.Count];

                var sprites = Cells[i].GetElements<Sprite>();

                for (int j = 0; j < sprites.Count(); j++)
                {
                    result.Cells[i].Sprites[j] = new SpriteTemplate();
                    result.Cells[i].Sprites[j].VisualData = sprites.ElementAt(j).Value.Name;
                    result.Cells[i].Sprites[j].Layer = sprites.ElementAt(j).Key;
                    result.Cells[i].Sprites[j].FlippedVertically = sprites.ElementAt(j).Value.FlippedVertically;
                    result.Cells[i].Sprites[j].FlippedHorizontally = sprites.ElementAt(j).Value.FlippedHorizontally;
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
