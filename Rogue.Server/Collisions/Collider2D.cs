using Microsoft.Xna.Framework;
using MonoFramework;
using MonoFramework.Collisions;
using Rogue.Server.World;
using Rogue.Server.World.Entities;
using Rogue.Server.World.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Server.Collisions
{
    public abstract class Collider2D
    {
        public delegate void OnCellChangedDelegate(MapCell oldCell, MapCell currentCell);

        public event OnCellChangedDelegate OnCellChanged;

        protected ServerObject GameObject
        {
            get;
            set;
        }
        private MapGrid Map
        {
            get
            {
                return GameObject.GetMapInstance().Record.Grid;
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
        private MapCell[] CurrentCells
        {
            get;
            set;
        }

        private MapCell m_currentCell;

        public MapCell CurrentCell
        {
            get
            {
                if (m_currentCell == null)
                {
                    m_currentCell = (MapCell)Map.GetCell(MovementHitBox.Center.ToVector2());
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

        public void Update()
        {
            var oldCell = CurrentCell;
            CurrentCell = (MapCell)Map.GetCell(MovementHitBox.Center.ToVector2());

            if (oldCell != CurrentCell)
            {
                OnCellChanged?.Invoke(oldCell, CurrentCell);
            }
        }
        public Collider2D(ServerObject obj)
        {
            this.GameObject = obj;
            this.CurrentCells = new MapCell[0];
        }
        /// <summary>
        /// Temps d'execution :  inferieur a 0ms 
        /// Todo : Check les collisions avec les autres entitées
        /// :p
        public ServerObject CanMove(Vector2 newPosition, DirectionEnum direction, bool collidesEntity = true)
        {
            var newHitBox = CalculateMovementHitBox(newPosition);

            CurrentCells = Map.GetCells(newHitBox);

            foreach (var cell in CurrentCells)
            {
                List<MapCell> nextCells = new List<MapCell>();

                var nextCell = cell.GetNextCells(Map, direction, 1)[0];
                nextCells.Add((MapCell)nextCell);

                var flags = direction.GetFlags(); // Dat fucking semi isometric view :3

                if (flags.Count() > 1) // on a plus d'une direction (par exemple bas droite, dans ce cas on ajoute les cellules du bas et les cellules de droites)
                {
                    foreach (DirectionEnum dir in flags)
                    {
                        nextCells.Add((MapCell)cell.GetNextCells(Map, dir, 1)[0]);
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

        public abstract ServerObject CollideEntity(Rectangle futureHitBox);

        public ServerObject CollideEntity(Vector2 newPosition)
        {
            return CollideEntity(CalculateEntityHitBox(newPosition));
        }

    }
}
