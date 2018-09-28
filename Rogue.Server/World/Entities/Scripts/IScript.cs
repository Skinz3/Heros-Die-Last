using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Server.World.Entities.Scripts
{
    public interface IScript
    {
        void Initialize(Entity entity);

        void Update(long deltaTime);

        void OnRemove();

        void Dispose();
    }
}
