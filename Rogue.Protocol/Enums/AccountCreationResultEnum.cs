using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Protocol.Enums
{
    public enum AccountCreationResultEnum
    {
        UNDEFINED_ERROR = 0,
        SUCCES = 1,
        USERNAME_OR_MAIL_EXIST = 2,
        USERNAME_IS_PASSWORD = 3,
        INVALID_FIELDS = 4,
    }
}
