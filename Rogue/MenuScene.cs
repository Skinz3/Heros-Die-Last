using Microsoft.Xna.Framework;
using MonoFramework.Scenes;
using Rogue.MapEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue
{
    public class MenuScene : Scene
    {
        public MenuScene()
        {

        }

        public override bool HandleCameraInput => true;

        public override Color ClearColor => Color.Black;

        public override string DefaultFontName => "arial";

        public override void OnDispose()
        {

        }
        public override void OnInitialize()
        {
            var playLabel = TextRenderer.AddText(new Vector2(500, 400), "Game", Color.White, 1f);
            playLabel.OnMouseLeftClick += Gtext_OnMouseLeftClick;

            var mapeditorLabel = TextRenderer.AddText(new Vector2(500, 430), "Map Editor", Color.White, 1f);
            mapeditorLabel.OnMouseLeftClick += MapeditorLabel_OnMouseLeftClick;
        }

        private void MapeditorLabel_OnMouseLeftClick(MonoFramework.Objects.Abstract.PositionableObject obj)
        {
            SceneManager.SetScene(new EditorScene());
        }

        private void Gtext_OnMouseLeftClick(MonoFramework.Objects.Abstract.PositionableObject obj)
        {
            SceneManager.SetScene(new GameScene());
        }

        public override void OnInitializeComplete()
        {

        }
    }
}
