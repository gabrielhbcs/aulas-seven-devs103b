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

namespace Game1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Model modelo;
        Texture2D textura;
        BasicEffect efeitobasico;

        Vector3 cameraPosition;
        Vector3 lookAt;

        //BasicEffect efeito;

        VertexBuffer conjuntoVertices;

        public enum TiposEfeitoBasico
        {
            Nenhum,
            LuzesAmbiente,
            UmaLuz,
            DuasLuzes,
            TresLuzes,
            IluminacaoPadrao,
            Espetacular,
            Emissiva,
            Texturizada,
            Fumaca
        };
        TiposEfeitoBasico tipoAtual = TiposEfeitoBasico.IluminacaoPadrao;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            efeitobasico = new BasicEffect(GraphicsDevice);

            modelo = Content.Load<Model>("caixa");

            textura = Content.Load<Texture2D>("chao");

            efeitobasico.Texture = textura;
            //efeito.Texture = textura;

            efeitobasico.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4,
                                                                          GraphicsDevice.Viewport.AspectRatio,
                                                                          1.0f, 100.0f);

            cameraPosition = new Vector3(30f);

            lookAt = Vector3.Zero;

            efeitobasico.View = Matrix.CreateLookAt(cameraPosition,
                                                    lookAt,
                                                    Vector3.Up);

            if (tipoAtual == TiposEfeitoBasico.Fumaca)
            {
                modelo = Content.Load<Model>("esfera");
            }
            switch (tipoAtual)
            {
                case TiposEfeitoBasico.IluminacaoPadrao:
                    efeitobasico.EnableDefaultLighting();
                    break;
            }

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            efeitobasico.View = Matrix.CreateLookAt(cameraPosition,
                                                    lookAt,
                                                    Vector3.Up);
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            switch(tipoAtual)
            {
                case TiposEfeitoBasico.Fumaca:
                    const int NumberSpheres = 500;
                    for (int i = 0; i < NumberSpheres; i++)
                    {
                        Matrix world = Matrix.CreateTranslation(((i % 5) - 2) * 4, 0, (i / 5 - 3) * -4);
                        DesenharModelo(modelo, Matrix.Identity, efeitobasico);
                    }
                    break;

                default:
                    DesenharModelo(modelo, Matrix.CreateScale(3), efeitobasico);
                  break;
            }
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
        private void DesenharModelo(Model m, Matrix world, BasicEffect be)
        {
            be.CurrentTechnique.Passes[0].Apply();
            foreach (ModelMesh mm in m.Meshes)
            {
                foreach (ModelMeshPart mmp in mm.MeshParts)
                {
                    be.World = world;
                    GraphicsDevice.SetVertexBuffer(mmp.VertexBuffer, mmp.VertexOffset);
                    GraphicsDevice.Indices = mmp.IndexBuffer;
                    GraphicsDevice.DrawIndexedPrimitives(
                        PrimitiveType.TriangleList, 0, 0,
                        mmp.NumVertices, mmp.StartIndex, mmp.PrimitiveCount);
                }

            }

        }

        private void DesenharModelo(Model m, Matrix world, DualTextureEffect be)
        {
            be.World = world;
            be.CurrentTechnique.Passes[0].Apply();
            foreach (ModelMesh mm in m.Meshes)
            {
                foreach (ModelMeshPart mmp in mm.MeshParts)
                {
                    GraphicsDevice.SetVertexBuffer(mmp.VertexBuffer, mmp.VertexOffset);
                    GraphicsDevice.Indices = mmp.IndexBuffer;
                    GraphicsDevice.DrawIndexedPrimitives(
                        PrimitiveType.TriangleList, 0, 0,
                        mmp.NumVertices, mmp.StartIndex, mmp.PrimitiveCount);
                }

            }

        }

    }

}
