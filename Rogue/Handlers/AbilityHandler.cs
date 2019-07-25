using Microsoft.Xna.Framework;
using Rogue.Core.DesignPattern;
using Rogue.Core.Network.Protocol;
using Rogue.Core.Objects;
using Rogue.Core.Scenes;
using Rogue.Network;
using Rogue.Objects;
using Rogue.Objects.Entities;
using Rogue.Objects.Items;
using Rogue.Protocol.Messages.Server;
using Rogue.World.Projectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Handlers
{
    /// <summary>
    /// todo => projectile should be a type in protocol same as Entity
    /// Fix this startPos weird story
    /// test it with network, synchronization problems should happen ?
    /// revoir la disposition des projectiles
    /// manière dont gérer les effets des projectiles.
    /// 
    /// </summary>
    class AbilityHandler
    {
        [MessageHandler]
        public static void HandleRemoveProjectileMessage(RemoveProjectileMessage message,RogueClient client)
        {
            client.Player.MapInstance.RemoveProjectile(message.id);
        }
        [InDeveloppement]
        [MessageHandler]
        public static void HandleProjectileCreateMessage(ProjectileCreateMessage message, RogueClient client)
        {
            var owner = client.Player.MapInstance.GetEntity<MovableEntity>(message.ownerId);

            var sPosition = message.startPosition;

            sPosition = new Vector2(sPosition.X - message.size / 2, sPosition.Y - message.size / 2);

            ReflectionProjectile projectile = new ReflectionProjectile(message.projectileId, sPosition, message.size, message.speed, Color.White, message.direction, owner, message.animationName);

            projectile.DefineAura(new Color(Color.CornflowerBlue, 120), 80f, 0.05f);

            client.Player.MapInstance.AddProjectile(projectile);
        }

        [MessageHandler]
        public static void HandleHitscanHitMessage(HitscanHitMessage message, RogueClient client)
        {

            var entity = client.Player?.MapInstance?.GetEntity<Player>(message.sourceId);
            entity.Animator.CurrentAnimation = "item103";
            var dir = (message.targetPoint - entity.Center);
            dir.Normalize();
            var target = entity.Center + dir * 1000;
            var line = new HitScanShot(entity.Center, target, Color.LightBlue * 0.8f, 3f);
            line.Initialize();
            SceneManager.CurrentScene.AddObject(line, LayerEnum.First);
        }
        [InDeveloppement]
        [MessageHandler]
        public static void HandleInflictDamageMessage(InflictDamageMessage message, RogueClient client)
        {
            var entity = client.Player?.MapInstance?.GetEntity<MovableEntity>(message.targetId);

            if (entity != null)
            {
                entity.InflictDamage(client.Player, message.amount);
            }
        }

    }
}
