using MonoFramework;
using MonoFramework.Lightning;
using MonoFramework.Objects;
using MonoFramework.Sprites;
using Rogue.Animations;
using Rogue.Protocol.Enums;
using Rogue.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.World.Maps
{
    class MapObjectManager
    {
        public static ICellElement GetCellElement(ProtocolMapObject mapObject)
        {
            if (mapObject.Type == MapObjectType.Sprite)
            {
                return SpriteManager.GetSprite(mapObject.VisualData);
            }
            else if (mapObject.Type == MapObjectType.Animation)
            {
                return AnimationManager.GetAnimation(mapObject.VisualData.ParseEnum<AnimationEnum>());
            }
            return null;
        }
    }
}
