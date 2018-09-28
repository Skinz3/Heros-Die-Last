using Microsoft.Xna.Framework;
using Rogue.Server.World.Entities;
using Rogue.Server.World.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Server.World
{
    public abstract class ServerObject
    {
        public Vector2 Position;

        public Point Size;

        public Vector2 Center
        {
            get
            {
                return Rectangle.Center.ToVector2();
            }
        }
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle(Position.ToPoint(), Size);
            }
        }

        public ServerObject(Vector2 position, Point size)
        {
            this.Position = position;
            this.Size = size;
        }
        public bool InMapInstance
        {
            get
            {
                return GetMapInstance() != null;
            }
        }
        public abstract MapInstance GetMapInstance();

        public abstract MapCell GetCell();

        public MapCell GetAdjacentFreeCell()
        {
            var currentCell = GetCell();

            foreach (var cell in currentCell.Adjacents)
            {
                if (cell.Walkable)
                {
                    if (GetMapInstance().GetEntities<Monster>().FirstOrDefault(x => x != this && x.GetCell() == cell) == null)
                    {
                        return (MapCell)cell;
                    }
                }
            }
            return null;
        }
        public float GetDistance(ServerObject other)
        {
            return (other.Position - this.Position).Length();
        }
    }
}
