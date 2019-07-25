using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rogue.Scenes;
using Rogue.Network;
using Rogue.Protocol.Enums;
using Rogue.Protocol.Messages.Client;
using Rogue.Protocol.Messages.Server;
using Rogue.Core.Scenes;

namespace Rogue.Frames
{
    public class HubFrame : ClientFrame
    {
        public string MapName
        {
            get;
            set;
        }
        public HubFrame(string mapName) : base("GameScene")
        {
            this.MapName = mapName;
        }

        public override FrameEnum FrameEnum => FrameEnum.HUB;

        public override ushort[] HandledMessages => new ushort[]
        {
            LoadFrameMessage.Id,
            GameEntitiesMessage.Id,
            ShowEntityMessage.Id,
            EntityDispositionMessage.Id,
            RemoveEntityMessage.Id,
            TeleportSameMapMessage.Id,
            DashMessage.Id,
            InflictDamageMessage.Id,
            AIMoveMessage.Id,
            AddLayerElementMessage.Id,
            RemoveLayerElementMessage.Id,
            InventoryAddItemMessage.Id,
            InventoryRemoveItemMessage.Id,
            InventoryUpdateQuantityMessage.Id,
            NotifyItemCooldownMessage.Id,
            ItemLootChestMessage.Id,
            HitscanHitMessage.Id,
            DefineEntityAuraMessage.Id,
            DefinePlayerWeaponMessage.Id,
            ProjectileCreateMessage.Id,
            RemoveProjectileMessage.Id,
        };


        public override void Enter()
        {
            base.Enter();
        }
        public void OnEntitiesReceived()
        {
            SceneManager.GetCurrentScene<MapSceneNet>().OnNetworkReady(MapName);
        }
        protected override void OnFrameLoaded()
        {
            base.OnFrameLoaded();
            ClientHost.Client.Send(new GameEntitiesRequestMessage());
        }
    }
}
