using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Rogue.Core;
using Rogue.Core.Collisions;
using Rogue.Core.Objects;
using Rogue.Core.Pathfinding;
using Rogue.Core.Scenes;
using Rogue.Core.Utils;
using Rogue.Collisions;
using Rogue.Network;
using Rogue.Protocol.Types;
using Rogue.Scripts;

namespace Rogue.Objects.Entities
{
    public class Monster : MovableEntity
    {
        public Monster(ProtocolMonster protocolMonster) : base(protocolMonster)
        {
            AddScript(new AIMovementScript());
        }
        public override void OnInitialize()
        {
            Animator.SetIdleAnimation();
            DefineAura(new Color(Color.CornflowerBlue, 80), 200, 0.05f);
            base.OnInitialize();
        }

        public override Collider2D CreateCollider()
        {
            return new WonderDotCollider(this);
        }
        public override void OnUpdate(GameTime time)
        {
          /*  foreach (var cell in SceneManager.GetCurrentScene<MapScene>().Map.Cells)
            {
                cell.FillColor = Color.Transparent;
            }
            var player = ClientHost.Client.Player;

            var astar = new AStar(SceneManager.GetCurrentScene<MapScene>().Map, Collider.CurrentCell.Id, player.Collider.CurrentCell.Id);

            foreach (var cell in astar.FindPath().Cast<GCell>())
            {
                cell.FillColor = new Color(Color.LightGreen, 0.2f);
            }
            GetCell().FillColor = Color.Red; */
        }
        public override void OnDraw(GameTime time)
        {
            base.OnDraw(time);
        }
        public override void OnDispose()
        {

        }
    }
}
