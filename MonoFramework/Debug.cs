using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoFramework.Scenes;
using System;
using System.Collections.Generic;

namespace MonoFramework
{
    public class Debug
    {
        public static Point CURSOR_SIZE = new Point(1, 1);

        
        /// <summary>
        /// The texture used when drawing rectangles, lines and other 
        /// primitives. This is a 1x1 white texture created at runtime.
        /// </summary>
        public static Texture2D DummyTexture
        {
            get;
            private set;
        }
        public static GraphicsDevice GraphicsDevice
        {
            get
            {
                return SpriteBatch.GraphicsDevice;
            }
        }
        private static GameCore GameCore
        {
            get;
            set;
        }
        public static SpriteBatch SpriteBatch
        {
            get
            {
                return GameCore.SpriteBatch;
            }
        }
        public static Viewport Viewport
        {
            get
            {
                return GraphicsDevice.Viewport;
            }
        }
        public static TimeSpan TargetElapsedTime
        {
            get
            {
                return GameCore.TargetElapsedTime;
            }
        }
        public static Vector2 ScreenSize
        {
            get
            {
                return new Vector2(Viewport.Width, Viewport.Height);
            }
        }
        public static Rectangle ScreenBounds
        {
            get
            {
                return Viewport.Bounds;
            }
        }
        public static ContentManager Content
        {
            get;
            private set;
        }
        public static void Initialize(GameCore gameCore, ContentManager content)
        {
            GameCore = gameCore;
            Content = content;
            DummyTexture = new Texture2D(GraphicsDevice, 1, 1);
            DummyTexture.SetData(new Color[] { Color.White });
        }

        public static void DrawLine(Vector2 start, Vector2 end, Color color, float thickness = 1f)
        {
            float length = (end - start).Length();
            float rotation = (float)Math.Atan2(end.Y - start.Y, end.X - start.X);
            SpriteBatch.Draw(DummyTexture, start, null, color, rotation, Vector2.Zero, new Vector2(length, thickness), SpriteEffects.None, 0);
        }
        public static void DrawRectangle(Rectangle rectangle, Color color, int thickness = 1)
        {
            SpriteBatch.Draw(DummyTexture, new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width, thickness), color);
            SpriteBatch.Draw(DummyTexture, new Rectangle(rectangle.Left, rectangle.Bottom, rectangle.Width, thickness), color);
            SpriteBatch.Draw(DummyTexture, new Rectangle(rectangle.Left, rectangle.Top, thickness, rectangle.Height), color);
            SpriteBatch.Draw(DummyTexture, new Rectangle(rectangle.Right, rectangle.Top, thickness, rectangle.Height + thickness), color);
        }
        public static void DrawText(Vector2 position, string text, Color color)
        {
            Debug.SpriteBatch.DrawString(SceneManager.CurrentScene.TextRenderer.GetDefaultSpriteFont(), text, position, color, 0f, new Vector2(), 1f, SpriteEffects.None, 0f);
        }
        public static void FillRectangle(Rectangle rectangle, Color color)
        {
            SpriteBatch.Draw(DummyTexture, rectangle, color);
        }
    }
}
