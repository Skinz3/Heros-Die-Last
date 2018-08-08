using Microsoft.Xna.Framework;
using MonoFramework.Objects.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFramework.Collisions
{
    public class Raycast
    {
        private Ray Ray
        {
            get;
            set;
        }
        public Raycast(Ray ray)
        {
            this.Ray = ray;
        }
        /// <summary>
        /// Todo
        /// </summary>
        /// <returns></returns>
        public GameObject Cast()
        {
            throw new NotImplementedException();
        }
    }
}
