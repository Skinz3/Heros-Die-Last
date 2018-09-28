using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Rogue;
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
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MonoFramework.Collisions;
using Rogue.MapEditor.Forms;

namespace Rogue.MapEditor
{
    /// <summary>
    /// Scene Properties : En grosse majoritée, déclaration des GameObjects qui composeront la scène.
    /// 
    /// KeyboardEvents: Gestion des évenements claviers (a remplacé un jour par une réel interface graphique :3)
    /// 
    /// Scene Initialization : Assignation des GameObjects, ajout a la scène grâce a la méthode base.AddObject(GameObject gameObject);
    /// 
    /// Mouse Events : Gestion des évenements souris
    /// 
    /// Scene Disposal : Fonctions permettant la libération des ressources. Dispose() est appelée lors d'un changement
    /// de scène, ou bien à la fermture de l'application.
    /// </summary>
    public class EditorScene : Scene
    {
        #region Scene Properties
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

        public bool CollisionEditor
        {
            get;
            private set;
        } = false;

        public override bool HandleCameraInput => true;

        private LayerEnum DrawingLayer
        {
            get;
            set;
        }
        private GText DrawingLayerLabel
        {
            get;
            set;
        }
        private GText DisplayedLayerLabel
        {
            get;
            set;
        }
        private GText CollisionEditorLabel
        {
            get;
            set;
        }
        public override Color ClearColor => Color.White;

        public override string DefaultFontName => "arial";

        #endregion

        public EditorScene(int width, int height)
        {
            Map = new GMap(new Point(width, height));
        }

        public EditorScene(MapTemplate targetTemplate)
        {
            this.TargetTemplate = targetTemplate;
            Map = new GMap(new Point(targetTemplate.Width, targetTemplate.Height));
        }

        private MapTemplate TargetTemplate
        {
            get;
            set;
        }

        #region Scene Initialization

        public override void OnInitialize()
        {
            this.DrawingLayer = LayerEnum.First;

            KeyboardManager.OnKeyPressed += OnKeyPressed;

            MouseManager.OnMiddleButtonPressed += OnMiddleButtonPressed;
            TileSelectionGrid = new TileSelectionGrid(new Vector2(0, 650), new Point(20, 3), 50, Color.Black, 1);
            AddObject(TileSelectionGrid, LayerEnum.UI);


            Map.OnMouseEnterCell += Map_OnMouseEnter;
            Map.OnMouseLeaveCell += Map_OnMouseLeave;
            Map.OnMouseRightClickCell += Map_OnMouseRightClick;
            AddObject(Map, LayerEnum.First);

            this.DrawingLayerLabel = TextRenderer.AddText(new Vector2(), "Drawing Layer: " + DrawingLayer.ToString(), Color.CornflowerBlue, 1f);

            this.DisplayedLayerLabel = TextRenderer.AddText(new Vector2(0, 30f), "Displayed Layer: " + Map.DisplayedLayer.ToString(), Color.CornflowerBlue, 1f);

            this.CollisionEditorLabel = TextRenderer.AddText(new Vector2(0, 60f), "Collision Editor :" + CollisionEditor, Color.CornflowerBlue);




        }

        private void OnMiddleButtonPressed()
        {
            var mousePosition = Mouse.GetState().Position;
            var cell = (GCell)Map.GetCell(Map.TranslateToScenePosition(mousePosition).ToVector2());


            if (cell != null && cell.Sprites.ContainsKey(DrawingLayer))
            {
                RaycastZ rayCast = new RaycastZ(mousePosition);

                if (rayCast.Cast() == Map)
                {
                    TileSelectionGrid.Cursor.Sprite = cell.GetElement<Sprite>(DrawingLayer);
                }
            }

        }

        public override void OnInitializeComplete()
        {
            if (TargetTemplate != null)
            {
                Map.Load(TargetTemplate);


            }
            WinformHelper.EnableStyle();

        }
        #endregion

