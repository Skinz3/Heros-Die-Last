using LiteNetLib;
using Microsoft.Xna.Framework;
using MonoFramework.Collisions;
using MonoFramework.Objects;
using MonoFramework.Utils;
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

namespace Rogue.Server.World.Entities
{
    public class Player : MovableEntity
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

        public override int Id => Account.Id;

        public bool Teleporting
        {
            get;
            set;
        }

        public override string Name => Account.CharacterName;

        public PlayerRecord Record
        {
            get;
            private set;
        }
        public Player(RogueClient client, PlayerRecord record) : base(Stats.GetDefaultStats())
        {
            this.Client = client;
            this.Account = client.Account;
            this.Record = record;
        }

        private void TeleportOnMap(MapRecord targetMap, int cellId)
        {
            Teleporting = true;

            MapCell targetCell = targetMap.Grid.GetCell(cellId);

            Client.Player.Position = targetCell.Rectangle.Center.ToVector2();

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
        public void Teleport(int mapId)
        {
            MapRecord targetMap = MapRecord.GetMap(mapId);
            TeleportOnMap(targetMap, targetMap.RandomSpawnCellId());
        }
        public void Teleport(string sceneName)
        {
            MapRecord targetMap = MapRecord.GetMap(sceneName);
            TeleportOnMap(targetMap, targetMap.RandomSpawnCellId());

        }
        
        public void OnReceivePosition(Vector2 position, DirectionEnum direction)
        {
            if (Teleporting)
            {
                return;
            }
            this.Position = position;
            this.Direction = direction;
            //   if (Configuration.Self.DiagnosticsEnabled == false)
            {
                this.MapInstance.Send(new EntityDispositionMessage(Id, position, direction), Id, SendOptions.Sequenced);
            }
            //   else
            {
                //  this.MapInstance.Send(new EntityDispositionMessageDiag(Id, position, rotation, DateTime.Now.ToBinary())
                //      , Id, SendOptions.Sequenced);
            }
        }
        public override ProtocolEntity GetProtocolObject()
        {
            return new ProtocolPlayer(Name, Id, Position, new Point(Record.Width, Record.Height), Stats.GetDefaultStats(), Record.Animations);
        }


    }
}
