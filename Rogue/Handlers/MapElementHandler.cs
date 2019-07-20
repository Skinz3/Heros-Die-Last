using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rogue.Core;
using Rogue.Core.Network.Protocol;
using Rogue.Core.Objects;
using Rogue.Core.Scenes;
using Rogue.Core.Sprites;
using Rogue.Animations;
using Rogue.Network;
using Rogue.Protocol.Enums;
using Rogue.Protocol.Messages.Server;
using Rogue.Scenes;
using Rogue.Scripts;
using Rogue.World.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Handlers
{
    class MapElementHandler
    { // todo =>>>>
        [MessageHandler]
        public static void HandleAddMapObjectMessage(AddLayerElementMessage message, RogueClient client)
        {
            var cell = SceneManager.GetCurrentScene<MapScene>().Map.GetCell<GCell>(message.mapObject.CellId);
            bool appear = !cell.Sprites.ContainsKey(message.mapObject.LayerEnum);

            cell.Walkable = message.mapObject.Walkable;

            var sprite = SpriteManager.GetSprite(message.mapObject.VisualData);

            cell.SetSprite(sprite, message.mapObject.LayerEnum);

            if (appear)
                cell.AddScript(new MOFadeInScript(message.mapObject.LayerEnum));
        }



        [MessageHandler]
        public static void HandleRemoveMapObjectMessage(RemoveLayerElementMessage message, RogueClient client)
        {
            var scene = SceneManager.GetCurrentScene<MapScene>();
            var cell = scene.Map.GetCell<GCell>(message.cellId);
            cell.Walkable = message.IsCellWalkable;
            cell.AddScript(new MOFadeOutScript(message.layer));
        }
    }
}
