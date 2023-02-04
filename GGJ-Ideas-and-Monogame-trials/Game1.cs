using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace GGJ_Ideas_and_Monogame_trials
{
    public class Game1 : Game
    {
        // GraphicsDeviceManager graphics;
        // SpriteBatch spriteBatch;

        private Model model;
        private Matrix world = Matrix.CreateTranslation(new Vector3(0, 0, 0));
        private Matrix view = Matrix.CreateLookAt(new Vector3(0, 0, 5), new Vector3(0, 0, 0), Vector3.UnitY);
        private Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathF.PI / 4, 1.6f, 0.1f, 100f);


        public Game1()
        {
            // R: this doesn't need to be assigned
            _ = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // spriteBatch - needed to draw textures?
            _ = new SpriteBatch(GraphicsDevice);

            Content = new ContentManager(this.Services, "Content");
            model = Content.Load<Model>("ship-no-texture");
        }



        /*
         *
         * Notes
         * -----
         * Called on each game-loop
         * Target fps (default) = 60
         *
         * gameTime.TotalGameTime = global clock
         * gameTime.ElapsedTime, time since last update (= 1/60 seconds)
         *
         *
         *
         */
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            world = Matrix.CreateRotationY((float) gameTime.TotalGameTime.TotalSeconds);
            base.Update(gameTime);
        }


        /*
         * Notes
         * -----
         * Same as Update. Called immediately after on the same thread
         *
         * attaching an object of type
         *
         *      DrawableGameComponent
         *
         *
         * will have its Draw(GameTime gametime) method implicitly called at this step (needs to be registered)
         *
         *
         *
         *
         *
         */
        protected override void Draw(GameTime gameTime)
        {
            // GraphicsDevice.Clear(Color.Black);
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // GraphicsDevice and graphics.GraphicsDevice is the same method
            // graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
            DrawModel(model, world, view, projection);
            base.Draw(gameTime);
        }

        private void DrawModel(Model model, Matrix world, Matrix view, Matrix projection)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = world;
                    effect.View = view;
                    effect.Projection = projection;
                }
                mesh.Draw();
            }
        }


        // 'using' statement in Program.cs ensures call to Dispose(..) at application close
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            // Debug.WriteLine($"disposed was called");
        }

    }
}
