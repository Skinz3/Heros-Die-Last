using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Rogue;
using Rogue.Animations;
using Rogue.Collisions;
using Rogue.Objects;
using Rogue.Objects.Entities;
using Rogue.Scenes;
using Rogue.Network;
using Rogue.Protocol.Enums;
using Rogue.Protocol.Messages.Client;
using Rogue.Protocol.Messages.Server;
using Rogue.Protocol.Types;
using Rogue.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoFramework.Collisions;
using MonoFramework;
using Rogue.Objects.UI;
using MonoFramework.Sprites;
using Rogue.World.Items;
using MonoFramework.Objects;
using MonoFramework.Scenes;

namespace Rogue.Objects
{
    public class Player : MovableEntity
    {
        public bool IsMainPlayer
        {
            get;
            private set;
        }
        public bool Aiming
        {
            get;
            set;
        }

        public override bool CanMove => IsMainPlayer && !Dashing && !Aiming;

        public override bool Controlable => IsMainPlayer;

        public override bool UseInterpolation => !IsMainPlayer;

        public Player(ProtocolPlayer protocolPlayer) : base(protocolPlayer)
        {
            this.IsMainPlayer = ClientHost.Client.Account.Id == Id;
        }
        public override void OnInitialize()
        {
            base.OnInitialize();
        }
        public override void OnInitializeComplete()
        {
            if (IsMainPlayer)
            {
                AddScript(new CameraControlScript());
                AddScript(new MainPlayerScript());
            }
            base.OnInitializeComplete();
        }

        public override void OnDraw(GameTime time)
        {
            base.OnDraw(time);
        }

        public override Collider2D CreateCollider()
        {
            return new WonderDotCollider(this);
        }
        public override void OnUpdate(GameTime time)
        {

        }
        public override void OnDispose()
        {

        }
    }
}
