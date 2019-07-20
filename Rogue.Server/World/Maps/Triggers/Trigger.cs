using Microsoft.Xna.Framework;
using Rogue.Server.Records;
using Rogue.Server.World.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Server.World.Maps.Triggers
{
    public abstract class Trigger
    {
        protected MapInstance MapInstance
        {
            get;
            private set;
        }
        protected MapInteractiveRecord InteractiveElement
        {
            get;
            private set;
        }
        public Trigger(MapInstance mapInstance, MapInteractiveRecord interactiveElement)
        {
            this.MapInstance = mapInstance;
            this.InteractiveElement = interactiveElement;
        }

        public abstract void OnClicked(Player player);

        public abstract void Update(long deltaTime);
    }
}
