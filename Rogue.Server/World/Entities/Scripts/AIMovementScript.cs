using Microsoft.Xna.Framework;
using Rogue.Core.Collisions;
using Rogue.Core.DesignPattern;
using Rogue.Core.Geometry;
using Rogue.Core.IO.Maps;
using Rogue.Core.Objects;
using Rogue.Core.Pathfinding;
using Rogue.Protocol.Messages.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Server.World.Entities.Scripts
{
    public class AIMovementScript : IScript
    {
        public ServerObject Target
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
                return Path == null ? null : Path.FirstOrDefault();
                //  else
                //  if (Path.Count > 1) return Path[1]; else return null;
            }
        }

        private AStar AStar
        {
            get;
            set;
        }

        private void PutEntities()
        {
            AStar.PutEntities(Entity.MapInstance.GetEntities<Monster>().Where(x => x != Entity).Select(x => x.GetCell().Id).ToArray());
        }

        public bool Moving
        {
            get
            {
                return Target != null;
            }
        }

        public void Initialize(Entity entity)
        {
            Entity = (MovableEntity)entity;
        }

        public void Update(long deltaTime)
        {
            if (Target == null)
            {
                return;
            }

            /* if (Target.InMapInstance == false)
             {
                 StopMove();
                 return;
             } */
            UpdatePosition(deltaTime);
        }
        private void OnReach()
        {
             
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

                var dir = direction.GetDirection();

                Entity.Direction = dir;

                var newPosition = Entity.Position + direction * Speed;

                Entity.Position = newPosition;

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

            }


        }




        private void CalculateNextCell()
        {
            var targetCell = Target.GetCell();

            if (targetCell == null || Target.GetMapInstance() != Entity.MapInstance)
            {
                StopMove();
                return;
            }
            AStar = new AStar(Entity.MapInstance.Record.Grid, Entity.GetCell().Id);
            PutEntities();
            Path = AStar.FindPath(targetCell.Id).ToList();

            if (NextCell != null) // Target is not ICell (we can send once)
            {
                Entity.MapInstance.Send(new AIMoveMessage(Entity.Id, Entity.Position, NextCell.Id));
            }

        }

        public void Move(ServerObject target)
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
