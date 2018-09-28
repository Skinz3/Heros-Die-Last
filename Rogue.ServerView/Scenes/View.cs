using Microsoft.Xna.Framework;
using MonoFramework.Objects;
using MonoFramework.Scenes;
using Rogue.Server.World.Entities;
using Rogue.Server.World.Maps;
using Rogue.ServerView.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.ServerView.Scenes
{
    public class View : Scene
    {
        public override bool HandleCameraInput => true;

        public override Color ClearColor => Color.White;

        public override string DefaultFontName => "pixel";

        private Dictionary<Entity, EntityRepresentation> Representations
        {
            get;
            set;
        }
        private MapInstance TargetInstance
        {
            get;
            set;
        }
        public View()
        {
            this.Representations = new Dictionary<Entity, EntityRepresentation>();
        }
        public override void OnDispose()
        {

        }

        public override void OnInitializeComplete()
        {
        }


        public override void Update(GameTime gameTime)
        {
            Link();
            base.Update(gameTime);
        }
        private void Link()
        {
            if (TargetInstance == null)
            {
                var obj = MapsManager.Instances.FirstOrDefault(x => x.Value.Count > 0);

                if (obj.Value != null)
                {
                    TargetInstance = obj.Value[0];
                    TargetInstance.OnEntityJoin += TargetInstance_OnEntityJoin;
                    TargetInstance.OnEntityLeave += TargetInstance_OnEntityLeave;

                    var template = TargetInstance.Record.Template;

                    var rep = new MapRepresentation(template);
                    rep.Initialize();
                    rep.Load();
                    AddObject(rep, LayerEnum.First);
                }
            }
            else
            {
                foreach (var rep in Representations.ToArray())
                {
                    if (TargetInstance.Contains(rep.Key) == false)
                    {
                        Representations.Remove(rep.Key);
                    }
                }
                foreach (var entity in TargetInstance.GetEntities<Entity>())
                {
                    if (Representations.ContainsKey(entity) == false)
                    {
                        var rep = new EntityRepresentation(entity, Color.Red);
                        rep.Initialize();
                        Representations.Add(entity, rep);
                        AddObject(rep, LayerEnum.Second);
                    }
                }
            }
        }
        private void TargetInstance_OnEntityLeave(Entity obj)
        {
            RemoveObject(Representations[obj]);
        }

        private void TargetInstance_OnEntityJoin(Server.World.Entities.Entity obj)
        {

        }

        public override void OnInitialize()
        {
        }
    }
}
