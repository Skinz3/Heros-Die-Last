using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Rectangle = System.Windows.Shapes.Rectangle;

namespace Rogue.WorldEditor
{
    /// <summary>
    /// Logique d'interaction pour AddLight.xaml
    /// </summary>
    public partial class AddLight : Window
    {
        private WpfCell Cell
        {
            get;
            set;
        }
        private Editor Editor
        {
            get;
            set;
        }
        public AddLight(Editor editor, WpfCell cell)
        {
            this.Cell = cell;
            this.Editor = editor;
            InitializeComponent();
            Preview(null, null);
        }

        private void Preview(object sender, TextChangedEventArgs e)
        {
            if (IsInitialized && sharpness.Text != string.Empty && r.Text != string.Empty && a.Text != string.Empty &&
                b.Text != string.Empty)
            {
                var bitmap = Utils.CreateLight((int)rectangle.Width, float.Parse(sharpness.Text),
                  System.Windows.Media.Color.FromArgb(byte.Parse(a.Text), byte.Parse(r.Text), byte.Parse(g.Text), byte.Parse(b.Text)));

                rectangle.Fill = new ImageBrush(Utils.GetImageSource(bitmap));
            }
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9^,]+");
            e.Handled = regex.IsMatch(e.Text);
        }


        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            Cell.SetLight(short.Parse(radius.Text), float.Parse(sharpness.Text),
             System.Windows.Media.Color.FromArgb(byte.Parse(a.Text), byte.Parse(r.Text), byte.Parse(g.Text), byte.Parse(b.Text)));

            WpfMap.Current.DisplayLights(Editor.canvas);
            this.Close();
        }


    }
}
