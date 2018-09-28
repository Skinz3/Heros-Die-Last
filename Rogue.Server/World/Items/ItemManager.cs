using MonoFramework.Utils;
using Rogue.Server.Records;
using Rogue.Server.Utils;
using Rogue.Server.World.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Server.World.Items
{
    public class ItemManager
    {
        static Logger logger = new Logger();

        public static Dictionary<int, Type> Classes = new Dictionary<int, Type>();

        [StartupInvoke("Item Handlers", StartupInvokePriority.Eighth)]
        public static void Initialize()
        {
            foreach (var type in Program.SERVER_ASSEMBLY.GetTypes())
            {
                var attribute = type.GetCustomAttribute<ItemHandler>();

                if (attribute != null)
                {
                    Classes.Add(attribute.itemId, type);
                }
            }
        }
        public static Item GetItemInstance(ItemRecord record, int quantity)
        {
            if (!Classes.ContainsKey(record.Id))
            {
                logger.Write(record.Name + " is not handled", MessageState.WARNING);
                return null;
            }
            return (Item)Activator.CreateInstance(Classes[record.Id], new object[] { record, quantity });
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class ItemHandler : Attribute
    {
        public int itemId;

        public ItemHandler(int itemId)
        {
            this.itemId = itemId;
        }
    }
}
