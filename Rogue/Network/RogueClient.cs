using Rogue.Network;
using MonoFramework.Network.Protocol;
using Rogue.Auth;
using Rogue.Frames;
using Rogue.Objects;
using Rogue.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoFramework.Network;
using Rogue.Objects.UI;

namespace Rogue.Network
{
    public class RogueClient : Client
    {
        public Account Account
        {
            get;
            private set;
        }
        public Player Player
        {
            get;
            private set;
        }
        public Inventory Inventory
        {
            get;
            private set;
        }
        public RogueClient(Frame baseFrame) : base(baseFrame)
        {
            this.Inventory = new Inventory();
        }

        public bool IsInFrame(FrameEnum frame)
        {
            return this.GetFrame<ClientFrame>().FrameEnum == frame;
        }

        public override void OnDataArrival(byte[] buffer)
        {
            base.OnDataArrival(buffer);
        }


        public override void OnMessageSended(Message message)
        {

        }

        protected override void OnDataReceived(byte[] buffer)
        {

        }
        public void DefineAccount(Account account)
        {
            this.Account = account;
        }
        public void DefinePlayer(Player player)
        {
            this.Player = player;
        }
    }
}
