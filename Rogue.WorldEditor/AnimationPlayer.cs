using Rogue.Core.Animations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Rogue.WorldEditor
{
    public class AnimationPlayer
    {
        private Rectangle Rectangle
        {
            get;
            set;
        }
        private int currentFrame;

        private BitmapImage[] Images
        {
            get;
            set;
        }
        private bool FlippedHorizontally
        {
            get;
            set;
        }
        private bool FlippedVertically
        {
            get;
            set;
        }
        private DispatcherTimer Timer
        {
            get;
            set;
        }
        public AnimationPlayer(string[] spritesNames, Rectangle rectangle, bool flipVertical, bool flipHorizontal)
        {
            this.Rectangle = rectangle;
            this.FlippedHorizontally = flipHorizontal;
            this.FlippedVertically = flipVertical;

            RenderOptions.SetBitmapScalingMode(Rectangle, BitmapScalingMode.NearestNeighbor);

            Images = new BitmapImage[spritesNames.Length];
            for (int i = 0; i < spritesNames.Length; i++)
            {
                string path = System.IO.Path.Combine(spritesNames[i]);
                Images[i] = Utils.GetImageSource(path);



            }
        }
        public void Play(double interval)
        {
            this.Timer = new DispatcherTimer(DispatcherPriority.Render);
            Timer.Tick += OnFrame;
            Timer.Interval = TimeSpan.FromMilliseconds(interval);
            Timer.Start();

        }
        public void Dispose()
        {
            Timer?.Stop();
            Timer = null;
            Rectangle.Fill = Brushes.White;
        }
        private void OnFrame(object sender, EventArgs e)
        {
            if (currentFrame >= Images.Length)
                currentFrame = 0;

            var imageBrush = new ImageBrush(Images[currentFrame]);

            imageBrush.RelativeTransform = new ScaleTransform(1, 1, 0.5f, 0.5f);

            if (FlippedHorizontally)
            {
                ((ScaleTransform)imageBrush.RelativeTransform).ScaleX = -1;
            }
            if (FlippedVertically)
            {
                ((ScaleTransform)imageBrush.RelativeTransform).ScaleY = -1;
            }
            Rectangle.Fill = imageBrush;

            currentFrame++;
        }
    }
}
