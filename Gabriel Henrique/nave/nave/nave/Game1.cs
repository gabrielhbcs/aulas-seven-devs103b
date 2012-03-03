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

namespace nave
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Model nave, inimigo;

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
            nave = Content.Load<Model>("Space Shooter/Space_Shooter");
            inimigo = Content.Load<Model>("Alien Probe/FBX/probe");

            spriteBatch = new SpriteBatch(GraphicsDevice);

        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            BasicEffect effect = nave.Meshes[0].Effects[0] as BasicEffect;
            effect.TextureEnabled = true;
            nave.Draw(Matrix.CreateScale(0.3f)*Matrix.CreateRotationY((float)gameTime.TotalGameTime.TotalSeconds),
                Matrix.CreateLookAt(new Vector3(0, 0, 10), new Vector3(-5, 0, 0), Vector3.Up),
                Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, GraphicsDevice.Viewport.AspectRatio, 1f, 100f));

            BasicEffect effect2 = inimigo.Meshes[0].Effects[0] as BasicEffect;
            effect2.TextureEnabled = true;
            inimigo.Draw(Matrix.CreateScale(.5f) * Matrix.CreateRotationY(-(float)gameTime.TotalGameTime.TotalSeconds),
                Matrix.CreateLookAt(new Vector3(0, 0, 10), new Vector3(5, 0, 0), Vector3.Up),
                Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, GraphicsDevice.Viewport.AspectRatio, 1f, 100f));



            
            base.Draw(gameTime);
        }
    }
}
