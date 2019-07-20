using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Rogue.WorldEditor
{
    /// <summary>
    /// Logique d'interaction pour TileSelection.xaml
    /// </summary>
    public partial class TileSelection : Window
    {
        public const int TILE_SIZE = 50;
        public const int TILE_PER_LINE = 10;

        public const string TILE_EXTENSION = ".png";
        public const string TILES_PATH = @"C:\Users\Skinz\Desktop\Heros-Die-Last\Rogue\bin\DesktopGL\AnyCPU\Debug\Content\Tiles";

        public bool IsTileSelected
        {
            get
            {
                return selectedTile.Fill is ImageBrush;
            }
        }
        public TileSelection()
        {
            InitializeComponent();
            RenderOptions.SetBitmapScalingMode(selectedTile, BitmapScalingMode.NearestNeighbor);
            Render();
        }
        private void Render()
        {
            var files = Directory.GetFiles(TILES_PATH);

            double lineCount = Math.Ceiling(files.Length / (double)TILE_PER_LINE);

            int i = 0;

            canvas.Width = TILE_SIZE * TILE_PER_LINE;
            canvas.Height = TILE_SIZE * lineCount;

            for (int y = 0; y < lineCount; y++)
            {
                for (int x = 0; x < TILE_PER_LINE; x++)
                {
                    if (i >= files.Length)
                        break;

                    Rectangle rect = new Rectangle();
                    rect.Uid = files[i];
                    rect.MouseLeftButtonDown += Rect_MouseLeftButtonDown;
                    rect.MouseEnter += Rect_MouseEnter;
                    rect.MouseLeave += Rect_MouseLeave;
                    rect.Stroke = new SolidColorBrush(Colors.Black);
                    RenderOptions.SetBitmapScalingMode(rect, BitmapScalingMode.NearestNeighbor);
                    ImageBrush imageBrush = new ImageBrush(Utils.GetImageSource(files[i]));
                    rect.Fill = imageBrush;
                    rect.StrokeThickness = 1;
                    rect.Width = TILE_SIZE;
                    rect.Height = TILE_SIZE;
                    Canvas.SetLeft(rect, x * TILE_SIZE);
                    Canvas.SetTop(rect, y * TILE_SIZE);
                    canvas.Children.Add(rect);
                    i++;
                }
            }
        }

        private void Rect_MouseLeave(object sender, MouseEventArgs e)
        {
            Rectangle rect = (Rectangle)sender;
            rect.Fill.Opacity = 1f;

        }
        public Brush GetSelectedTileBrush()
        {
            return selectedTile.Fill;
        }
        public string GetSelectedTilePath()
        {
            return selectedTile.Uid;
        }
        private void Rect_MouseEnter(object sender, MouseEventArgs e)
        {
            Rectangle rect = (Rectangle)sender;
            rect.Fill.Opacity = 0.5f;
        }

        private void Rect_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle rect = (Rectangle)sender;
            var imageBrush = new ImageBrush(Utils.GetImageSource(rect.Uid));
            selectedTile.Fill = imageBrush;
            selectedTile.Uid = rect.Uid;
            tilename.Content = "Tilename: " + System.IO.Path.GetFileNameWithoutExtension(rect.Uid);
        }
    }
}
