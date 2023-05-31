using Rogue.Core;
using Rogue.Core.IO.Maps;
using Rogue.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Rogue.WorldEditor
{
    public class WpfMap
    {
        public event Action<WpfCell> MouseEnter;
        public event Action<WpfCell> MouseLeave;
        public event Action<WpfCell> MouseLeftButtonDown;
        public event Action<WpfCell> MouseRightButtonDown;

        public static WpfMap Current;

        public int Width
        {
            get;
            private set;
        }
        public float Zoom
        {
            get;
            set;
        }
        public int Height
        {
            get;
            private set;
        }

        private int PixelWidth
        {

            get
            {
                return Width * MapTemplate.MAP_CELL_SIZE;
            }
        }
        private int PixelHeight
        {
            get
            {
                return Height * MapTemplate.MAP_CELL_SIZE;
            }
        }
        public WpfCell[] Cells
        {
            get;
            private set;
        }
        public bool DisplayGrid
        {
            get;
            set;
        } = true;

        public WpfMap(MapTemplate template)
        {
            Initialize(template.Width, template.Height);

            foreach (var cell in Cells)
            {
                var cellTemplate = template.GetCellTemplate(cell.Id);

                foreach (var sprite in cellTemplate.Sprites)
                {
                    if (sprite.IsAnimation)
                    {
                        cell.Layers[sprite.Layer].SetAnimation(sprite.VisualData);
                    }
                    else
                    {
                        string spritePath = System.IO.Path.Combine(Configuration.GetTilesPath(), sprite.VisualData + TileSelection.TILE_EXTENSION);
                        cell.Layers[sprite.Layer].SetSprite(spritePath, sprite.FlippedVertically, sprite.FlippedHorizontally);
                    }
                }
                cell.SetLight(cellTemplate.Light);
                cell.Walkable = cellTemplate.Walkable;

            }

        }
        public WpfMap(int width, int heigth)
        {
            Initialize(width, heigth);
        }
        private void Initialize(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            this.Cells = new WpfCell[width * height];

            int id = 0;
            for (int i = 0; i < width; i++)
            {
                for (int y = 0; y < height; y++)
                {
                    this.Cells[id] = new WpfCell(this, id, new Point(i * MapTemplate.MAP_CELL_SIZE, y * MapTemplate.MAP_CELL_SIZE));
                    id++;
                }
            }
        }
        public void Draw(Canvas canvas, LayerEnum layers)
        {
            canvas.Children.Clear();
            canvas.Width = PixelWidth;
            canvas.Height = PixelHeight;

            canvas.Background = Brushes.White;

            foreach (var cell in Cells)
            {
                cell.Draw(canvas, layers);
            }

            DisplayLights(canvas);
        }
        public void DisplayLights(Canvas canvas)
        {
            foreach (var cell in Cells)
            {
                var template = cell.GetLightTemplate();

                if (template != null)
                {
                    if (cell.LightRectangle != null)
                    {
                        canvas.Children.Remove(cell.LightRectangle);
                    }
                    var bitmap = Utils.CreateLight(template.Radius, template.Sharpness, Color.FromArgb(template.A, template.R, template.G, template.B));
                    cell.LightRectangle = new Rectangle();
                    cell.LightRectangle.Width = template.Radius;
                    cell.LightRectangle.Height = template.Radius;
                    cell.LightRectangle.IsHitTestVisible = false;
                    cell.LightRectangle.Fill = new ImageBrush(Utils.GetImageSource(bitmap));
                    Canvas.SetLeft(cell.LightRectangle, cell.Position.X - (template.Radius / 2) + (MapTemplate.MAP_CELL_SIZE / 2));
                    Canvas.SetTop(cell.LightRectangle, cell.Position.Y - template.Radius / 2 + (MapTemplate.MAP_CELL_SIZE / 2));
                    canvas.Children.Add(cell.LightRectangle);
                }
            }
        }
        public void ToggleGrid(bool display)
        {
            foreach (var cell in Cells)
            {
                cell.GridRectangle.StrokeThickness = display ? 1f : 0f;
            }
            DisplayGrid = display;
        }
        public WpfCell GetCell(int cellId)
        {
            if (cellId >= Cells.Length || cellId < 0)
                return null;
            else
                return Cells[cellId];
        }
        public void OnMouseEnter(WpfCell cell)
        {
            MouseEnter?.Invoke(cell);
        }
        public void OnMouseLeave(WpfCell cell)
        {
            MouseLeave?.Invoke(cell);
        }
        public void OnMouseLeftButtonDown(WpfCell cell)
        {
            MouseLeftButtonDown?.Invoke(cell);
        }
        public void OnMouseRightButtonDown(WpfCell cell)
        {
            MouseRightButtonDown?.Invoke(cell);
        }
        public MapTemplate Export()
        {
            MapTemplate template = new MapTemplate()
            {
                Cells = new CellTemplate[Width * Height],
                Height = Height,
                Width = Width,
                Zoom = Zoom,
            };

            for (int i = 0; i < template.Cells.Length; i++)
            {
                template.Cells[i] = new CellTemplate()
                {
                    Id = Cells[i].Id,
                    Walkable = Cells[i].Walkable,
                    Sprites = Cells[i].GetSpritesTemplates(),
                    Light = Cells[i].GetLightTemplate(),
                };
            }

            return template;
        }


    }
    public class WpfCell
    {
        public static LayerEnum[] EditableLayers = new LayerEnum[]
        {
            LayerEnum.First,
            LayerEnum.Second,
            LayerEnum.Third,
        };

        public int Id
        {
            get;
            private set;
        }
        public Point Position
        {
            get;
            private set;
        }
        public Dictionary<LayerEnum, Element> Layers
        {
            get;
            private set;
        }

        private LightTemplate Light
        {
            get;
            set;
        }

        public Rectangle GridRectangle
        {
            get;
            private set;
        }

        public Rectangle PreviewRectangle
        {
            get;
            private set;
        }
        private WpfMap Map
        {
            get;
            set;
        }
        public bool Walkable
        {
            get;
            set;
        } = true;

        public Rectangle LightRectangle
        {
            get;
            set;
        }

        public WpfCell(WpfMap map, int id, Point position)
        {
            this.Map = map;
            this.Id = id;
            this.Position = position;

            this.Layers = new Dictionary<LayerEnum, Element>();

            foreach (var layer in EditableLayers)
            {
                Element dummy = new Element(this);
                Layers.Add(layer, dummy);
            }

            GridRectangle = new Rectangle();
            GridRectangle.Fill = new SolidColorBrush(Colors.Transparent);
            GridRectangle.Width = MapTemplate.MAP_CELL_SIZE;
            GridRectangle.Height = MapTemplate.MAP_CELL_SIZE;

            PreviewRectangle = new Rectangle();
            PreviewRectangle.Fill = new SolidColorBrush(Colors.Transparent);
            PreviewRectangle.Width = MapTemplate.MAP_CELL_SIZE;
            PreviewRectangle.Height = MapTemplate.MAP_CELL_SIZE;
            RenderOptions.SetBitmapScalingMode(PreviewRectangle, BitmapScalingMode.NearestNeighbor);


            GridRectangle.MouseEnter += Rectangle_MouseEnter;
            GridRectangle.MouseLeave += Rectangle_MouseLeave;
            GridRectangle.MouseRightButtonDown += GridRectangle_MouseRightButtonDown;
            GridRectangle.MouseLeftButtonDown += Rectangle_MouseLeftButtonDown;
            GridRectangle.Stroke = new SolidColorBrush(Colors.Black);
            GridRectangle.StrokeThickness = map.DisplayGrid ? 1f : 0f;
        }

        public void Draw(Canvas canvas, LayerEnum displayedLayers)
        {
            foreach (var layer in Layers)
            {
                if (displayedLayers.HasFlag(layer.Key))
                {
                    layer.Value.AddToCanvas(canvas);
                }
            }

            AddRectangleToCanvas(PreviewRectangle, canvas);
            AddRectangleToCanvas(GridRectangle, canvas);

        }

        public void SetLight(LightTemplate template)
        {
            Light = template;
        }
        public void SetLight(short radius, float sharpness, Color color)
        {
            Light = new LightTemplate()
            {
                A = color.A,
                R = color.R,
                G = color.G,
                B = color.B,
                Radius = radius,
                Sharpness = sharpness,
            };
        }
        public void RemoveLight(Canvas canvas)
        {
            Light = null;
            canvas.Children.Remove(LightRectangle);
            LightRectangle = null;
        }
        public LightTemplate GetLightTemplate()
        {
            return Light;
        }
        private void AddRectangleToCanvas(Rectangle rectangle, Canvas canvas)
        {
            Canvas.SetLeft(rectangle, Position.X);
            Canvas.SetTop(rectangle, Position.Y);
            canvas.Children.Add(rectangle);
        }
        private void GridRectangle_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Map.OnMouseRightButtonDown(this);
        }
        private void Rectangle_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Map.OnMouseLeftButtonDown(this);
        }

        private void Rectangle_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Map.OnMouseLeave(this);
        }

        private void Rectangle_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Map.OnMouseEnter(this);
        }

        public SpriteTemplate[] GetSpritesTemplates()
        {
            List<SpriteTemplate> templates = new List<SpriteTemplate>();

            foreach (var layer in Layers)
            {
                if (layer.Value.Type != ElementType.Dummy)
                {
                    SpriteTemplate sprite = new SpriteTemplate();

                    if (layer.Value.Rectangle.Fill.RelativeTransform is ScaleTransform)
                    {
                        var scaleTransform = (ScaleTransform)layer.Value.Rectangle.Fill.RelativeTransform;
                        sprite.FlippedHorizontally = scaleTransform.ScaleX == -1 ? true : false;
                        sprite.FlippedVertically = scaleTransform.ScaleY == -1 ? true : false;
                    }

                    sprite.Layer = layer.Key;

                    sprite.IsAnimation = layer.Value.Type == ElementType.Animation;

                    sprite.VisualData = layer.Value.VisualIdentifier;

                    templates.Add(sprite);
                }
            }
            return templates.ToArray();
        }


    }
}
