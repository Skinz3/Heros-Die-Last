using Rogue.Objects;
using Rogue.Objects.Entities;
using Rogue.Scenes;
using Rogue.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rogue.Core.Scenes;
using Rogue.Core.Objects;
using Rogue.World.Entities.Projectiles;

namespace Rogue.World.Maps
{
    public class MapInstance
    {
        Logger logger = new Logger();

        private Dictionary<int, Entity> Entities
        {
            get;
            set;
        }
        private Dictionary<int, Projectile> Projectiles
        {
            get;
            set;
        }
        public MapInstance()
        {
            Entities = new Dictionary<int, Entity>();
            Projectiles = new Dictionary<int, Projectile>();
        }
        public Entity[] GetEntities()
        {
            return Entities.Values.ToArray();
        }
        public T[] GetEntities<T>() where T : Entity
        {
            return Entities.Values.OfType<T>().ToArray();
        }

        public void AddEntity(Entity entity, bool initialize = true)
        {
            if (!Entities.ContainsKey(entity.Id))
            {
                Entities.Add(entity.Id, entity);

                if (initialize)
                {
                    entity.Initialize();
                    SceneManager.CurrentScene.AddObject(entity, LayerEnum.Second);
                }
            }
            else
            {
                logger.Write("Unable to add entity " + entity.Id + " to the scene, the entity already exist.", MessageState.WARNING);
            }
        }

        public void RemoveEntity(int entityId)
        {
            if (Entities.ContainsKey(entityId))
            {
                var entity = Entities[entityId];
                SceneManager.CurrentScene.RemoveObject(entity);
                entity.Dispose();
                Entities.Remove(entityId);
            }
            else
            {
                logger.Write("Unable to remove entity " + entityId + " from the scene, the entity is unknown.", MessageState.WARNING);
            }

        }

        public void RemoveProjectile(int id)
        {
            if (Projectiles.ContainsKey(id))
            {
                var projectile = Projectiles[id];
                SceneManager.CurrentScene.RemoveObject(projectile);
                projectile.Dispose();
              
                Projectiles.Remove(id);
            }
            else
            {
                logger.Write("Unable to remove projectile, unknown projectile...", MessageState.WARNING);
            }
        }
        public void AddProjectile(Projectile projectile)
        {
            projectile.Initialize();
            Projectiles.Add(projectile.Id, projectile);
            SceneManager.CurrentScene.AddObject(projectile, LayerEnum.Second);
        }
        public T GetEntity<T>(int entityId) where T : Entity
        {
            return GetEntity(entityId) as T;
        }
        public Entity GetEntity(int entityId)
        {
            if (Entities.ContainsKey(entityId))
            {
                return Entities[entityId];
            }
            else
            {
                return default(Entity);
            }
        }

        public void Start()
        {
            foreach (var entity in Entities.Values)
            {
                entity.Initialize();
                SceneManager.CurrentScene.AddObject(entity, LayerEnum.Second);
            }
        }


        public override string ToString()
        {
            return string.Join(",", Array.ConvertAll(Entities.Values.ToArray(), x => x.ToString()));
        }


    }
}
