using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiteNetLib.Utils;
using System.Reflection;
using MonoFramework.Network.Protocol;

namespace Rogue.Protocol.Messages.Server
{
    public class AuthentificationSuccesMessage : Message
    {
        public const ushort Id = 2;

        public override ushort MessageId
        {
            get
            {
                return Id;
            }
        }

        public int accountId;

        public string characterName;

        public string email;

        public int iron;

        public int gold;

        public float leaveRatio;

        public int[] friendList;

        public AuthentificationSuccesMessage()
        {

        }
        public AuthentificationSuccesMessage(int accountId, string characterName, string email, int iron, int gold,
            float leaveRatio, int[] friendList)
        {
            this.accountId = accountId;
            this.characterName = characterName;
            this.email = email;
            this.iron = iron;
            this.gold = gold;
            this.leaveRatio = leaveRatio;
            this.friendList = friendList;
        }

        public override void Deserialize(LittleEndianReader reader)
        {
            this.accountId = reader.GetInt();
            this.characterName = reader.GetString();
            this.email = reader.GetString();
            this.iron = reader.GetInt();
            this.gold = reader.GetInt();

            leaveRatio = reader.GetFloat();

            friendList = new int[reader.GetInt()];

            for (int i = 0; i < friendList.Length; i++)
            {
                friendList[i] = reader.GetInt();
            }

        }
        public override void Serialize(LittleEndianWriter writer)
        {
            writer.Put(accountId);
            writer.Put(characterName);
            writer.Put(email);
            writer.Put(iron);
            writer.Put(gold);

            writer.Put(leaveRatio);

            writer.Put(friendList.Length);

            foreach (var friend in friendList)
            {
                writer.Put(friend);
            }

        }
    }
}
