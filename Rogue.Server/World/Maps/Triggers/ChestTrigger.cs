using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoFramework;
using MonoFramework.Utils;
using Rogue.Protocol.Messages.Server;
using Rogue.Server.Records;
using Rogue.Server.World.Entities;

namespace Rogue.Server.World.Maps.Triggers
{
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
        public ChestTrigger(MapInstance mapInstance, MapObjectRecord mapObject) : base(mapInstance, mapObject)
        {

        }

        private void LootItem(Player player)
        {
            var targetItem = ItemRecord.Items.Random();

            var quantity = new AsyncRandom().Next(1, 20);
            player.Inventory.AddItem(targetItem.Id, quantity);
            player.MapInstance.Send(new ItemLootChestMessage(targetItem.Id, MapObjectRecord.CellId, quantity));
        }
        public override void OnClicked(Player player)
        {
            Loot = false;

            this.Open = !Open;

            if (Open)
                MapInstance.UpdateMapObject(this.MapObjectRecord, MapObjectRecord.Value1);
            else
                MapInstance.UpdateMapObject(this.MapObjectRecord, MapObjectRecord.DefaultVisualData);

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
