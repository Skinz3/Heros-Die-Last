using Rogue.Core.Network.Protocol;
using Rogue.Network;
using Rogue.Objects;
using Rogue.Objects.Entities;
using Rogue.Protocol.Messages.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Handlers
{
    class CosmecticsHandler
    {
        [MessageHandler]
        public static void HandleDefinePlayerWeaponMessage(DefinePlayerWeaponMessage message, RogueClient client)
        {
            var entity = client.Player?.MapInstance?.GetEntity<Player>(message.targetId);
            entity.DefineWeapon(message.animationName);
        }
        [MessageHandler]
        public static void HandleDefineEntityAuraMessage(DefineEntityAuraMessage message, RogueClient client)
        {
            var entity = client.Player?.MapInstance?.GetEntity<MovableEntity>(message.targetId);
            entity.DefineAura(message.aura.Color, message.aura.Radius, message.aura.Sharpness);
        }
    }
}
