using Microsoft.Xna.Framework;
using MonoFramework.Objects;
using MonoFramework.Objects.Abstract;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public Rectangle MovementHitBox
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
                return Map.GetCell(MovementHitBox.Center.ToVector2());
            }
        }

        public abstract Rectangle CalculateHitBox(Vector2 position);

        public Collider2D(PositionableObject gameObject, GMap map)
        {
            this.GameObject = gameObject;
            this.Map = map;
            this.CurrentCells = new GCell[0];
        }
        /// <summary>
        /// Temps d'execution :  inferieur a 0ms 
        /// Todo : Check les collisions avec les autres entitées
        /// :p
        public bool CanMove(Vector2 newPosition, DirectionEnum direction)
        {
            var newHitBox = CalculateHitBox(newPosition);

            CurrentCells = Map.GetCells(newHitBox);

            foreach (var cell in CurrentCells)
            {
                List<GCell> nextCells = new List<GCell>();

                var nextCell = cell.GetNextCells(Map, direction, 1)[0];
                nextCells.Add(nextCell);

                var flags = direction.GetFlags(); // Dat fucking semi isometric view :3

                if (flags.Count() > 1) // on a plus d'une direction (par exemple bas droite, dans ce cas on ajoute les cellules du bas et les cellules de droites)
                {
                    foreach (DirectionEnum dir in flags)
                    {
                        nextCells.Add(cell.GetNextCells(Map, dir, 1)[0]);
                    }
                }

                foreach (var next in nextCells)
                {
                    if (next != null && next.Rectangle.Intersects(newHitBox) && next.Walkable == false)
                    {
                        return false;
                    }
                }

            }
            return true;
        }

    }
}
