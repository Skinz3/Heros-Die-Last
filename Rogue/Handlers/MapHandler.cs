﻿using Microsoft.Xna.Framework;
using Rogue.Core.Network.Protocol;
using Rogue.Objects.Entities;
using Rogue.Frames;
using Rogue.Network;
using Rogue.Objects;
using Rogue.Protocol.Messages.Client;
using Rogue.Protocol.Messages.Server;
using Rogue.Scripts;
using Rogue.World.Entities;
using Rogue.World.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rogue.Objects.UI;
using Rogue.Core.Scenes;
using Rogue.Core.DesignPattern;
using Rogue.Core.Objects;
using Rogue.Animations;
using Rogue.Protocol.Enums;
using Rogue.Core;
using Rogue.Core.Sprites;
using Rogue.World.Items;
using Rogue.Objects.Items;
using System.Threading;

namespace Rogue.Handlers
{
    class MapHandler
    {
        [MessageHandler]
        public static void HandleAIMoveMessage(AIMoveMessage message, RogueClient client)
        {
            var entity = client.Player?.MapInstance?.GetEntity<MovableEntity>(message.entityId);

            var target = SceneManager.GetCurrentScene<MapScene>().Map.GetCell(message.targetCellId);

            if (entity != null)
            {
                entity.GetScript<AIMovementScript>().Move(target);
            }
        }
        [MessageHandler]
        public static void HandleDashMessage(DashMessage message, RogueClient client)
        {
            var entity = client.Player?.MapInstance?.GetEntity<MovableEntity>(message.entityId);

            if (entity != null)
            {
                entity.Dash(message.speed, message.distance, message.direction, message.animation);
            }
        }
        [MessageHandler]
        public static void HandleTeleportSameMapMessage(TeleportSameMapMessage message, RogueClient client)
        {
            var entity = client.Player?.MapInstance?.GetEntity<MovableEntity>(message.entityId);

            if (entity != null)
            {
                entity.Position = message.position;
            }
        }
        [MessageHandler]
        public static void HandleItemLootChestMessage(ItemLootChestMessage message, RogueClient client)
        {
            var item = Item.CreateInstance(message.itemId, message.icon, message.quantity);
            var cell = SceneManager.GetCurrentScene<MapScene>().Map.GetCell<GCell>(message.cellId);
            SceneManager.CurrentScene.AddObject(new LootItem(item, cell, Color.White), LayerEnum.Third);
        }
        [MessageHandler]
        public static void HandleEntityDispositionMessage(EntityDispositionMessage message, RogueClient client)
        {
            var entity = client.Player?.MapInstance?.GetEntity<MovableEntity>(message.entityId);

            if (entity != null)
            {
                entity.OnPositionReceived(message.position, message.direction, message.mouseRotation);
            }
        }
        [MessageHandler]
        public static void HandleRemoveEntityMessage(RemoveEntityMessage message, RogueClient client)
        {
            client.Player.MapInstance?.RemoveEntity(message.entityId);
        }
        [MessageHandler]
        public static void HandleShowEntityMessage(ShowEntityMessage message, RogueClient client)
        {
            Entity entity = EntityManager.GetEntity(message.entity);
            entity.DefineMapInstance(client.Player.MapInstance);
            client.Player.MapInstance.AddEntity(entity);
        }
        [MessageHandler]
        public static void HandleGameEntitiesMessage(GameEntitiesMessage message, RogueClient client)
        {
            MapInstance instance = new MapInstance();

            foreach (var protocolEntity in message.entities)
            {
                Entity entity = EntityManager.GetEntity(protocolEntity);

                if (entity.Id == client.Account.Id)
                {
                    client.DefinePlayer((Player)entity);
                }

                entity.DefineMapInstance(instance);
                instance.AddEntity(entity, false);
            }

            client.Player.MapInstance.Start();

            client.GetFrame<HubFrame>().OnEntitiesReceived();

            client.Send(new GameEntityOKRequestMessage());
        }
    }
}
