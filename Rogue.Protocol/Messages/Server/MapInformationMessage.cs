using LiteNetLib.Utils;
using MonoFramework.Network.Protocol;
using Rogue.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Protocol.Messages.Server
{
    public class MapInformationMessage : Message
    {
        public const ushort Id = 38;

        public override ushort MessageId
        {
            get
            {
                return Id;
            }
        }


        public ProtocolMapObject[] mapObjects;

        public ProtocolMapLight[] mapLights;

        public MapInformationMessage()
        {

        }
        public MapInformationMessage(ProtocolMapObject[] mapObjects, ProtocolMapLight[] mapLights)
        {
            this.mapObjects = mapObjects;
            this.mapLights = mapLights;
        }

        public override void Deserialize(LittleEndianReader reader)
        {
            this.mapObjects = new ProtocolMapObject[reader.GetInt()];

            for (int i = 0; i < mapObjects.Length; i++)
            {
                mapObjects[i] = new ProtocolMapObject();
                mapObjects[i].Deserialize(reader);
            }

            this.mapLights = new ProtocolMapLight[reader.GetInt()];

            for (int i = 0; i < mapLights.Length; i++)
            {
                mapLights[i] = new ProtocolMapLight();
                mapLights[i].Deserialize(reader);
            }
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.Put(mapObjects.Length);

            foreach (var mapObject in mapObjects)
            {
                mapObject.Serialize(writer);
            }

            writer.Put(mapLights.Length);

            foreach (var mapLight in mapLights)
            {
                mapLight.Serialize(writer);
            }
        }
    }
}
