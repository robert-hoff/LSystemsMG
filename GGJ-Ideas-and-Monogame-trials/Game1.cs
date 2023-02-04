using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

/**
 *
 * <remarks>constructor</remarks>
 * - `new GraphicsDeviceManager(this)` attaches itself to this.GraphicsDevice
 * - removed 'spritebatch' (refer to template)
 *
 * <remarks>method Update(GameTime gameTime)</remarks>
 * Called on each game-loop
 * - Target FPS (default) = 60
 * - gameTime.TotalGameTime = global clock
 * - gameTime.ElapsedTime, time since last update (1/60 seconds)
 *
 * <remarks>method Draw(GameTime gameTime)</remarks>
 * Same as Update. Called immediately after (on the execution thread)
 * - attaching an object of type `DrawableGameComponent`
 *   will have its Draw(GameTime gametime) method implicitly called at this step (needs to be registered)
 *
 *
 *
 */
namespace GGJ_Ideas_and_Monogame_trials
{
    public class Game1 : Game
    {
        private Model model;
        private Matrix world = Matrix.CreateTranslation(new Vector3(0, 0, 0));
        private Matrix view = Matrix.CreateLookAt(new Vector3(0, 0, 5), new Vector3(0, 0, 0), Vector3.UnitY);

        private int viewportWidth = 800;
        private int viewportHeight = 600;
        private Matrix projection;

        public Game1()
        {
            Content.RootDirectory = "Content";
            Window.Title = "Rotating Spaceship";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;

            _ = new GraphicsDeviceManager(this)
            {
                // TODO - check over graphics options
                IsFullScreen = false,
                PreferredBackBufferWidth = viewportWidth,
                PreferredBackBufferHeight = viewportHeight,
                PreferredBackBufferFormat = SurfaceFormat.Color,
                PreferMultiSampling = true,
                // PreferredDepthStencilFormat = DepthFormat.None,
                SynchronizeWithVerticalRetrace = true,
            };
        }

        private float ViewportAspectRatio()
        {
            return (float) viewportWidth / viewportHeight;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Content = new ContentManager(this.Services, "Content");
            // model = Content.Load<Model>("ship-no-texture");
            model = Content.Load<Model>("ship-with-texture");
        }

        protected override void Update(GameTime gameTime)
        {
            // get window dimensions in case of resize
            viewportWidth = Window.ClientBounds.Width;
            viewportHeight = Window.ClientBounds.Height;

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            world = Matrix.CreateRotationY((float) gameTime.TotalGameTime.TotalSeconds);
            projection = Matrix.CreatePerspectiveFieldOfView(MathF.PI / 4, ViewportAspectRatio(), 0.1f, 100f);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.CornflowerBlue);
            DrawModel(model, world, view, projection);
            base.Draw(gameTime);
        }

        private void DrawModel(Model model, Matrix world, Matrix view, Matrix projection)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (Effect effect in mesh.Effects)
                {
                    BasicEffect basicEffect = (BasicEffect) effect;
                    basicEffect.World = world;
                    basicEffect.View = view;
                    basicEffect.Projection = projection;
                }
                mesh.Draw();
            }
        }
    }
}