        #region Keyboard Events
        private void OnKeyPressed(Keys obj)
        {
            if (obj == Keys.Space)
            {
                CollisionEditor = !CollisionEditor;

                if (CollisionEditor)
                {
                    foreach (var cell in Map.Cells)
                    {
                        if (cell.Walkable == false)
                        {
                            cell.SetText("Wall", Color.Red);
                        }
                    }
                }
                else
                {
                    foreach (var cell in Map.Cells)
                    {
                        cell.RemoveText();
                    }
                }
            }
            if (obj == Keys.F)
            {
                DisplayGrid = !DisplayGrid;
                Map.ToogleDrawRectangles(DisplayGrid);
            }

            if (obj == Keys.LeftShift)
            {
                TileSelectionGrid.Cursor.Sprite = Sprite.Flip(TileSelectionGrid.Cursor.Sprite, false, true);
            }
            if (obj == Keys.LeftControl)
            {
                TileSelectionGrid.Cursor.Sprite = Sprite.Flip(TileSelectionGrid.Cursor.Sprite, true, false);
            }
            if (obj == Keys.P)
            {
                string targetPath = string.Empty;

                if (TargetTemplate == null)
                {
                    System.Windows.Forms.SaveFileDialog dialog = new System.Windows.Forms.SaveFileDialog();
                    dialog.Filter = "Map file (.map)|*.map";
                    dialog.ShowDialog();
                    targetPath = dialog.FileName;
                }
                else
                {
                    targetPath = TargetTemplate.Path;
                }
                if (targetPath != string.Empty)
                {
                    TargetTemplate = Map.Export(Camera.Zoom);
                    TargetTemplate.Path = targetPath;
                    LittleEndianWriter writer = new LittleEndianWriter();
                    TargetTemplate.Serialize(writer);
                    File.WriteAllBytes(targetPath, writer.Data);
                    WinformHelper.ShowMessage("Your map has been saved!");
                }

            }
            if (obj == Keys.Tab)
            {
                int drawingLayer = (int)DrawingLayer;
                drawingLayer *= 2;

                if (drawingLayer > 4)
                    drawingLayer = 1;

                DrawingLayer = (LayerEnum)drawingLayer;

            }

            if (obj == Keys.D1)
            {
                Map.DefineDisplayedLayer(LayerEnum.First);
            }
            if (obj == Keys.D2)
            {
                Map.DefineDisplayedLayer(LayerEnum.Second);
            }
            if (obj == Keys.D3)
            {
                Map.DefineDisplayedLayer(LayerEnum.Third);
            }
            if (obj == Keys.D4)
            {
                Map.DefineDisplayedLayer(LayerEnum.First | LayerEnum.Second | LayerEnum.Third);
            }

            DisplayedLayerLabel.Text = "Displayed Layer: " + Map.DisplayedLayer.ToString();
            DrawingLayerLabel.Text = "Drawing Layer: " + DrawingLayer.ToString();
            CollisionEditorLabel.Text = "Collision Editor: " + CollisionEditor;
        }
        #endregion

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
            if (Map.DisplayedLayer.HasFlag(DrawingLayer))
            {
                RaycastZ rayCast = new RaycastZ(Mouse.GetState().Position);

                if (rayCast.Cast() == Map) // Si on clique bien sur la carte (et pas sur un élément d'UI par dessus par exemple)
                {
                    if (CollisionEditor)
                    {
                        obj.Walkable = true;
                        obj.RemoveText();
                    }
                    else
                    {
                        obj.RemoveSprite(DrawingLayer);
                    }
                }
            }
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                if (Map.DisplayedLayer.HasFlag(DrawingLayer)) // Si on dessine bien sur le layer affiché
                {
                    var obj = (GCell)Map.GetCell(Map.TranslateToScenePosition(Mouse.GetState().Position).ToVector2());
                    RaycastZ rayCast = new RaycastZ(Mouse.GetState().Position);

                    if (rayCast.Cast() == Map) // Si on clique bien sur la carte (et pas sur un élément d'UI par dessus par exemple)
                    {
                        if (CollisionEditor)
                        {
                            obj.Walkable = false;
                            obj.SetText("Wall", Color.Red);
                        }
                        else
                        {
                            if (TileSelectionGrid.SelectedSprite != null) // Si on a bien séléctionné un sprite
                                obj.AddSprite(TileSelectionGrid.SelectedSprite, DrawingLayer);
                        }
                    }
                }
            }
        }

        #endregion

        #region Scene Disposal

        public override void OnDispose()
        {
            KeyboardManager.OnKeyPressed -= OnKeyPressed;
        }
        #endregion

    }
}
