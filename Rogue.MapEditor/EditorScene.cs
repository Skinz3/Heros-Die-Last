using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoFramework;
using MonoFramework.Cameras;
using MonoFramework.Input;
using MonoFramework.IO;
using MonoFramework.IO.Maps;
using MonoFramework.Objects;
using MonoFramework.Scenes;
using Rogue.MapEditor.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rogue.MapEditor
{
    public class EditorScene : Scene
    {
        private TileSelectionGrid TileSelectionGrid
        {
            get;
            set;
        }
        private GMap Map
        {
            get;
            set;
        }
        public bool DisplayGrid
        {
            get;
            private set;
        } = true;

        public override bool HandleCameraInput => true;


        public EditorScene()
        {

        }



        public override void Dispose()
        {

        }

        private void InputManager_OnKeyPressed(Keys obj)
        {
            if (obj == Keys.F)
            {
                DisplayGrid = !DisplayGrid;
                Map.ToogleDrawRectangles(DisplayGrid);
            }
            if (obj == Keys.O)
            {
                System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
                dialog.ShowDialog();
                dialog.Dispose();
                if (dialog.FileName != string.Empty)
                {
                    LittleEndianReader reader = new LittleEndianReader(File.ReadAllBytes(dialog.FileName));
                    MapTemplate template = new MapTemplate();
                    template.Deserialize(reader);
                    Map.Load(template);
                }
            }
            if (obj == Keys.P)
            {
                var thread = new Thread(new ThreadStart(new Action(() =>
                {
                    System.Windows.Forms.SaveFileDialog dialog = new System.Windows.Forms.SaveFileDialog();
                    dialog.ShowDialog();

                    var template = Map.Export();

                    LittleEndianWriter writer = new LittleEndianWriter();
                    template.Serialize(writer);
                    File.WriteAllBytes(dialog.FileName, writer.Data);
                })));
                thread.SetApartmentState(ApartmentState.STA);

                thread.Start();

            }
        }

        public override void OnInitialize()
        {
            InputManager.OnKeyPressed += InputManager_OnKeyPressed;

            TileSelectionGrid = new TileSelectionGrid(new Vector2(100, 640), new Point(15, 3), 50, Color.Black, 1f);
            AddObject(TileSelectionGrid, LayerEnum.UI);

            Map = new GMap(new Point(40, 40));
            Map.OnMouseLeftClick += Map_OnMouseLeftClick;
            Map.OnMouseRightClick += Map_OnMouseRightClick;
            AddObject(Map, LayerEnum.FIRST);
        }

        private void Map_OnMouseRightClick(GCell obj)
        {
            obj.RemoveSprite(LayerEnum.FIRST);
        }

        private void Map_OnMouseLeftClick(GCell obj)
        {
            if (TileSelectionGrid.SelectedSprite != null)
                obj.AddSprite(TileSelectionGrid.SelectedSprite, LayerEnum.FIRST);
        }

        public override void OnInitializeComplete()
        {

        }

    }
}
