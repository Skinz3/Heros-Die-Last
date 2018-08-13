using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoFramework;
using MonoFramework.Animations;
using MonoFramework.Collisions;
using MonoFramework.Geometry;
using MonoFramework.Input;
using MonoFramework.Objects;
using MonoFramework.Objects.Abstract;
using MonoFramework.Objects.Entities;
using MonoFramework.PhysX;
using MonoFramework.Scenes;
using Rogue.Collisions;
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

namespace Rogue.Objects
{
    public class Player : MovableEntity
    {
        public bool IsMainPlayer
        {
            get;
            private set;
        }

        public override bool Controlable => IsMainPlayer;

        public Player(ProtocolPlayer protocolPlayer) : base(protocolPlayer)
        {
            this.IsMainPlayer = ClientHost.Client.Account.Id == Id;
        }
        public override void OnInitializeComplete()
        {
            if (IsMainPlayer)
            {
                AddScript(new MainPlayerScript());
            }
            else
            {
                AddScript(new EntityInterpolationScript());
            }

            base.OnInitializeComplete();
        }

        public override void OnDraw(GameTime time)
        {
            Debug.DrawRectangle(Collider.MovementHitBox, Color.LimeGreen);
            base.OnDraw(time);
        }
        public void OnPositionReceived(Vector2 position, DirectionEnum direction)
        {
            if (EntityInterpolationScript.UseInterpolation)
            {
                GetScript<EntityInterpolationScript>().OnPositionReceived(position, direction);
            }
            else
            {
                Position = position;
                MovementEngine.Direction = direction;
            }

        }
        public override Collider2D CreateCollider()
        {
            return new PlayerCollider(this);
        }
        public override void OnUpdate(GameTime time)
        {
            base.OnUpdate(time);
        }
        public override void OnDispose()
        {

        }
    }
}
