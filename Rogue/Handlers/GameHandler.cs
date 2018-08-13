using Microsoft.Xna.Framework;
using MonoFramework.Geometry;
using MonoFramework.Network.Protocol;
using MonoFramework.Objects.Entities;
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

namespace Rogue.Handlers
{
    class GameHandler
    {
        [MessageHandler]
        public static void HandleEntityDispositionMessage(EntityDispositionMessage message, RogueClient client)
        {
            var entity = client.Player?.MapInstance?.GetEntity<Player>(message.entityId);

            if (entity != null)
            {
                entity.OnPositionReceived(message.position, message.direction);

            }
        }
        [MessageHandler]
        public static void HandleRemoveEntityMessage(RemoveEntityMessage message, RogueClient client)
        {
            client.Player.MapInstance.RemoveEntity(message.entityId);
        }
        [MessageHandler]
        public static void HandleShowEntityMessage(ShowEntityMessage message, RogueClient client)
        {
            Entity entity = EntityManager.GetEntity(message.entity);
            client.Player.MapInstance.AddEntity(entity);
        }
        [MessageHandler]
        public static void HandleGameEntitiesMessage(GameEntitiesMessage message, RogueClient client)
        {
            EntityInterpolationScript.UseInterpolation = message.useInterpolation;
            MainPlayerScript.PositionUpdateFrameCount = message.positionUpdateFrameCount;

            MapInstance instance = new MapInstance();
            foreach (var protocolEntity in message.entities)
            {
                Entity entity = EntityManager.GetEntity(protocolEntity);
                if (protocolEntity.EntityId == client.Account.Id)
                {
                    client.DefinePlayer((Player)entity);
                    client.Player.DefineMapInstance(instance);
                }

                instance.AddEntity(entity, false);
            }

            client.Player.MapInstance.Start();

            client.Send(new GameEntityOKRequestMessage());
        }
    }
}
