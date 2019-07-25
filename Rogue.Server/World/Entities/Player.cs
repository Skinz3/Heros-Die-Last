using LiteNetLib;
using Microsoft.Xna.Framework;
using Rogue.Protocol.Enums;
using Rogue.Protocol.Messages.Server;
using Rogue.Protocol.Types;
using Rogue.Server.Frames;
using Rogue.Server.Network;
using Rogue.Server.Records;
using Rogue.Server.World.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rogue.Core.Utils;
using Rogue.Core.Collisions;
using Rogue.Server.World.Entities.Scripts;
using Rogue.Server.Collisions;
using Rogue.Server.World.Items;

namespace Rogue.Server.World.Entities
{
    public class Player : RecordableEntity
    {
        Logger logger = new Logger();

        public RogueClient Client
        {
            get;
            private set;
        }
        private AccountRecord Account
        {
            get;
            set;
        }
        public MapCell CurrentCell
        {
            get
            {
                return (MapCell)MapInstance.Record.Grid.GetCell(Record.Center(Position));
            }
        }
        public override int Id => Account.Id;

        public bool Teleporting
        {
            get;
            set;
        }
        public Inventory Inventory
        {
            get;
            private set;
        }
        private string WeaponAnimation
        {
            get;
            set;
        }
        private float MouseRotation
        {
            get;
            set;
        }
        public override string Name => Account.CharacterName;

        public Player(RogueClient client, EntityRecord record, Vector2 position) : base(record, position)
        {
            this.Client = client;
            this.Account = client.Account;
            this.Inventory = new Inventory(this);
            this.MouseRotation = 0f;
        }

        private void TeleportOnMap(MapRecord targetMap)
        {
            TeleportOnMap(targetMap, targetMap.RandomSpawnPosition(Record.Width, Record.Height));
        }
        private void TeleportOnMap(MapRecord targetMap, int cellId)
        {
            TeleportOnMap(targetMap, targetMap.Grid.GetCell<MapCell>(cellId).GetCenterPosition(Record.Width, Record.Height));
        }
        private void TeleportOnMap(MapRecord targetMap, Vector2 position)
        {
            if (Teleporting)
            {
                logger.Write("User try to teleport but was already teleporting...", MessageState.ERROR);
                return;
            }
            Teleporting = true;

            Client.Player.Position = position;

            Client.Player.LeaveMapInstance();

            switch (targetMap.Frame)
            {
                case FrameEnum.HUB:
                    Client.LoadFrame(new HubFrame(targetMap.MapName));
                    break;
                default:
                    logger.Write("Unable to teleport player, unbindable frame.");
                    break;
            }

        }
        public void Teleport(string mapName)
        {
            MapRecord targetMap = MapRecord.GetMap(mapName);
            TeleportOnMap(targetMap);
        }
        public void TeleportSameMap(int cellId)
        {
            this.Position = GetCellPosition(MapInstance.Record.Grid.GetCell<MapCell>(cellId));
            MapInstance.Send(new TeleportSameMapMessage(Id, Position));

        }

        public void Teleport(string mapName, int cellId)
        {
            MapRecord targetMap = MapRecord.GetMap(mapName);
            TeleportOnMap(targetMap, cellId);
        }

        public void OnReceivePosition(Vector2 position, DirectionEnum direction, float mouseRotation)
        {
            if (Teleporting)
            {
                return;
            }

            Position = position;
            Direction = direction;
            MouseRotation = mouseRotation;

            SendPosition();
        }
        public override ProtocolEntity GetProtocolObject()
        {
            return new ProtocolPlayer(Name, Id, Position, new Point(Record.Width, Record.Height), Stats, Record.Animations.ToArray(), Record.IdleAnimation, Record.MovementAnimation, Aura, WeaponAnimation);
        }

        public override void OnUpdate(long deltaTime)
        {
            Inventory.Update(deltaTime);
        }
        public override MapInstance GetMapInstance()
        {
            return MapInstance;
        }

        public override Collisions.Collider2D CreateCollider()
        {
            return new WonderDotCollider(this);
        }

        public override Rectangle GetHitBox()
        {
            return Collider.EntityHitBox;
        }

        public void DefineWeaponAnimation(string animationName)
        {
            WeaponAnimation = animationName;
            MapInstance.Send(new DefinePlayerWeaponMessage(Id, "item103"));
        }

        public override void SendPosition()
        {
            this.MapInstance?.Send(new EntityDispositionMessage(Id, Position, Direction, MouseRotation), Id, SendOptions.Unreliable);
        }
    }
}
