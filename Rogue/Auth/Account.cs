using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Auth
{
    public struct Account
    {
        public int Id
        {
            get;
            private set;
        }
        public string CharacterName
        {
            get;
            private set;
        }
        public string Email
        {
            get;
            private set;
        }

        public int Iron
        {
            get;
            private set;
        }

        public int Gold
        {
            get;
            private set;
        }

        public float LeaveRatio
        {
            get;
            private set;
        }

        public int[] FriendList
        {
            get;
            private set;
        }

        public Account(int id,string characterName, string email, int iron, int gold, float leaveRation, int[] friendList)
        {
            Id = id;
            CharacterName = characterName;
            Email = email;
            Iron = iron;
            Gold = gold;
            LeaveRatio = leaveRation;
            FriendList = friendList;
        }

        public bool IsFriendly(int accountId)
        {
            return FriendList.Contains(accountId);
        }
    }
}
