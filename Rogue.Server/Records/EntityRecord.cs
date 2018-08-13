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
    [Table("players")]
    public class EntityRecord : ITable
    {
        public static List<EntityRecord> Players = new List<EntityRecord>();

        public string Name;

        public int Width;

        public int Height;

        [Xml]
        public DirectionalAnimation[] Animations;

        public EntityRecord(string name,int width,int height, DirectionalAnimation[] animations)
        {
            this.Name = name;
            this.Width = width;
            this.Height = height;
            this.Animations = animations;
        }
    }
}
