﻿using LiteNetLib.Utils;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Protocol.Types
{
    public class ProtocolMonster : ProtocolMovableEntity
    {
        public new const short Id = 8;

        public override short TypeIdProp
        {
            get
            {
                return Id;
            }
        }

        public ProtocolMonster(string name, int entityId, Vector2 position, Point size,
            Stats stats, StateAnimations[] animations) : base(name, entityId, position, size, stats, animations)
        {

        }
        public ProtocolMonster()
        {

        }
        public override void Deserialize(LittleEndianReader reader)
        {
            base.Deserialize(reader);
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            base.Serialize(writer);
        }
    }
}