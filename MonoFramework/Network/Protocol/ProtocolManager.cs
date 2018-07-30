using LiteNetLib.Utils;
using MonoFramework.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MonoFramework.Network.Protocol
{
    public class ProtocolManager
    {
        public const string ID_MESSAGE_FIELD_NAME = "Id";

        private static readonly Type[] HandlerMethodParameterTypes = new Type[] { typeof(Message), typeof(Client) };

        private static readonly Dictionary<ushort, Delegate> Handlers = new Dictionary<ushort, Delegate>();

        private static readonly Dictionary<ushort, Type> Messages = new Dictionary<ushort, Type>();

        private static readonly Dictionary<ushort, Func<Message>> Constructors = new Dictionary<ushort, Func<Message>>();

        public static Logger logger = new Logger();

        public static bool ShowProtocolMessage;

        public static void Initialize(Assembly messagesAssembly, Assembly handlersAssembly, bool showProtocolMessages)
        {
            ShowProtocolMessage = showProtocolMessages;
            foreach (var type in messagesAssembly.GetTypes().Where(x => x.IsSubclassOf(typeof(Message))))
            {
                FieldInfo field = type.GetField(ID_MESSAGE_FIELD_NAME);

                if (field != null)
                {
                    ushort num = (ushort)field.GetValue(type);
                    if (Messages.ContainsKey(num))
                    {
                        throw new AmbiguousMatchException(string.Format("MessageReceiver() => {0} item is already in the dictionary, old type is : {1}, new type is  {2}",
                            num, Messages[num], type));
                    }

                    Messages.Add(num, type);

                    ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
                    if (constructor == null)
                    {
                        throw new Exception(string.Format("'{0}' doesn't implemented a parameterless constructor", type));
                    }
                    Constructors.Add(num, constructor.CreateDelegate<Func<Message>>());
                }
            }

            foreach (var item in handlersAssembly.GetTypes())
            {
                foreach (var subItem in item.GetMethods())
                {
                    var attribute = subItem.GetCustomAttribute(typeof(MessageHandlerAttribute));
                    if (attribute != null)
                    {
                        ParameterInfo[] parameters = subItem.GetParameters();
                        Type methodParameters = subItem.GetParameters()[0].ParameterType;
                        if (methodParameters.BaseType != null)
                        {
                            try
                            {
                                Delegate target = subItem.CreateDelegate(HandlerMethodParameterTypes);
                                FieldInfo field = methodParameters.GetField("Id");
                                Handlers.Add((ushort)field.GetValue(null), target);
                            }
                            catch
                            {
                                throw new Exception("Cannot register " + subItem.Name + " has message handler...");
                            }

                        }
                    }

                }
            }

            ProtocolTypeManager.Initialize(messagesAssembly);

            if (ShowProtocolMessage)
                logger.Write(Messages.Count + " Message(s) Loaded | " + Handlers.Count + " Handler(s) Loaded");
        }
        /// <summary>
        /// Unpack message
        /// </summary>
        /// <param name="id">Id of the message</param>
        /// <param name="reader">Reader with the message datas</param>
        /// <returns></returns>
        private static Message ConstructMessage(ushort id, NetDataReader reader)
        {
            if (!Messages.ContainsKey(id))
            {
                return null;
            }
            Message message = Constructors[id]();
            if (message == null)
            {
                return null;
            }
            message.Unpack(reader);
            return message;
        }
        public static Dictionary<ushort, Delegate> GetHandlers(ushort[] ids)
        {
            return Handlers.Where(x => ids.Contains(x.Key)).ToDictionary(x => x.Key, y => y.Value);
        }
        /// <summary>
        /// Build a messagePart and call the ConstructMessage(); method.
        /// </summary>
        /// <param name="buffer">data received</param>
        /// <returns>Message of your protocol, builted</returns>
        public static Message BuildMessage(byte[] buffer)
        {
            var reader = new NetDataReader(buffer);
            ushort messageId = (ushort)reader.GetShort();

            Message message;
            try
            {
                message = ConstructMessage(messageId, reader);
                return message;
            }
            catch (Exception ex)
            {
                logger.Write("Error while building Message :" + ex.Message, MessageState.WARNING);
                return null;
            }
            finally
            {
                reader.Clear();
            }


        }
    }
}
