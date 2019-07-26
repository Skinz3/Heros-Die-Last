using Rogue.Core.Collisions;
using Rogue.Core.IO.Animations;
using Rogue.Core.Objects;
using Rogue.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
    /// Logique d'interaction pour AddAnimation.xaml
    /// </summary>
    public partial class AddAnimation : Window
    {
        public const string ANIMATIONS_PATH = @"C:\Users\franc\source\repos\Heros-Die-Last\Rogue\bin\DesktopGL\AnyCPU\Debug\Animations";
        public const string SPRITE_PATH = @"C:\Users\franc\source\repos\Heros-Die-Last\Rogue\bin\DesktopGL\AnyCPU\Debug\Content";

        private WpfCell Cell
        {
            get;
            set;
        }
        private AnimationPlayer Player
        {
            get;
            set;
        }
        private Editor Editor
        {
            get;
            set;
        }
        public AddAnimation(WpfCell cell, Editor editor)
        {
            this.Cell = cell;
            this.Editor = editor;
            InitializeComponent();

        }
        private AnimationPlayer AnimationPlayer
        {
            get;
            set;
        }

      
        public static string GetSpritePath(string spriteName)
        {
            string[] files = Directory.GetFiles(SPRITE_PATH,
            "*.*",
            SearchOption.AllDirectories);

            foreach (var file in files)
            {
                if (System.IO.Path.GetFileNameWithoutExtension(file) == spriteName)
                    return file;
            }
            throw new FileNotFoundException();
        }
        public static AnimationElementTemplate GetAnimationElementTemplate(string name)
        {
            AnimationTemplate template = new AnimationTemplate();

            if (template.Load(ANIMATIONS_PATH + "/" + name + ".anm"))
            {
                return template.Elements[DirectionEnum.None];
            }
            else
            {
                return null;
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Cell.Layers[Editor.DrawingLayer].SetAnimation(animationName.Text);
            this.Close();
        }

        private void AnimationName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Player != null)
            {
                Player.Dispose();
            }

            var template = GetAnimationElementTemplate(animationName.Text);

            if (template != null && template.IsDefine)
            {
                var sprites = template.SpriteNames.Select(x => GetSpritePath(x)).ToArray();
                Player = new AnimationPlayer(sprites, preview, template.FlipVertical, template.FlipHorizontal);
                Player.Play(template.Delay);
            }
        }
    }
}
