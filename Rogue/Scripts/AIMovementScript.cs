using Microsoft.Xna.Framework;
using Rogue.Core.Collisions;
using Rogue.Core.Geometry;
using Rogue.Core.Objects;
using Rogue.Core.Objects.Abstract;
using Rogue.Core.Pathfinding;
using Rogue.Core.Scenes;
using Rogue.Objects;
using Rogue.Objects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Scripts
{
    public class AIMovementScript : IScript
    {
        public ICell Target
        {
            get;
            private set;
        }
        private MovableEntity Entity
        {
            get;
            set;
        }
        private float Speed
        {
            get
            {
                return Entity.Stats.Speed;
            }
        }
        private List<ICell> Path
        {
            get;
            set;
        }
        private ICell NextCell
        {
            get
            {
                // if (Speed < 10)
                return Path.FirstOrDefault();
                //  else
                //  if (Path.Count > 1) return Path[1]; else return null;
            }
        }

        private AStar AStar
        {
            get;
            set;
        }



        public bool Moving
        {
            get
            {
                return Target != null;
            }
        }

        public void Initialize(GameObject entity)
        {
            Entity = (MovableEntity)entity;
        }


        public void Update(GameTime deltaTime)
        {
            if (Target == null)
            {
                return;
            }

            UpdatePosition(deltaTime.ElapsedGameTime.Milliseconds);
        }
        private void OnReach()
        {

        }

        private void PutEntities()
        {
            AStar.PutEntities(Entity.MapInstance.GetEntities<Monster>().Where(x => x != Entity).Select(x => x.GetCell().Id).ToArray());
        }
        private void UpdatePosition(long deltaTime)
        {
            if (NextCell == null)
            {
                CalculateNextCell();

                if (NextCell == null)
                {
                    OnReach();
                    return;
                }
            }

            var nextCellCenter = NextCell.Rectangle.Center.ToVector2();

            var entityCenter = Entity.Center;

            if (nextCellCenter == entityCenter)
            {
                CalculateNextCell();
            }
            var direction = (nextCellCenter - entityCenter);

            if (direction != Vector2.Zero)
            {
                direction.Normalize();

                var newPosition = Entity.Position + direction * Speed;

                var dir = direction.GetDirection();

                Entity.Position = newPosition;
                Entity.MovementEngine.Direction = dir;

                if (dir == DirectionEnum.Right)
                {
                    if (entityCenter.X + Speed >= nextCellCenter.X)
                    {
                        CalculateNextCell();
                    }
                }
                else if (dir == DirectionEnum.Up)
                {
                    if (entityCenter.Y - Speed <= nextCellCenter.Y)
                    {
                        CalculateNextCell();
                    }
                }
                else if (dir == DirectionEnum.Down)
                {
                    if (entityCenter.Y - Speed >= nextCellCenter.Y)
                    {
                        CalculateNextCell();
                    }
                }
                else if (dir == DirectionEnum.Left)
                {
                    if (entityCenter.X - Speed <= nextCellCenter.X)
                    {
                        CalculateNextCell();
                    }
                }

                if (dir == DirectionEnum.None)
                {
                    Entity.Animator.SetIdleAnimation();
                }
                else
                {
                    Entity.Animator.SetMovementAnimation();
                }


            }


        }

        private void CalculateNextCell()
        {
            AStar = new AStar(SceneManager.GetCurrentScene<MapScene>().Map, Entity.GetCell().Id);
            PutEntities();
            Path = AStar.FindPath(Target.Id).ToList();

        }

        public void Move(ICell target)
        {
            this.Target = target;
            CalculateNextCell();

        }
        public void StopMove()
        {
            this.Target = null;
            Path = null;
        }

        public void Dispose()
        {
            StopMove();
        }

        public void OnRemove()
        {
          
        }
    }
}
