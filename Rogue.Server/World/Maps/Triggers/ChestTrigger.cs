using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rogue.Core;
using Rogue.Core.IO.Maps;
using Rogue.Core.Objects;
using Rogue.Core.Utils;
using Rogue.Protocol.Messages.Server;
using Rogue.Server.Records;
using Rogue.Server.World.Entities;

namespace Rogue.Server.World.Maps.Triggers
{
    /// <summary>
    /// Value1 = ChestOpen
    /// </summary>
    public class ChestTrigger : Trigger
    {
        private bool Loot
        {
            get;
            set;
        }
        private bool Open
        {
            get;
            set;
        }
        private SpriteTemplate DefaultSprite
        {
            get;
            set;
        }
        public ChestTrigger(MapInstance mapInstance, MapInteractiveRecord interactiveElement) : base(mapInstance, interactiveElement)
        {
            DefaultSprite = MapInstance.Record.Template.GetSpriteTemplate(interactiveElement.CellId, interactiveElement.LayerEnum);
        }

        private void LootItem(Player player)
        {
            var targetItem = ItemRecord.Items.Random(); //ItemRecord.Items.Find(x => x.Id == 104); 
            var quantity = new AsyncRandom().Next(1, 200);
            player.Inventory.AddItem(targetItem.Id, quantity);
            player.MapInstance.Send(new ItemLootChestMessage(targetItem.Id, targetItem.Icon, InteractiveElement.CellId, quantity));
        }
        public override void OnClicked(Player player)
        {
            Loot = false;

            this.Open = !Open;

            if (Open)
                MapInstance.UpdateElementVisual(InteractiveElement.CellId, InteractiveElement.LayerEnum, InteractiveElement.Value1, DefaultSprite.Type, false);
            else
                MapInstance.UpdateElementVisual(InteractiveElement.CellId, InteractiveElement.LayerEnum, DefaultSprite.VisualData, DefaultSprite.Type, false);

            if (!Loot && Open)
            {
                LootItem(player);
                Loot = true;
            }
        }

        public override void Update(long deltaTime)
        {

        }
    }
}
