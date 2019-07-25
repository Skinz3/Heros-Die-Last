using Rogue.Core.Utils;
using Rogue.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.World.Projectiles
{
    class ProjectilesManager
    {
        static Logger logger = new Logger();

        public static Projectile GetProjectile(int id, int size, ProjectileTypeEnum type)
        {
            switch (type)
            {
                case ProjectileTypeEnum.Basic:
                    break;
                case ProjectileTypeEnum.Reflection:
                    break;
            }

            logger.Write("Unable to create projectile :" + type, MessageState.ERROR);
            return default(Projectile);
        }
    }
}
