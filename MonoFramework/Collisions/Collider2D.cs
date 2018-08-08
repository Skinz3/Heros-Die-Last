using Microsoft.Xna.Framework;
using MonoFramework.Objects;
using MonoFramework.Objects.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFramework.Collisions
{
    public abstract class Collider2D
    {
        protected PositionableObject GameObject
        {
            get;
            set;
        }
        private GMap Map
        {
            get;
            set;
        }
        public Rectangle HitBox
        {
            get
            {
                return CalculateHitBox(GameObject.Position);
            }
        }
        private GCell[] CurrentCells
        {
            get;
            set;
        }

        public GCell CurrentCell
        {
            get
            {
                return Map.GetCell(HitBox.Center.ToVector2());
            }
        }

        public abstract Rectangle CalculateHitBox(Vector2 position);

        public Collider2D(PositionableObject gameObject, GMap map)
        {
            this.GameObject = gameObject;
            this.Map = map;
            this.CurrentCells = new GCell[0];
        }

        public bool CanMove(Vector2 newPosition, DirectionEnum direction)
        {
            var newHitBox = CalculateHitBox(newPosition);

            CurrentCells = Map.GetCells(newHitBox);

            foreach (var cell in CurrentCells)
            {
                var nextCell = cell.GetNextCells(Map, direction, 1)[0];

                if (nextCell != null && nextCell.Rectangle.Intersects(newHitBox) && nextCell.Walkable == false)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
