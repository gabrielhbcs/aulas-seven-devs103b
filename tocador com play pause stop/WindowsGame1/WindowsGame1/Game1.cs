using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Model modelo;

        Video video;
        VideoPlayer player;

        KeyboardState teclado;
        KeyboardState a_teclado;

        bool pausado = true;
        bool parado = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            modelo = Content.Load<Model>("caixa");

            video = Content.Load<Video>("video2");

            player = new VideoPlayer();
            player.Volume = .5f;

            player.IsLooped = true;

            player.Play(video);
            player.Pause();

        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            teclado = Keyboard.GetState();
            if (teclado.IsKeyDown(Keys.P) && teclado != a_teclado)
            {
                if (!pausado && !parado)
                {
                    player.Pause();
                    pausado = true;
                }
                else if (pausado || parado)
                {
                    player.Resume();
                    pausado = false;
                    parado = false;
                }
                else if (parado)
                {
                    //player.Play();
                }
            }
            
            if (teclado.IsKeyDown(Keys.S) && teclado != a_teclado && !parado)
            {
                player.Stop();
                parado = true;
            }
            if (teclado.IsKeyDown(Keys.M) && teclado != a_teclado)
            {
                if (!player.IsMuted)
                    player.IsMuted = true;
                else player.IsMuted = false;
            }
            if (teclado.IsKeyDown(Keys.V) && teclado != a_teclado && player.Volume < .9f)
            {
                player.Volume += .1f;
            }
            if (teclado.IsKeyDown(Keys.C) && teclado != a_teclado && player.Volume > 0.1f)
            {
                player.Volume -= .1f;
            }
            a_teclado = teclado;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            BasicEffect effect = modelo.Meshes[0].Effects[0] as BasicEffect;

            effect.TextureEnabled = true;

            effect.Texture = player.GetTexture();

            GraphicsDevice.SamplerStates[0] = SamplerState.LinearClamp;

            modelo.Draw(Matrix.CreateRotationX((float)gameTime.TotalGameTime.TotalSeconds) * Matrix.CreateRotationY((float)gameTime.TotalGameTime.TotalSeconds / 2) * Matrix.CreateScale(3.0f),
                        Matrix.CreateLookAt(new Vector3(5, 3, -5), Vector3.Zero, Vector3.Up),
                        Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, GraphicsDevice.Viewport.AspectRatio, 1f, 100f));
            

            base.Draw(gameTime);
        }
    }
}
