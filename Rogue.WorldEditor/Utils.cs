using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Rogue.WorldEditor
{
    class Utils
    {
        [DllImport("user32.dll", EntryPoint = "SetWindowText")]
        private static extern int SetWindowText(IntPtr hWnd, string text);

        [DllImport("user32.dll", EntryPoint = "FindWindowEx")]
        private static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(IntPtr hWnd, int uMsg, int wParam, string lParam);

        public static void OpenNotepad(string message = null, string title = null)
        {
            Process notepad = Process.Start(new ProcessStartInfo("notepad.exe"));
            if (notepad != null)
            {
                notepad.WaitForInputIdle();

                if (!string.IsNullOrEmpty(title))
                    SetWindowText(notepad.MainWindowHandle, title);

                if (!string.IsNullOrEmpty(message))
                {
                    IntPtr child = FindWindowEx(notepad.MainWindowHandle, new IntPtr(0), "Edit", null);
                    SendMessage(child, 0x000C, 0, message);
                }
            }
        }
        public static BitmapImage GetImageSource(string imagePath)
        {
            MemoryStream ms = new MemoryStream();
            var bmp = Bitmap.FromFile(imagePath);
            return GetImageSource((Bitmap)bmp);
           
        }
        public static BitmapImage GetImageSource(Bitmap bitmap)
        {
            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Png);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }

        public static Bitmap CreateLight(int radius, float sharpness, System.Windows.Media.Color mColor)
        {

            Color color = Color.FromArgb(mColor.A, mColor.R, mColor.G, mColor.B);

            float thikness = radius;
            float diameter = radius * 2f;

            Bitmap bitmap = new Bitmap((int)diameter, (int)diameter);

            Point center = new Point(radius, radius);

            for (int colIndex = 0; colIndex < diameter; colIndex++)
            {
                for (int rowIndex = 0; rowIndex < diameter; rowIndex++)
                {
                    Point position = new Point(colIndex, rowIndex);
                    float distance = GetDistance(center, position);

                    // hermite interpolation
                    float x = distance / diameter;
                    float edge0 = (radius * sharpness) / diameter;
                    float edge1 = radius / diameter;

                    float temp = Clamp((x - edge0) / (edge1 - edge0), 0.0f, 1.0f);
                    float result = temp * temp * (3.0f - 2.0f * temp);

                    Color c = Color.Transparent;

                    if (distance >= radius - thikness)
                    {
                        float alpha = 1f - result;

                        c = Color.FromArgb((int)(color.A * alpha), color);
                    }


                    bitmap.SetPixel(colIndex, rowIndex, c);


                }
            }
            return bitmap;
        }
        public static T Clamp<T>(T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }

        private static float GetDistance(Point p1, Point p2)
        {
            return (float)Math.Sqrt(Math.Pow((p2.X - p1.X), 2) + Math.Pow((p2.Y - p1.Y), 2));
        }

    }
}
