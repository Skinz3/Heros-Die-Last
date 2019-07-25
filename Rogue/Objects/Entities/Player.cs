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
using Rogue.Core.Collisions;
using Rogue.Core;
using Rogue.Objects.UI;
using Rogue.Core.Sprites;
using Rogue.World.Items;
using Rogue.Core.Objects;
using Rogue.Core.Scenes;
using Rogue.Core.DesignPattern;
using Rogue.Core.Animations;
using Rogue.Core.Geometry;
using Rogue.Core.Input;
using Rogue.World.Entities.Weapons;

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
        public float MouseRotation
        {
            get;
            set;
        }
        public bool HoldingWeapon => Weapon != null;

        public override bool CanMove => IsMainPlayer && !Dashing && !Aiming;

        public override bool Controlable => IsMainPlayer;

        public override bool UseInterpolation => true;

        private Weapon Weapon
        {
            get;
            set;
        }
        public void DefineWeapon(string animationName)
        {
            this.Weapon = new Weapon(animationName, this);
        }
        public Player(ProtocolPlayer protocolPlayer) : base(protocolPlayer)
        {
            this.IsMainPlayer = ClientHost.Client.Account.Id == Id;

            if (protocolPlayer.WeaponAnimation != string.Empty)
            {
                DefineWeapon(protocolPlayer.WeaponAnimation);
            }

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
            if (Weapon != null && State == EntityStateEnum.MOVING)
            {
                if (Weapon.OverCharacter)
                {
                    base.OnDraw(time);
                    Weapon.OnDraw(time);
                }
                else
                {
                    Weapon.OnDraw(time);
                    base.OnDraw(time);
                }
            }
            else
            {
                base.OnDraw(time);
            }

        }

        public override Collider2D CreateCollider()
        {
            return new WonderDotCollider(this);
        }
        public override void OnPositionReceived(Vector2 position, DirectionEnum direction, float mouseRotation)
        {
            MouseRotation = mouseRotation;
            base.OnPositionReceived(position, direction, mouseRotation);
        }
        public override void OnUpdate(GameTime time)
        {
            if (this.IsMainPlayer)
            {
                MouseRotation = Center.GetMouseRotation();
            }
            base.OnUpdate(time);
        }
        public override void OnDispose()
        {

        }
    }
}
