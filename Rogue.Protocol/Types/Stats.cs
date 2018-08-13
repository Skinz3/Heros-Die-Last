using LiteNetLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Protocol.Types
{
    public class Stats : MessageType
    {
        public const short Id = 7;

        public override short TypeIdProp => Id;

        public int LifePoints
        {
            get;
            private set;
        }
        public int MaxLifePoints
        {
            get;
            private set;
        }
        public float Speed
        {
            get;
            private set;
        }
        public float DefaultSpeed
        {
            get;
            private set;
        }
        public Stats()
        {

        }
        public Stats(int lifePoints, float speed)
        {
            this.LifePoints = lifePoints;
            this.MaxLifePoints = lifePoints;
            this.Speed = speed;
            this.DefaultSpeed = speed;
        }

        public Stats(int lifePoints, int maxLifePoints, float speed, float defaultSpeed)
        {
            this.LifePoints = lifePoints;
            this.MaxLifePoints = maxLifePoints;
            this.Speed = speed;
            this.DefaultSpeed = defaultSpeed;
        }


        public override void Deserialize(NetDataReader reader)
        {
            LifePoints = reader.GetInt();
            MaxLifePoints = reader.GetInt();
            Speed = reader.GetFloat();
            DefaultSpeed = reader.GetFloat();
        }

        public override void Serialize(NetDataWriter writer)
        {
            writer.Put(LifePoints);
            writer.Put(MaxLifePoints);
            writer.Put(Speed);
            writer.Put(DefaultSpeed);
        }

        public static Stats GetDefaultStats()
        {
            return new Stats(100, 3f);
        }

    }
}
