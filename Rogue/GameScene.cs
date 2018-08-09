using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoFramework.Collisions;
using MonoFramework.Input;
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
    public class GameScene : Scene
    {
        public override bool HandleCameraInput => false;

        public override Color ClearColor => Color.Black;

        public override string DefaultFontName => "arial";

        private MapTemplate mapTemplate;
        public GMap map;
        private Player player;

        public GameScene()
        {

        }
        public override void OnInitialize()
        {
            KeyboardManager.OnKeyPressed += OnKeyPressed;
            mapTemplate = new MapTemplate();
            LittleEndianReader reader = new LittleEndianReader(File.ReadAllBytes(@"C:/Users/Skinz/Documents/test.map"));
            mapTemplate.Deserialize(reader);

            map = new GMap(new Point(mapTemplate.Width, mapTemplate.Height));
            AddObject(map, LayerEnum.First);



            string[] spriteNames = new string[] { "sprite_hero06", "sprite_hero07", "sprite_hero08", "sprite_hero09" };
            player = new Player(new Vector2(), map, new Point(48 * 3, 48 * 3), spriteNames, 150f, true);

            AddObject(new AnimableObject(new Vector2(5 * 50,0), new Point(50, 50), new string[] { "sprite_230", "sprite_231", "sprite_232", "sprite_233" }, 100f, true),
               LayerEnum.First);

            AddObject(new AnimableObject(new Vector2(7 * 50, 0), new Point(50, 50), new string[] { "sprite_230", "sprite_231", "sprite_232", "sprite_233" }, 100f, true),
              LayerEnum.First);

        }

        private void OnKeyPressed(Keys obj)
        {
            if (obj == Keys.Escape)
            {
                SceneManager.SetScene(new MenuScene());
            }
        }

        public override void OnInitializeComplete()
        {
            map.ToogleDrawRectangles(false);
            map.Load(mapTemplate);
            map.AddChild(player);
            this.Camera.Target = player;
            Camera.Zoom = 1.2f;



        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public override void OnDispose()
        {
            KeyboardManager.OnKeyPressed -= OnKeyPressed;
        }
    }
}
