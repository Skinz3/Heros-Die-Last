using Microsoft.Xna.Framework;
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

        [Xml]
        public StateAnimations[] Animations;

        public EntityRecord(string name, int width, int height, StateAnimations[] animations)
        {
            this.Name = name;
            this.Width = width;
            this.Height = height;
            this.Animations = animations;
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
            if (Name == "Default")
            {
                return new Stats(8500, 3f);
            }
            return new Stats(5500, 1.5f);
        }
    }
}
