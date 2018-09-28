using Microsoft.Xna.Framework;
using MonoFramework.IO.Maps;
using MonoFramework.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.ServerView.Objects
{
    public class MapRepresentation : GGrid
    {
        private MapTemplate Template
        {
            get;
            set;
        }
        public MapRepresentation(MapTemplate template) : base(new Vector2(), new Point(template.Width,
            template.Height), MapTemplate.MAP_CELL_SIZE, Color.Black, 1)
        {
            this.Template = template;
        }
        public void Load()
        {
            foreach (var cell in Template.Cells)
            {
                if (cell.Walkable == false)
                {
                    GetCell<GCell>(cell.Id).FillColor = Color.Black;
                }
            }
        }
    }
}
