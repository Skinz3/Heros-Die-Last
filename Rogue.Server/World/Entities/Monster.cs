using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteNetLib;
using Microsoft.Xna.Framework;
using Rogue.Core.Objects;
using Rogue.Core.Pathfinding;
using Rogue.Core.Scenes;
using Rogue.Protocol.Messages.Server;
using Rogue.Protocol.Types;
using Rogue.Server.Collisions;
using Rogue.Server.Records;
using Rogue.Server.World.Entities.Scripts;
using Rogue.Server.World.Maps;

namespace Rogue.Server.World.Entities
{
    public class Monster : RecordableEntity
    {
        public Monster(EntityRecord record, Vector2 position) : base(record, position)
        {
        }
        public Monster(EntityRecord record, MapCell cell) : base(record, new Vector2())
        {
            this.Position = GetCellPosition(cell);
        }
        private int m_Id;

        public override int Id => m_Id;

        public override Collider2D CreateCollider()
        {
            return new WonderDotCollider(this);
        }
        public override void DefineMapInstance(MapInstance instance)
        {
            this.m_Id = instance.GetNextEntityId();
            AddScript(new AIMovementScript());
            base.DefineMapInstance(instance);
        }
        public override Rectangle GetHitBox()
        {
            return Collider.EntityHitBox;
        }
        protected override void OnDead(Entity source)
        {
            base.OnDead(source);
        }
        public override MapInstance GetMapInstance()
        {
            return MapInstance;
        }
        public override ProtocolEntity GetProtocolObject()
        {
            return new ProtocolMonster(Name, Id, Position, Size, Stats, Record.Animations.ToArray(), Record.IdleAnimation, Record.MovementAnimation, Aura);
        }
        public void MoveOnTarget(Entity target)
        {
            GetScript<AIMovementScript>().Move(target);
        }
        public void MoveOnCell(MapCell cell)
        {
            MapInstance.Send(new AIMoveMessage(Id, Position, cell.Id));
            GetScript<AIMovementScript>().Move(cell);
        }

        ICell targetCell;

        public override void SendPosition()
        {
            this.MapInstance.Send(new EntityDispositionMessage(Id, Position, Direction, 0f), Id, SendOptions.Unreliable);
        }
        private void MoveRandomly()
        {
            if (GetCell() == targetCell)
            {
                targetCell = null;
            }
            if (targetCell == null)
            {
                targetCell = MapInstance.Record.Grid.RandomPathableCell(GetCell().Id);
                MoveOnCell((MapCell)targetCell);
            }
        }

        bool d = false;

        public override void OnUpdate(long deltaTime)
        {
            var player = MapInstance.GetEntities<Player>().FirstOrDefault();
        
            if (!d && player != null && !GetScript<AIMovementScript>().Moving && this.Stats.LifePoints < Stats.MaxLifePoints)
            {

                MoveOnTarget(player);
                d = true;
            }

        }
 

    }
}
