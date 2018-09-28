using Microsoft.Xna.Framework;
using MonoFramework.Objects.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFramework.Objects
{
    public interface IScript
    {
        void Initialize(GameObject target);

        void Update(GameTime time);

        void Dispose();

        void OnRemove();
    }
}
