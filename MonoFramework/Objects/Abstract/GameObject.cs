using Microsoft.Xna.Framework;
using MonoFramework.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFramework.Objects.Abstract
{
    /// <summary>
    /// Représente un objet de scene.
    /// </summary>
    public abstract class GameObject
    {
        static Logger logger = new Logger();

        public bool Initialized
        {
            get;
            private set;
        }
        /// <summary>
        /// Objets enfants (seront dessinés sous le parent)
        /// </summary>
        public List<GameObject> Childs
        {
            get;
            private set;
        }
        private List<IScript> Scripts
        {
            get;
            set;
        }
        /// <summary>
        /// Objet parent
        /// </summary>
        public GameObject Parent
        {
            get;
            set;
        }
        /// <summary>
        /// Couche sur laquelle est dessiné l'objet 
        /// </summary>
        public LayerEnum Layer
        {
            get;
            set;
        }

        public GameObject()
        {
            this.Childs = new List<GameObject>();
            this.Scripts = new List<IScript>();
        }
        /// <summary>
        /// On empêche un éventuel problème d'énumeration grâce au Childs
        /// </summary>
        public void Initialize()
        {
            if (!Initialized)
            {
                OnInitialize();
                OnInitializeComplete();
                Initialized = true;
            }
            else
            {
                logger.Write("GameObject: " + GetType().Name + " was already initialized!", MessageState.ERROR);
            }

        }
        public T GetScript<T>() where T : IScript
        {
            return Scripts.OfType<T>().FirstOrDefault();
        }
        public T GetParent<T>() where T : GameObject
        {
            return Parent as T;
        }
        public void AddScript(IScript script)
        {
            script.Initialize(this);
            Scripts.Add(script);
        }
        public void RemoveFirstScript<T>() where T : IScript
        {
            var script = GetScript<T>();
            RemoveScript(script);
        }
        public void RemoveScript(IScript script)
        {

            script?.OnRemove();
            Scripts.Remove(script);
        }
        public void AddChild(GameObject child)
        {
            child.Parent = this;
            child.Initialize();
            Childs.Add(child);
        }
        public virtual void Draw(GameTime time)
        {
            OnDraw(time);

            foreach (var child in Childs)
            {
                child.Draw(time);
            }
        }

        public virtual void Update(GameTime time)
        {
            OnUpdate(time);

            foreach (var child in Childs.ToArray())
            {
                child.Update(time);
            }
            foreach (var script in Scripts.ToArray())
            {
                script.Update(time);
            }
        }

        public abstract void OnInitialize();
        public abstract void OnInitializeComplete();
        public abstract void OnDraw(GameTime time);
        public abstract void OnUpdate(GameTime time);

        public virtual void Dispose()
        {
            foreach (var script in Scripts.ToArray())
            {
                script.Dispose();
            }
            Scripts = null;
            OnDispose();
        }

        public abstract void OnDispose();

        public override string ToString()
        {
            return GetType().Name;
        }
    }
}
