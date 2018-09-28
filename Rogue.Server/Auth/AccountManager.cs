using Rogue.Protocol.Enums;
using Rogue.Server.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoFramework.Utils;

namespace Rogue.Server.Auth
{
    public class AccountManager
    {
        private static Logger logger = new Logger();

        private static object locker = new object();

        public static AccountCreationResultEnum CreateAccount(string username, string password, string email, string charactername)
        {
            lock (locker)
            {
                AccountCreationResultEnum result = AccountRecord.CreateAccount(username, password, email, charactername);

                if (result == AccountCreationResultEnum.SUCCES)
                {
                    logger.Write("Account (" + username + ") created!");
                }

                return result;
            }
        }
    }
}
