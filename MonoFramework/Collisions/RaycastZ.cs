using Microsoft.Xna.Framework;
using MonoFramework.Objects.Abstract;
using MonoFramework.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFramework.Collisions
{
    public class RaycastZ
    {
        private Point Position
        {
            get;
            set;
        }
        public RaycastZ(Point position)
        {
            this.Position = position;
        }

        public GameObject Cast(int size = 1)
        {
            Rectangle rect = new Rectangle(Position, new Point(size, size));

            foreach (var gameObject in SceneManager.CurrentScene.UIGameObjects.OfType<PositionableObject>())
            {
                if (gameObject.Rectangle.Intersects(rect))
                {
                    return gameObject;
                }
            }

            foreach (var list in SceneManager.CurrentScene.GameObjects.Values)
            {
                foreach (var gameObject in list.OfType<PositionableObject>())
                {
                    rect.Location = gameObject.TranslateToScenePosition(Position);

                    if (gameObject.Rectangle.Intersects(rect))
                    {
                        return gameObject;
                    }
                }

            }
            return null;
        }
    }
}
