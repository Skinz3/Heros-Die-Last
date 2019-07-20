using Microsoft.Xna.Framework;
using Rogue.Core.IO.Maps;
using Rogue.Core.Objects;
using Rogue.Core.Objects.Abstract;
using Rogue.Core.Objects.UI;
using Rogue.Core.Scenes;
using Rogue.MapEditor.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rogue.MapEditor
{
    public class EditorMenuScene : Scene
    {
        public override bool HandleCameraInput => false;

        public override Color ClearColor => Color.Black;

        public override string DefaultFontName => "pixel";

        public override void OnDispose()
        {

        }

        public override void OnInitialize()
        {
            TextRenderer.AddText(new Vector2(300, 300), "Map Editor", Color.White, 5f);
            AddObject(new SimpleButton(new Vector2(400, 400), new Point(200, 50), "Create Map", CreateMap), LayerEnum.UI);
            AddObject(new SimpleButton(new Vector2(400, 450), new Point(200, 50), "Load Map", LoadMap), LayerEnum.UI);
        }

        private void CreateMap(PositionableObject obj)
        {
            GridSizeSelection selection = new GridSizeSelection();

            selection.ShowDialog();

            if (selection.Ok)
            {
                SceneManager.LoadScene(new EditorScene(selection.MapWidth, selection.MapHeight));
                selection.Dispose();
            }
        }
        private void LoadMap(PositionableObject obj)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Map file (.map)|*.map";
            dialog.ShowDialog();
            dialog.Dispose();

            if (dialog.FileName != string.Empty)
            {
                MapTemplate template = new MapTemplate();
                template.Load(dialog.FileName);

                SceneManager.LoadScene(new EditorScene(template));
            }
        }
        public override void OnInitializeComplete()
        {

        }
    }
}
