using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rogue.Core;
using Rogue.Core.IO.Maps;
using Rogue.Core.Objects;
using Rogue.Server.Records;
using Rogue.Server.World.Entities;

namespace Rogue.Server.World.Maps.Triggers
{
    /// <summary>
    /// Value1 = LockSpriteChange
    /// Value2 = TargetCellId
    /// Value3 = TargetLayer
    /// Value4 = TargetMapObjectType
    /// Value5 = TargetVisualData
    /// Value6 = TargetWalkable
    /// </summary>
    public class LockTrigger : Trigger
    {
        private bool Locked
        {
            get;
            set;
        }
        private SpriteTemplate DefaultSprite
        {
            get;
            set;
        }
        public LockTrigger(MapInstance mapInstance, MapInteractiveRecord interactiveElement) : base(mapInstance, interactiveElement)
        {
            this.Locked = true;
            DefaultSprite = MapInstance.Record.Template.GetSpriteTemplate(interactiveElement.CellId, interactiveElement.LayerEnum);


        }
        public override void OnClicked(Player player)
        {
            Locked = !Locked;

            if (!Locked)
            {
                MapInstance.UpdateElementVisual(this.InteractiveElement.CellId, this.InteractiveElement.LayerEnum, InteractiveElement.Value1, DefaultSprite.Type, false);
                MapInstance.RemoveMapElement(int.Parse(InteractiveElement.Value2), InteractiveElement.Value3.ParseEnum<LayerEnum>(), true);
            }
            else
            {
                MapInstance.UpdateElementVisual(this.InteractiveElement.CellId, InteractiveElement.LayerEnum, DefaultSprite.VisualData, DefaultSprite.Type, false);
                MapInstance.AddMapElement(int.Parse(InteractiveElement.Value2), InteractiveElement.Value3.ParseEnum<LayerEnum>(), false, InteractiveElement.Value5, InteractiveElement.Value4.ParseEnum<MapObjectType>());
            }
        }

        public override void Update(long deltaTime)
        {

        }
    }
}
