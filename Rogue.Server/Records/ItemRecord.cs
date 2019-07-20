using Rogue.ORM.Attributes;
using Rogue.ORM.Interfaces;
using Rogue.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Server.Records
{
    [Table("items")]
    public class ItemRecord : ITable
    {
        public static List<ItemRecord> Items = new List<ItemRecord>();

        public int Id;

        public string Name;

        public string Icon;

        public ItemTypeEnum Type;

        public bool InstantUse;

        public float Cooldown;

        public ItemRecord(int id, string name, string icon, ItemTypeEnum type, bool instantUse, float cooldown)
        {
            this.Id = id;
            this.Name = name;
            this.Icon = icon;
            this.Type = type;
            this.InstantUse = instantUse;
            this.Cooldown = cooldown;
        }

        public static ItemRecord GetItem(int itemId)
        {
            return Items.FirstOrDefault(x => x.Id == itemId);
        }
        public static ItemRecord GetItem(string name)
        {
            return Items.FirstOrDefault(x => x.Name == name);
        }
    }
}
