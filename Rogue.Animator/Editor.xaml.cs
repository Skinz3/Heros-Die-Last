using Microsoft.Win32;
using Rogue.Core;
using Rogue.Core.Collisions;
using Rogue.Core.IO.Animations;
using Rogue.WorldEditor;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Rogue.Animator
{
    /// <summary>
    /// Logique d'interaction pour Editor.xaml
    /// </summary>
    public partial class Editor : Window
    {
        private AnimationTemplate CurrentAnimation
        {
            get;
            set;
        }
        private AnimationElementTemplate CurrentAnimationElement
        {
            get
            {
                return CurrentAnimation == null ? null : CurrentAnimation.Elements[CurrentDirection];
            }
        }
        private DirectionEnum CurrentDirection
        {
            get
            {
                return (DirectionEnum)directions.SelectedItem;
            }
        }
        private AnimationPlayer Player
        {
            get;
            set;
        }
        public Editor()
        {
            InitializeComponent();

            foreach (var direction in typeof(DirectionEnum).GetEnumValues())
            {
                directions.Items.Add(direction);
            }
            directions.SelectedItem = DirectionEnum.None;
            CurrentAnimation = AnimationTemplate.New();
        }

        private void ResetUI() // todo reset UI
        {
            fhorizontal.IsChecked = false;
            fvertical.IsChecked = false;
            loop.IsChecked = true;
            frames.Items.Clear();
            delay.Text = "100";

            if (Player != null)
                Player.Dispose();
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void PlayPreview()
        {
            if (CurrentAnimationElement != null && CurrentAnimationElement.IsDefine)
            {
                if (Player != null)
                    Player.Dispose();

                var sprites = CurrentAnimationElement.SpriteNames.Select(x => Utils.GetSpritePath(x)).ToArray();

                Player = new AnimationPlayer(sprites, preview, fvertical.IsChecked.Value, fhorizontal.IsChecked.Value);
                Player.Play(int.Parse(delay.Text));
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "PNG files (*.png) | *.png";
            dialog.Multiselect = true;
            var result = dialog.ShowDialog();

            if (result.Value)
            {
                CurrentAnimationElement.SpriteNames = Array.ConvertAll(dialog.FileNames, x => System.IO.Path.GetFileNameWithoutExtension(x));

                Synchronize();

                frames.Items.Clear();

                foreach (var spriteName in CurrentAnimation.Elements[CurrentDirection].SpriteNames)
                {
                    frames.Items.Add(spriteName);
                }

                PlayPreview();
            }
        }

        private void Synchronize()
        {
            if (CurrentAnimationElement != null)
            {
                CurrentAnimationElement.Delay = short.Parse(delay.Text);
                CurrentAnimationElement.FlipHorizontal = fhorizontal.IsChecked.Value;
                CurrentAnimationElement.FlipVertical = fvertical.IsChecked.Value;
                CurrentAnimationElement.Loop = loop.IsChecked.Value;
            }
        }
        private void OnDelayInputChange(object sender, TextChangedEventArgs e)
        {
            if (delay.Text != string.Empty && delay.Text != "0")
            {
                Synchronize();
                PlayPreview();
            }
            else
                Player?.Dispose();
        }

        private void Fhorizontal_Click(object sender, RoutedEventArgs e)
        {
            Synchronize();
            PlayPreview();
        }

        private void Fvertical_Click(object sender, RoutedEventArgs e)
        {
            Synchronize();
            PlayPreview();
        }

        private void Directions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CurrentAnimationElement != null && CurrentAnimationElement.IsDefine)
            {
                fhorizontal.IsChecked = CurrentAnimationElement.FlipHorizontal;
                fvertical.IsChecked = CurrentAnimationElement.FlipVertical;
                delay.Text = CurrentAnimationElement.Delay.ToString();
                loop.IsChecked = CurrentAnimationElement.Loop;

                PlayPreview();

                frames.Items.Clear();

                foreach (var spriteName in CurrentAnimationElement.SpriteNames)
                {
                    frames.Items.Add(spriteName);
                }
            }
            else
            {
                ResetUI();
            }
        }

        private void NewClick(object sender, RoutedEventArgs e)
        {
            directions.SelectedItem = DirectionEnum.None;
            CurrentAnimation = AnimationTemplate.New();
            ResetUI();
            animationName.Text = "myAnimation";
        }

        private void OpenClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = Utils.DEFAULT_SAVE_PATH;
            dialog.Filter = "ANM files (*.anm) | *.anm";
            var result = dialog.ShowDialog();

            if (result.Value)
            {
                CurrentAnimation = new AnimationTemplate();
                CurrentAnimation.Load(dialog.FileName);

                fhorizontal.IsChecked = CurrentAnimationElement.FlipHorizontal;
                fvertical.IsChecked = CurrentAnimationElement.FlipVertical;
                delay.Text = CurrentAnimationElement.Delay.ToString();
                loop.IsChecked = CurrentAnimationElement.Loop;
                UpdateFrames();

                animationName.Text = CurrentAnimation.AnimationName;

                Player?.Dispose();
                PlayPreview();
            }
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new System.Windows.Forms.FolderBrowserDialog();
            saveFileDialog.SelectedPath = Utils.DEFAULT_SAVE_PATH;
            var result = saveFileDialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK && animationName.Text != "")
            {
                CurrentAnimation.AnimationName = animationName.Text;
                CurrentAnimation.Path = System.IO.Path.Combine(saveFileDialog.SelectedPath, CurrentAnimation.AnimationName + ".anm");
                CurrentAnimation.Save();
                MessageBox.Show("Animation saved successfully", "OK", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }

        private void Loop_Click(object sender, RoutedEventArgs e)
        {
            Synchronize();
        }
        private void UpdateFrames()
        {
            frames.Items.Clear();

            foreach (var sprite in CurrentAnimationElement.SpriteNames)
            {
                frames.Items.Add(System.IO.Path.GetFileNameWithoutExtension(sprite));
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (frames.SelectedItem != null)
            {
                int index = CurrentAnimationElement.SpriteNames.ToList().IndexOf(frames.SelectedItem.ToString());

                int swapIndex = index - 1;

                string val = CurrentAnimationElement.SpriteNames[index];

                if (swapIndex == -1)
                {
                    swapIndex = CurrentAnimationElement.SpriteNames.Length - 1;
                }
                CurrentAnimationElement.SpriteNames[index] = CurrentAnimationElement.SpriteNames[swapIndex];
                CurrentAnimationElement.SpriteNames[swapIndex] = val;

                UpdateFrames();
                frames.Focus();

                frames.SelectedItem = CurrentAnimationElement.SpriteNames[swapIndex];

                PlayPreview();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (frames.SelectedItem != null)
            {
                int index = CurrentAnimationElement.SpriteNames.ToList().IndexOf(frames.SelectedItem.ToString());

                int swapIndex = index + 1;

                string val = CurrentAnimationElement.SpriteNames[index];

                if (swapIndex == CurrentAnimationElement.SpriteNames.Length)
                {
                    swapIndex = 0;
                }
                CurrentAnimationElement.SpriteNames[index] = CurrentAnimationElement.SpriteNames[swapIndex];
                CurrentAnimationElement.SpriteNames[swapIndex] = val;

                UpdateFrames();

                frames.Focus();
                frames.SelectedItem = CurrentAnimationElement.SpriteNames[swapIndex];

                PlayPreview();
            }
        }
    }
}
