using Microsoft.Xna.Framework;
using Rogue.Core.DesignPattern;
using Rogue.ORM.Attributes;
using Rogue.ORM.Interfaces;
using Rogue.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Server.Records
{
    [Table("entities")]
    public class EntityRecord : ITable
    {
        public static List<EntityRecord> Entities = new List<EntityRecord>();

        public string Name;

        public int Width;

        public int Height;

        public List<string> Animations;

        public string IdleAnimation;

        public string MovementAnimation;

        public float DefaultSpeed;

        public int DefaultLife;

        public EntityRecord(string name, int width, int height, List<string> animations, string idleAnimation, string movementAnimation,
            float defaultSpeed, int defaultLife)
        {
            this.Name = name;
            this.Width = width;
            this.Height = height;
            this.Animations = animations;
            this.IdleAnimation = idleAnimation;
            this.MovementAnimation = movementAnimation;
            this.DefaultSpeed = defaultSpeed;
            this.DefaultLife = defaultLife;
        }
        public Vector2 Center(Vector2 position)
        {
            return new Vector2(position.X + Width / 2, position.Y + Height / 2);
        }
        public static EntityRecord GetEntity(string name)
        {
            return Entities.FirstOrDefault(x => x.Name == name);
        }
        public Stats GetStats()
        {
            return new Stats(DefaultLife, DefaultSpeed);
        }
    }
}
