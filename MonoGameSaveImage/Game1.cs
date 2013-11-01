using Android.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MonoGameSaveImage
{
    /// <summary>
    /// Default Project Template
    /// </summary>
    public class Game1 : Game
    {

        #region Fields
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D logoTexture;
        #endregion

        TouchCollection lastTouches;

        #region Initialization

        public Game1()
        {

            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";

            graphics.IsFullScreen = true;
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
        }

        /// <summary>
        /// Overridden from the base Game.Initialize. Once the GraphicsDevice is setup,
        /// we'll use the viewport to initialize some values.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }


        /// <summary>
        /// Load your graphics content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be use to draw textures.
            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);

            // TODO: use this.Content to load your game content here eg.
            logoTexture = Content.Load<Texture2D>("chuck");
        }

        #endregion

        #region Update and Draw

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here                        

            base.Update(gameTime);

            var touches = TouchPanel.GetState();
            if (touches.Count == 0 && lastTouches.Count > 0)
            {
                var directory = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads);

                //Jpeg
                var path = Path.Combine(directory.AbsolutePath, "chuck.jpg");
                var mimeType = "image/jpeg";
                using (var stream = File.Create(path))
                {
                    logoTexture.SaveAsJpeg(stream, logoTexture.Width, logoTexture.Height);
                }

                //Png
                //var path = Path.Combine(directory.AbsolutePath, "chuck.jpg");
                //var mimeType = "image/png";
                //using (var stream = File.Create(path))
                //{
                //    logoTexture.SaveAsPng(stream, logoTexture.Width, logoTexture.Height);
                //}

                var intent = new Intent(Intent.ActionView);
                intent.SetDataAndType(Android.Net.Uri.Parse("file://" + path), mimeType);
                Game.Activity.StartActivity(intent);
            }
            lastTouches = touches;
        }

        /// <summary>
        /// This is called when the game should draw itself. 
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // Clear the backbuffer
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            // draw the logo
            spriteBatch.Draw(logoTexture, new Vector2(0, 0), Color.White);

            spriteBatch.End();

            //TODO: Add your drawing code here
            base.Draw(gameTime);
        }

        #endregion
    }
}