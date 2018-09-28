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

        public ItemTypeEnum Type;

        public bool InstantUse;

        public ItemRecord(int id, string name, ItemTypeEnum type, bool instantUse)
        {
            this.Id = id;
            this.Name = name;
            this.Type = type;
            this.InstantUse = instantUse;
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
