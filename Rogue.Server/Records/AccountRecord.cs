using MonoFramework.Utils;
using Rogue.ORM.Attributes;
using Rogue.ORM.Interfaces;
using Rogue.Protocol.Enums;
using Rogue.Server.Auth;
using Rogue.Server.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Server.Records
{
    [Table("accounts")]
    public class AccountRecord : ITable
    {
        static Logger logger = new Logger();

        static UniqueIdProvider uniqueIdProvider;

        public static List<AccountRecord> Accounts = new List<AccountRecord>();

        [Primary]
        public int Id;

        public string Username;

        public string Password;

        public string CharacterName;

        public string Email;

        public bool Banned;

        public int Iron;

        public int Gold;

        public float LeaveRatio;

        public List<int> FriendList;

        [StartupInvoke(StartupInvokePriority.Eighth)]
        public static void Initialize()
        {
            int id = 0;

            if (Accounts.Count != 0)
            {
                id = Accounts.Last().Id + 1;
            }
            uniqueIdProvider = new UniqueIdProvider(id);

        }
        public AccountRecord(int id, string username, string password, string characterName, string email, bool banned, int iron, int gold,
            float leaveRation, List<int> friendList)
        {
            this.Id = id;
            this.Username = username;
            this.Password = password;
            this.CharacterName = characterName;
            this.Email = email;
            this.Banned = banned;
            this.Iron = iron;
            this.Gold = gold;
            this.LeaveRatio = leaveRation;
            this.FriendList = friendList;
        }
        public static AccountRecord GetAccountByUsername(string username)
        {
            return Accounts.FirstOrDefault(x => x.Username == username);
        }
        public static bool AccountExist(string username, string email)
        {
            return Accounts.Any(x => x.Username.ToLower() == username.ToLower() || x.Email.ToLower() == email.ToLower());
        }
        public static AccountCreationResultEnum CreateAccount(string username, string password, string email, string characterName)
        {
            if (username == string.Empty || password == string.Empty || email == string.Empty || characterName == string.Empty)
            {
                return AccountCreationResultEnum.INVALID_FIELDS;
            }
            if (username.ToLower() == password.ToLower())
            {
                return AccountCreationResultEnum.USERNAME_IS_PASSWORD;
            }
            if (AccountExist(username, email))
            {
                return AccountCreationResultEnum.USERNAME_OR_MAIL_EXIST;
            }
            try
            {
                AccountRecord account = new AccountRecord(uniqueIdProvider.GetNextId(), username, password, characterName, email, false, 0, 0, 0, new List<int>());
                account.AddInstantElement();
                return AccountCreationResultEnum.SUCCES;
            }
            catch (Exception ex)
            {
                logger.Write("Cannot create account (" + username + ") :" + ex);
                return AccountCreationResultEnum.UNDEFINED_ERROR;
            }
        }
    }
}
