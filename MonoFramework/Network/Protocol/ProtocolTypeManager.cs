using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MonoFramework.Network.Protocol
{
    public static class ProtocolTypeManager
    {
        [Serializable]
        public class ProtocolTypeNotFoundException : Exception
        {
            public ProtocolTypeNotFoundException()
            {
            }
            public ProtocolTypeNotFoundException(string message)
                : base(message)
            {
            }
            public ProtocolTypeNotFoundException(string message, Exception inner)
                : base(message, inner)
            {
            }
            protected ProtocolTypeNotFoundException(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
            }
        }
        private static readonly Dictionary<short, Type> m_types = new Dictionary<short, Type>(200);
        private static readonly Dictionary<short, Func<object>> m_typesConstructors = new Dictionary<short, Func<object>>(200);

        public static void Initialize(Assembly assembly)
        {
            foreach (var type in assembly.GetTypes().Where(x => !x.IsSubclassOf(typeof(Message))))
            {
                FieldInfo field = type.GetField("Id");
                if (field != null)
                {
                    short key = (short)field.GetValue(type);
                    ProtocolTypeManager.m_types.Add(key, type);
                    ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
                    if (constructor == null)
                    {
                        throw new Exception(string.Format("'{0}' doesn't implemented a parameterless constructor", type));
                    }
                    ProtocolTypeManager.m_typesConstructors.Add(key, constructor.CreateDelegate<Func<object>>());
                }
            }
        }
        public static T GetInstance<T>(short id) where T : class
        {
            if (!ProtocolTypeManager.m_types.ContainsKey(id))
            {
                throw new ProtocolTypeManager.ProtocolTypeNotFoundException(string.Format("Type <id:{0}> doesn't exist", id));
            }
            T instance = ProtocolTypeManager.m_typesConstructors[id]() as T;

            return instance;
        }
    }
}
