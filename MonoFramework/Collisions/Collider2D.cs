using Microsoft.Xna.Framework;
using Rogue.Core.Objects;
using Rogue.Core.Objects.Abstract;
using Rogue.Core.Scenes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Core.Collisions
{
    public abstract class Collider2D
    {
        public delegate void OnCellChangedDelegate(GCell oldCell, GCell currentCell);

        public event OnCellChangedDelegate OnCellChanged;

        protected PositionableObject GameObject
        {
            get;
            set;
        }
        private GMap Map
        {
            get
            {
                return SceneManager.GetCurrentScene<MapScene>().Map;
            }
        }
        public Rectangle MovementHitBox
        {
            get
            {
                return CalculateMovementHitBox(GameObject.Position);
            }
        }
        public Rectangle EntityHitBox
        {
            get
            {
                return CalculateEntityHitBox(GameObject.Position);
            }
        }
        public GCell[] CurrentCells
        {
            get;
            private set;
        }

        private GCell m_currentCell;

        public GCell CurrentCell
        {
            get
            {
                if (m_currentCell == null)
                {
                    m_currentCell = (GCell)Map.GetCell(MovementHitBox.Center.ToVector2());
                }
                return m_currentCell;
            }
            private set
            {
                m_currentCell = value;
            }
        }

        public abstract Rectangle CalculateMovementHitBox(Vector2 position);

        public abstract Rectangle CalculateEntityHitBox(Vector2 position);

        public Collider2D(PositionableObject gameObject)
        {
            this.GameObject = gameObject;
            this.CurrentCells = new GCell[0];
        }
        public void Update()
        {
            var oldCell = CurrentCell;
            CurrentCell = (GCell)Map.GetCell(MovementHitBox.Center.ToVector2());

            if (oldCell != CurrentCell)
            {
                OnCellChanged?.Invoke(oldCell, CurrentCell);
            }
        }
        /// <summary>
        /// Temps d'execution :  inferieur a 0ms 
        /// Todo : Check les collisions avec les autres entitées
        /// :p
        public GameObject CanMove(Vector2 newPosition, DirectionEnum direction, bool collidesEntity = true)
        {

            var newHitBox = CalculateMovementHitBox(newPosition);

            CurrentCells = (GCell[])Map.GetCells(newHitBox);

            foreach (var cell in CurrentCells)
            {
                List<GCell> nextCells = new List<GCell>();

                var nextCell = (GCell)cell.GetNextCells(Map, direction, 1)[0];
                nextCells.Add(nextCell);

                var flags = direction.GetFlags(); // Dat fucking semi isometric view :3

                if (flags.Count() > 1) // on a plus d'une direction (par exemple bas droite, dans ce cas on ajoute les cellules du bas et les cellules de droites)
                {
                    foreach (DirectionEnum dir in flags)
                    {
                        nextCells.Add((GCell)cell.GetNextCells(Map, dir, 1)[0]);
                    }
                }

                foreach (var next in nextCells)
                {
                    if (next != null && next.Rectangle.Intersects(newHitBox) && next.Walkable == false)
                    {
                        return next;
                    }
                }
            }


            return collidesEntity ? CollideEntity(newHitBox) : null;
        }

        public abstract GameObject CollideEntity(Rectangle futureHitBox);

        public GameObject CollideEntity(Vector2 newPosition)
        {
            return CollideEntity(CalculateEntityHitBox(newPosition));
        }

    }
}
