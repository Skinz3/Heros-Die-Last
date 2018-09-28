using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoFramework;
using MonoFramework.Network.Protocol;
using MonoFramework.Objects;
using MonoFramework.Scenes;
using MonoFramework.Sprites;
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
    class MapObjectsHandler
    {
        [MessageHandler]
        public static void HandleMapObjectsMessage(MapInformationMessage message, RogueClient client)
        {
            var map = SceneManager.GetCurrentScene<MapScene>().Map;

            foreach (var obj in message.mapObjects)
            {
                ICellElement cellElement = MapObjectManager.GetCellElement(obj);
                var cell = map.GetCell<GCell>(obj.CellId);
                cell.Walkable = obj.Walkable;
                cell.AddSprite(cellElement, obj.LayerEnum);
            }

            foreach (var light in message.mapLights)
            {
                var cell = map.GetCell<GCell>(light.CellId);
                cell.SetLight(light.Color, light.Radius, light.Sharpness);
            }
        }
        [MessageHandler]
        public static void HandleAddMapObjectMessage(AddMapObjectMessage message, RogueClient client)
        {
            var cell = SceneManager.GetCurrentScene<MapScene>().Map.GetCell<GCell>(message.mapObject.CellId);
            bool appear = !cell.Sprites.ContainsKey(message.mapObject.LayerEnum);
            ICellElement cellElement = MapObjectManager.GetCellElement(message.mapObject);
            cell.Walkable = message.mapObject.Walkable;
            cell.AddSprite(cellElement, message.mapObject.LayerEnum);
            if (appear)
                cell.AddScript(new MOFadeInScript(message.mapObject.LayerEnum));
        }



        [MessageHandler]
        public static void HandleRemoveMapObjectMessage(RemoveMapObjectMessage message, RogueClient client)
        {
            var scene = SceneManager.GetCurrentScene<MapScene>();
            var cell = scene.Map.GetCell<GCell>(message.cellId);
            cell.Walkable = scene.MapTemplate.GetCellTemplate(cell.Id).Walkable;
            cell.AddScript(new MOFadeOutScript(message.layer));
        }
    }
}
