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

        TestObject obj;

        public override bool HandleCameraInput => false;

        public override Color ClearColor => Color.Black;

        public override string DefaultFontName => "arial";

        public TestScene()
        {

        }
        public override void OnInitialize()
        {
            string[] spriteNames = new string[] { "sprite_hero06", "sprite_hero07", "sprite_hero08", "sprite_hero09" };
            var obj = new TestObject(new Vector2(), new Point(48 * 3, 48 * 3), spriteNames, 150f, true);
            AddObject(obj, LayerEnum.First);
        }

        public override void OnInitializeComplete()
        {

        }

        public override void OnDispose()
        {
            throw new NotImplementedException();
        }
    }
}
