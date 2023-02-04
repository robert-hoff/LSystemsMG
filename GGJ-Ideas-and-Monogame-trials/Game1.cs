using System;
using System.Runtime.CompilerServices;
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
 * <remarks>Initialize()</remarks>
 * - called once after constructor
 * - GraphicsDevice will be ready
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
        private Matrix view = Matrix.CreateLookAt(new Vector3(0, 0, 20), new Vector3(0, 0, 0), Vector3.UnitY);
        private Matrix projection;

        private int DEFAULT_VIEWPORT_WIDTH = 800;
        private int DEFAULT_VIEWPORT_HEIGHT = 600;
        private float FOV = MathF.PI / 4;
        private float NEAR_CLIP = 0.1f;
        private float FAR_CLIP = 100f;


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
                PreferredBackBufferWidth = DEFAULT_VIEWPORT_WIDTH,
                PreferredBackBufferHeight = DEFAULT_VIEWPORT_HEIGHT,
                PreferredBackBufferFormat = SurfaceFormat.Color,
                PreferMultiSampling = true,
                // PreferredDepthStencilFormat = DepthFormat.None,
                SynchronizeWithVerticalRetrace = true,
            };
        }

        private float ViewportAspectRatio()
        {
            return (float) Window.ClientBounds.Width / Window.ClientBounds.Height;
        }

        private float Deg(float degrees)
        {
            return degrees * MathF.PI / 180;
        }




        private BasicEffect basicEffect;

        protected override void Initialize()
        {
            // enable these lines if using SpriteBatch
            GraphicsDevice.BlendState = BlendState.Opaque;
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;

            // render both sides of polygons
            GraphicsDevice.RasterizerState = RasterizerState.CullNone;

            // -- where is BasicEffect?
            // BasicEffect.World = Matrix.CreateTranslation(new Vector3(0, 0, 0));
            basicEffect = new BasicEffect(GraphicsDevice);
            basicEffect.World = world;
            basicEffect.View = view;
            basicEffect.Projection = projection;
            // use my own vertex colors
            basicEffect.VertexColorEnabled = true;

            // basicEffect.AmbientLightColor = Vector3.One;
            // basicEffect.DirectionalLight0.Enabled = true;
            // basicEffect.DirectionalLight0.DiffuseColor = Vector3.One;
            // basicEffect.DirectionalLight0.Direction = Vector3.Normalize(Vector3.One);

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
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                // Debug.WriteLine($"mouse down");
            }

            // world = Matrix.CreateRotationY((float) gameTime.TotalGameTime.TotalSeconds);
            world = Matrix.CreateRotationY(Deg(30));
            projection = Matrix.CreatePerspectiveFieldOfView(FOV, ViewportAspectRatio(), NEAR_CLIP, FAR_CLIP);


            basicEffect.World = Matrix.CreateRotationY(Deg(30));
            basicEffect.Projection = Matrix.CreatePerspectiveFieldOfView(FOV, ViewportAspectRatio(), NEAR_CLIP, FAR_CLIP);


            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            // GraphicsDevice.Clear(Color.CornflowerBlue);
            GraphicsDevice.Clear(Color.Black);
            // DrawModel(model, world, view, projection);



            basicEffect.CurrentTechnique.Passes[0].Apply();
            VertexPositionColor[] vertexList = new VertexPositionColor[3];
            vertexList[0].Position = new Vector3(-0.5f, -0.5f, 0f);
            vertexList[0].Color = Color.Green;
            vertexList[1].Position = new Vector3(0, 0.5f, 0f);
            vertexList[1].Color = Color.Green;
            vertexList[2].Position = new Vector3(0.5f, -0.5f, 0f);
            vertexList[2].Color = Color.Green;
            // VertexBuffer vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor), 4, BufferUsage.None);
            // vertexBuffer.SetData<VertexPositionColor>(vertexList);
            // GraphicsDevice.SetVertexBuffer(vertexBuffer);
            GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleList, vertexList, 0, 1);





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
