using Microsoft.Xna.Framework;
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
        /// <summary>
        /// Objets enfants (seront dessinés sous le parent)
        /// </summary>
        private List<GameObject> Childs
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
        }
        /// <summary>
        /// On empêche un éventuel problème d'énumeration grâce au Childs
        /// </summary>
        public void Initialize()
        {
            OnInitialize();

            foreach (var child in Childs)
            {
                child.Initialize();
            }

            OnInitializeComplete();
          
        }
        public void AddChild(GameObject child)
        {
            child.Parent = this;
            Childs.Add(child);
        }
        public void Draw(GameTime time)
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
            
            foreach (var child in Childs)
            {
                child.Update(time);
            }
        }

        public abstract void OnInitialize();
        public abstract void OnInitializeComplete();
        public abstract void OnDraw(GameTime time);
        public abstract void OnUpdate(GameTime time);

        public override string ToString()
        {
            return GetType().Name;
        }
    }
}
