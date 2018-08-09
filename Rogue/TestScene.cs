using Microsoft.Xna.Framework;
using MonoFramework.Collisions;
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
        public override bool HandleCameraInput => false;

        public override Color ClearColor => Color.Black;

        public override string DefaultFontName => "arial";

        private MapTemplate mapTemplate;
        public GMap map;
        private Player player;

        public TestScene()
        {

        }
        public override void OnInitialize()
        {
            mapTemplate = new MapTemplate();
            LittleEndianReader reader = new LittleEndianReader(File.ReadAllBytes(@"C:/Users/Skinz/Documents/test.map"));
            mapTemplate.Deserialize(reader);

            map = new GMap(new Point(mapTemplate.Width, mapTemplate.Height));
            AddObject(map, LayerEnum.First);



            string[] spriteNames = new string[] { "sprite_hero06", "sprite_hero07", "sprite_hero08", "sprite_hero09" };
            player = new Player(new Vector2(), new Point(48 * 3, 48 * 3), spriteNames, 150f, true);
            AddObject(player, LayerEnum.Second);
        }

        public override void OnInitializeComplete()
        {
            map.ToogleDrawRectangles(false);
            map.Load(mapTemplate);
            this.Camera.Target = player;
        }

        public override void OnDispose()
        {

        }
    }
}
