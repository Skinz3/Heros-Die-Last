using Rogue.Core.IO.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Rogue.WorldEditor
{
    public enum ElementType
    {
        Sprite,
        Animation,
        Dummy,
    }
    public class Element
    {
        public Rectangle Rectangle
        {
            get;
            private set;
        }
        public WpfCell Cell
        {
            get;
            private set;
        }
        public ElementType Type
        {
            get;
            set;
        }
        public string VisualIdentifier
        {
            get;
            set;
        }
        private AnimationPlayer Player
        {
            get;
            set;
        }
        public Element(WpfCell cell)
        {
            this.Cell = cell;
            this.Rectangle = new Rectangle();
            RenderOptions.SetBitmapScalingMode(Rectangle, BitmapScalingMode.NearestNeighbor); // pixel perfect
            SetEmpty();
        }
        public void AddToCanvas(Canvas canvas)
        {
            Canvas.SetLeft(Rectangle, Cell.Position.X);
            Canvas.SetTop(Rectangle, Cell.Position.Y);
            canvas.Children.Add(Rectangle);
        }

        public void SetEmpty()
        {
            DestroyAnimation();
            this.Rectangle.Fill = new SolidColorBrush(Colors.Transparent);
            Rectangle.Width = MapTemplate.MAP_CELL_SIZE;
            Rectangle.Height = MapTemplate.MAP_CELL_SIZE;
            Type = ElementType.Dummy;
        }

        public void SetSprite(string path, bool flipVertical = false, bool flipHorizontal = false)
        {
            SetEmpty();
            var imageBrush = new ImageBrush(Utils.GetImageSource(path));
            this.Rectangle.Fill = imageBrush;

            imageBrush.RelativeTransform = new ScaleTransform(1, 1, 0.5f, 0.5f);

            if (flipHorizontal)
            {
                ((ScaleTransform)imageBrush.RelativeTransform).ScaleX = -1;
            }
            if (flipVertical)
            {
                ((ScaleTransform)imageBrush.RelativeTransform).ScaleY = -1;
            }

            VisualIdentifier = System.IO.Path.GetFileNameWithoutExtension(path);
            Type = ElementType.Sprite;
        }

        public void DestroyAnimation()
        {
            if (Player != null)
            {
                Player.Dispose();
                Player = null;
            }
        }
        public void SetAnimation(string animation)
        {
            SetEmpty();
            VisualIdentifier = animation.ToString();
            var animationTemplate = AddAnimation.GetAnimationElementTemplate(animation);
            var sprites = animationTemplate.SpriteNames.Select(x => AddAnimation.GetSpritePath(x)).ToArray();
            Player = new AnimationPlayer(sprites, Rectangle, animationTemplate.FlipVertical, animationTemplate.FlipHorizontal);
            Player.Play(animationTemplate.Delay);
            Type = ElementType.Animation;
        }
    }
}
