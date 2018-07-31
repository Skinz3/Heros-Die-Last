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
using MonoFramework.Sprites;
using Rogue.MapEditor.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MonoFramework.Objects.UI;
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

        private LayerEnum DrawingLayer
        {
            get;
            set;
        }
        private GString DrawingLayerLabel
        {
            get;
            set;
        }
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
                System.Windows.Forms.SaveFileDialog dialog = new System.Windows.Forms.SaveFileDialog();
                dialog.ShowDialog();

                var template = Map.Export();

                LittleEndianWriter writer = new LittleEndianWriter();
                template.Serialize(writer);
                File.WriteAllBytes(dialog.FileName, writer.Data);

            }
            if (obj == Keys.Tab)
            {
                int drawingLayer = (int)DrawingLayer;
                drawingLayer++;

                if (drawingLayer == 4)
                    drawingLayer = 1;

                DrawingLayer = (LayerEnum)drawingLayer;
                DrawingLayerLabel.Text = "Drawing Layer: " + DrawingLayer.ToString();
            }
        }

        public override void OnInitialize()
        {
            this.DrawingLayer = LayerEnum.FIRST;

            InputManager.OnKeyPressed += InputManager_OnKeyPressed;

            TileSelectionGrid = new TileSelectionGrid(new Vector2(0, 640), new Point(15, 3), 50, Color.Black, 1);
            AddObject(TileSelectionGrid, LayerEnum.UI);

            Map = new GMap(new Point(40, 40));
            Map.OnMouseLeftClickCell += Map_OnMouseLeftClick;
            Map.OnMouseEnterCell += Map_OnMouseEnter;
            Map.OnMouseLeaveCell += Map_OnMouseLeave;
            Map.OnMouseRightClickCell += Map_OnMouseRightClick;
            AddObject(Map, LayerEnum.FIRST);


            this.DrawingLayerLabel = new GString(new Vector2(), "arial", "Drawing Layer: " + DrawingLayer.ToString(), Color.Black, 1f);
            AddObject(DrawingLayerLabel, LayerEnum.UI);
        }


        public override void OnInitializeComplete()
        {
            
        }

        #region Mouse Events
        private void Map_OnMouseLeave(GCell obj)
        {
            obj.FillColor = Color.Transparent;
        }

        private void Map_OnMouseEnter(GCell obj)
        {
            obj.FillColor = Color.Red;
            obj.FillColor.A = 50;
        }

        private void Map_OnMouseRightClick(GCell obj)
        {
            obj.RemoveSprite(DrawingLayer);
        }

        private void Map_OnMouseLeftClick(GCell obj)
        {
            if (TileSelectionGrid.SelectedSprite != null)
                obj.AddSprite(TileSelectionGrid.SelectedSprite, DrawingLayer);
        }
        #endregion

    }
}
