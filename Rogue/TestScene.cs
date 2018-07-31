using Microsoft.Xna.Framework;
using MonoFramework.IO;
using MonoFramework.IO.Maps;
using MonoFramework.Objects;
using MonoFramework.Objects.Abstract;
using MonoFramework.Scenes;
using MonoFramework.Utils;
using Rogue.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue
{
    public class TestScene : Scene
    {

        List<TestObject> Objects
        {
            get;
            set;
        }

        public override bool HandleCameraInput => true;

        public TestScene()
        {
            Objects = new List<TestObject>();
        }

        public override void Dispose()
        {

        }

        public override void OnInitialize()
        {
            string[] spriteNames = new string[] { "sprite_hero06", "sprite_hero07", "sprite_hero08", "sprite_hero09"};
         

            var test = new AsyncRandom();
            for (int i = 0; i < 100; i++)
            {
                var obj = new TestObject(new Vector2(i * 80, 0), new Point(48 * 3, 48 * 3), spriteNames, 150f, true);
                obj.speed =(float)test.NextDouble(0.5d, 3d);
                Objects.Add(obj);
            }


            foreach (var obj in Objects)
            {
                AddObject(obj, LayerEnum.FIRST);
            }
        }

        public override void OnInitializeComplete()
        {

        }


    }
}
