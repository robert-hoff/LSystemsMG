using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Windows.Forms.Design;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace GGJ_Ideas_and_Monogame_trials
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
        }

        protected override void Initialize()
        {
            // Vector3 v3Position = new Vector3(0, 0, 0);
            // Matrix.CreateTranslation(v3Position);





            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Content = new ContentManager(this.Services, "Content");

            // _spriteBatch; needed to draw textures?
            _spriteBatch = new SpriteBatch(GraphicsDevice);


            // Model model = Content.Load<Model>("SimpleShip\\Ship.fbx");

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
            GraphicsDevice.Clear(Color.CornflowerBlue);



            base.Draw(gameTime);
        }



        // 'using' statement in Program.cs ensures call at application close
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            // Debug.WriteLine($"disposed was called");
        }
    }
}
