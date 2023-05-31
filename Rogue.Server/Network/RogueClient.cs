using LiteNetLib;
using Rogue.Protocol.Enums;
using Rogue.Protocol.Messages.Server;
using Rogue.Server.Frames;
using Rogue.Server.Records;
using Rogue.Server.World.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rogue.Core.Network;
using Rogue.Core.Utils;
using Rogue.Core.Network.Protocol;
using Rogue.Core.DesignPattern;

namespace Rogue.Server.Network
{
    public class RogueClient : Client
    {
        Logger logger = new Logger();


        public bool IsLogged => Account != null;

        public bool IsCharacterDefined => Player != null;

        public AccountRecord Account
        {
            get;
            private set;
        }
        public Player Player
        {
            get;
            private set;
        }

        public RogueClient(Frame baseFrame) : base(baseFrame)
        {

        }
        public RogueClient(NetPeer ip, Frame baseFrame) : base(ip, baseFrame)
        {

        }

        public override void LoadFrame(Frame frame)
        {
            ((ServerFrame)frame).Load(this);
            base.LoadFrame(frame);
        }
        public bool IsInFrame(FrameEnum frame)
        {
            return this.GetFrame<Frames.ServerFrame>().FrameEnum == frame;
        }

        public void RemoveIdentity()
        {
            DisposePlayer();
            Dispose();
        }
        private void DisposePlayer()
        {
            if (IsCharacterDefined)
            {
                Player.LeaveMapInstance();
                Player = null;
            }
        }
        [InDeveloppement(InDeveloppementState.THINK_ABOUT_IT, "Really important part of network")]
        protected override bool OnMessageReceived(Message message)
        {
            if (Player != null && Player.MapInstance != null)
            {
                Player.MapInstance.Sync(new Action(() => // Sync network with server Clock
                {
                    base.OnMessageReceived(message);

                }));

                return true; // ... ? stop thread, waiting update() ?
            }
            else
            {
                return base.OnMessageReceived(message);
            }

        }
        public override void OnDataArrival(byte[] buffer)
        {
            base.OnDataArrival(buffer);
        }
        protected override void OnDataReceived(byte[] buffer)
        {

        }

        public override void OnMessageSended(Message message)
        {

        }

        public void DefineAccount(AccountRecord account)
        {
            this.Account = account;
        }
        public void DefinePlayer(Player player)
        {
            this.Player = player;
        }

        public void OpenMenu()
        {
            this.LoadFrame(new MenuFrame("MenuScene"));
        }
    }
}
