using Microsoft.Xna.Framework;
using Rogue.Core.Objects;
using Rogue.Core.Scenes;
using Rogue.Server.World.Entities;
using Rogue.Server.World.Maps;
using Rogue.Server.World.Projectiles;
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
        private Dictionary<Projectile, ProjectileRepresentation> ProjectilesRepresentations
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
            this.ProjectilesRepresentations = new Dictionary<Projectile, ProjectileRepresentation>();
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
                    TargetInstance.OnProjectileAdded += TargetInstance_OnProjectileAdded;
                    TargetInstance.OnProjectileRemoved += TargetInstance_OnProjectileRemoved;
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

        private void TargetInstance_OnProjectileRemoved(Projectile obj)
        {
            RemoveObject(ProjectilesRepresentations[obj]);
        }

        private void TargetInstance_OnProjectileAdded(Projectile obj)
        {
            var rep = new ProjectileRepresentation(obj, new Color(Color.CornflowerBlue, 80f));
            rep.Initialize();
            ProjectilesRepresentations.Add(obj, rep);
            AddObject(rep, LayerEnum.Second);
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
