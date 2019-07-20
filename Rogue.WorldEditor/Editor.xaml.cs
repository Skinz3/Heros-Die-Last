using Microsoft.Win32;
using Rogue.Core.IO;
using Rogue.Core.IO.Maps;
using Rogue.Core.Objects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Rogue.WorldEditor
{
    /// <summary>
    /// Logique d'interaction pour Editor.xaml
    /// </summary>
    public partial class Editor : UserControl
    {
        private string CurrentMapPath
        {
            get;
            set;
        }
        private new ContextMenu ContextMenu
        {
            get;
            set;
        }
        #region Properties
        private TileSelection TileSelection
        {
            get;
            set;
        }
        public LayerEnum DrawingLayer
        {
            get
            {
                return (LayerEnum)drawingLayerlb.SelectedItem;
            }
        }
        public bool EditingColliders
        {
            get
            {
                return editColliderCb.IsChecked.Value;
            }
        }
        #endregion

        public Editor()
        {
            InitializeComponent();
            RenderMap();
            ContextMenu = CreateContextMenu();
            InitializeDrawingLayers();
            this.Loaded += UserControl_Loaded;
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            this.KeyDown += Editor_KeyDown;
        }
        private void RenderMap()
        {
            if (WpfMap.Current == null)
                return;
            WpfMap.Current.Draw(canvas, GetDisplayedLayers());
            WpfMap.Current.MouseEnter += Current_MouseEnter;
            WpfMap.Current.MouseLeave += Current_MouseLeave;
            WpfMap.Current.MouseLeftButtonDown += Current_MouseLeftButtonDown;
            WpfMap.Current.MouseRightButtonDown += Current_MouseRightButtonDown;


        }

        private void DisplayTileSelection()
        {
            TileSelection?.Close();
            this.TileSelection = new TileSelection();
            TileSelection.Show();
            TileSelection.Closed += TileSelection_Closed;
        }

        private void TileSelection_Closed(object sender, EventArgs e)
        {
            TileSelection = null;
        }




        #region Mouse and Keyboard Inputs
        private void Editor_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            if (e.Key == Key.R)
            {
                Reload();
            }
            if (e.Key == Key.G)
            {
                displayGridcb.IsChecked = !displayGridcb.IsChecked;
                WpfMap.Current.ToggleGrid(displayGridcb.IsChecked.Value);
            }
            if (e.Key == Key.C)
            {
                editColliderCb.IsChecked = !editColliderCb.IsChecked;
                CheckBox_Click(null, null);
            }
            if (e.Key == Key.Space)
            {
                LayerEnum nextLayer = LayerEnum.First;

                switch (DrawingLayer)
                {
                    case LayerEnum.First:
                        nextLayer = LayerEnum.Second;
                        break;
                    case LayerEnum.Second:
                        nextLayer = LayerEnum.Third;
                        break;
                    case LayerEnum.Third:
                        nextLayer = LayerEnum.First;
                        break;
                }

                drawingLayerlb.SelectedItem = nextLayer;
            }
            if (e.Key == Key.T)
            {
                if (TileSelection != null)
                {
                    TileSelection.Focus();
                    TileSelection.Activate();
                }
                else
                {
                    DisplayTileSelection();
                }
            }
            if (e.Key == Key.S)
            {
                Save(CurrentMapPath);
            }

        }

        private ContextMenu CreateContextMenu()
        {
            ContextMenu menu = new ContextMenu();

            MenuItem copyItem = new MenuItem() { Header = "_Copy" };
            copyItem.Click += MenuCopySprite;
            menu.Items.Add(copyItem);

            MenuItem flipXItem = new MenuItem() { Header = "_Flip X" };
            flipXItem.Click += MenuRotateXSprite;
            menu.Items.Add(flipXItem);

            MenuItem flipYItem = new MenuItem() { Header = "_Flip Y" };
            flipYItem.Click += MenuRotateYSprite;
            menu.Items.Add(flipYItem);

            MenuItem addAnimationItem = new MenuItem() { Header = "_Add Animation" };
            addAnimationItem.Click += MenuAddInteractive;
            menu.Items.Add(addAnimationItem);

            MenuItem addLightItem = new MenuItem() { Header = "_Add Light" };
            addLightItem.Click += AddLightClick;
            menu.Items.Add(addLightItem);

            MenuItem removeLightItem = new MenuItem() { Header = "_Remove Light" };
            removeLightItem.Click += RemoveLightClick;
            menu.Items.Add(removeLightItem);

            return menu;
        }
        private void Current_MouseRightButtonDown(WpfCell obj)
        {
            if (Keyboard.IsKeyDown(Key.LeftShift))
            {
                ContextMenu.PlacementTarget = obj.GridRectangle;
                ContextMenu.DataContext = obj;
                ContextMenu.IsOpen = true;
            }
            else
            {
                RightClick(obj);
            }
        }

        private void Current_MouseLeftButtonDown(WpfCell obj)
        {
            LeftClick(obj);
        }
        private void RightClick(WpfCell obj)
        {
            if (EditingColliders)
            {
                obj.Walkable = true;
                obj.PreviewRectangle.Fill = Brushes.Transparent;
                return;
            }


            if (!GetDisplayedLayers().HasFlag(DrawingLayer))
            {
                MessageBox.Show("You are trying to erase on a layer but he is not displayed!", "Information", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }

            obj.Layers[DrawingLayer].SetEmpty();
        }
        private void LeftClick(WpfCell obj)
        {
            if (EditingColliders)
            {
                obj.Walkable = false;
                obj.PreviewRectangle.Fill = Brushes.Red;
                obj.PreviewRectangle.Opacity = 0.5f;
                return;
            }
            if (!GetDisplayedLayers().HasFlag(DrawingLayer))
            {
                MessageBox.Show("You are trying to draw on a layer but he is not displayed!", "Information", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
            if (TileSelection != null && TileSelection.IsTileSelected)
            {
                obj.Layers[DrawingLayer].SetSprite(TileSelection.selectedTile.Uid);
            }
        }

        private void Current_MouseLeave(WpfCell obj)
        {
            obj.GridRectangle.Stroke = new SolidColorBrush(Colors.Black);

            if (TileSelection != null && TileSelection.IsTileSelected && !EditingColliders)
            {
                obj.PreviewRectangle.Fill = Brushes.Transparent;
                obj.PreviewRectangle.Opacity = 1f;
            }
        }
        private void Current_MouseEnter(WpfCell obj)
        {
            obj.GridRectangle.Stroke = new SolidColorBrush(Colors.CornflowerBlue);

            MainWindow.Self.Title = "Rogue.WorldEditor CellId: " + obj.Id;

            if (TileSelection != null && TileSelection.IsTileSelected && !EditingColliders)
            {
                obj.PreviewRectangle.Fill = TileSelection.selectedTile.Fill;

                obj.PreviewRectangle.Opacity = 0.5f;
            }
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                LeftClick(obj);
            }
            else if (Mouse.RightButton == MouseButtonState.Pressed)
            {
                RightClick(obj);
            }

        }
        #endregion

        #region UI events 
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Dispose();
            this.KeyDown -= Editor_KeyDown;

            Content = new CreateMap();
        }
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            DisplayTileSelection();
        }
        private void MenuItemSave(object sender, RoutedEventArgs e)
        {
            SaveAs();
        }
        private void Save(string path)
        {
            if (path == null)
            {
                SaveAs();
            }
            else
            {
                var template = WpfMap.Current.Export();
                template.Path = path;
                template.Save();
                MessageBox.Show("Map was saved successfully!", "OK", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }
        private void SaveAs()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "MAP files (*.map) | *.map";
            var result = saveFileDialog.ShowDialog();

            if (result.Value)
            {
                CurrentMapPath = saveFileDialog.FileName;
                Save(saveFileDialog.FileName);
            }
        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            WpfMap.Current.ToggleGrid(displayGridcb.IsChecked.Value);
        }
        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "MAP files (*.map) | *.map";
            var result = openFileDialog.ShowDialog();

            if (result == true)
            {
                OpenMap(openFileDialog.FileName);
            }
        }
        private void OpenMap(string path)
        {
            Dispose();
            var template = new MapTemplate();
            template.Load(path);
            WpfMap.Current = new WpfMap(template);
            RenderMap();
            CurrentMapPath = path;
        }



        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {

            if (editColliderCb.IsChecked.Value)
            {
                foreach (var cell in WpfMap.Current.Cells)
                {
                    if (cell.Walkable == false)
                    {
                        cell.PreviewRectangle.Fill = Brushes.Red;
                        cell.PreviewRectangle.Opacity = 0.5f;
                    }
                }
            }
            else
            {
                foreach (var cell in WpfMap.Current.Cells)
                {
                    cell.PreviewRectangle.Fill = Brushes.Transparent;
                    cell.PreviewRectangle.Opacity = 1f;
                }
            }
        }
        private void MenuRotateXSprite(object sender, RoutedEventArgs e)
        {
            WpfCell cell = (WpfCell)ContextMenu.DataContext;

            if (cell.Layers[DrawingLayer].Type == ElementType.Sprite)
            {
                var element = cell.Layers[DrawingLayer];

                if (element.Rectangle.Fill.RelativeTransform is ScaleTransform)
                {
                    ((ScaleTransform)element.Rectangle.Fill.RelativeTransform).ScaleX = -((ScaleTransform)element.Rectangle.Fill.RelativeTransform).ScaleX;
                }
                else
                {
                    element.Rectangle.Fill.RelativeTransform = new ScaleTransform(-1, 1, 0.5f, 0.5f);
                }
            }
        }
        private void MenuRotateYSprite(object sender, RoutedEventArgs e)
        {

            WpfCell cell = (WpfCell)ContextMenu.DataContext;

            if (cell.Layers[DrawingLayer].Type == ElementType.Sprite)
            {
                var element = cell.Layers[DrawingLayer];

                if (element.Rectangle.Fill.RelativeTransform is ScaleTransform)
                {
                    ((ScaleTransform)element.Rectangle.Fill.RelativeTransform).ScaleY = -((ScaleTransform)element.Rectangle.Fill.RelativeTransform).ScaleY;
                }
                else
                {
                    element.Rectangle.Fill.RelativeTransform = new ScaleTransform(1, -1, 0.5f, 0.5f);
                }
            }
        }
        private void MenuCopySprite(object sender, RoutedEventArgs e)
        {


            if (TileSelection == null)
                DisplayTileSelection();


            WpfCell cell = (WpfCell)ContextMenu.DataContext;

            if (cell.Layers[DrawingLayer].Type == ElementType.Sprite)
            {
                var element = cell.Layers[DrawingLayer];

                TileSelection.selectedTile.Fill = element.Rectangle.Fill.Clone();
                TileSelection.selectedTile.Uid = AddAnimation.GetSpritePath(element.VisualIdentifier);

            }

        }
        #endregion

        #region LayerManagement
        private void InitializeDrawingLayers()
        {
            foreach (var layer in WpfCell.EditableLayers)
            {
                drawingLayerlb.Items.Add(layer);
            }
            drawingLayerlb.SelectedItem = drawingLayerlb.Items[0];
        }
        private LayerEnum GetDisplayedLayers()
        {
            LayerEnum layers = LayerEnum.First | LayerEnum.Second | LayerEnum.Third;

            if (!layer1Display.IsChecked.Value)
            {
                layers &= ~LayerEnum.First;
            }
            if (!layer2Display.IsChecked.Value)
            {
                layers &= ~LayerEnum.Second;
            }
            if (!layer3Display.IsChecked.Value)
            {
                layers &= ~LayerEnum.Third;
            }
            return layers;
        }
        private void OnLayerDisplay(object sender, RoutedEventArgs e)
        {
            WpfMap.Current.Draw(canvas, GetDisplayedLayers());
        }
        #endregion

        public void Dispose()
        {
            displayGridcb.IsChecked = true;
            editColliderCb.IsChecked = false;
            TileSelection?.Close();
        }




        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            Utils.OpenNotepad(Properties.Resources.help);
        }

        private void ZoomSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var zoom = zoomSlider.Value;
            canvas.RenderTransform = new ScaleTransform(zoom, zoom, 0.5f, 0.5f);

            if (WpfMap.Current != null)
                WpfMap.Current.Zoom = (float)zoom;
        }

        private void MenuAddInteractive(object sender, RoutedEventArgs e)
        {
            AddAnimation addInteractive = new AddAnimation((WpfCell)ContextMenu.DataContext, this);
            addInteractive.Show();
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            Reload();
        }
        private void Reload()
        {
            if (CurrentMapPath != null)
                OpenMap(CurrentMapPath);
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            Save(CurrentMapPath);
        }

        private void AddLightClick(object sender, RoutedEventArgs e)
        {
            AddLight addLight = new AddLight(this, (WpfCell)ContextMenu.DataContext);
            addLight.Show();
        }

        private void RemoveLightClick(object sender, RoutedEventArgs e)
        {
            var cell = (WpfCell)ContextMenu.DataContext;
            cell.RemoveLight(canvas);
        }


    }
}
