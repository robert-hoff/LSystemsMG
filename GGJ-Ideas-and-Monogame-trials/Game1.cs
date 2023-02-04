using System;
using System.Diagnostics;
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
        private const int DEFAULT_VIEWPORT_WIDTH = 800;
        private const int DEFAULT_VIEWPORT_HEIGHT = 600;
        private const float CAMERA_HEIGHT = 10f;
        private CameraTransforms cameraTransforms;

        private Model spaceshipModel;

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

            int screenWidth = Window.ClientBounds.Width;
            int screenHeight = Window.ClientBounds.Height;
            cameraTransforms = new CameraTransforms(screenWidth, screenHeight, initialCameraZ: CAMERA_HEIGHT);
        }

        private BasicEffect basicEffect;

        protected override void Initialize()
        {
            // enable these lines if using SpriteBatch
            GraphicsDevice.BlendState = BlendState.Opaque;
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;

            // polygon winding, render both faces
            GraphicsDevice.RasterizerState = RasterizerState.CullNone;


            // -- create 'basicEffect' definition for rendering primitives via buffers
            // basicEffect = new BasicEffect(GraphicsDevice);
            // basicEffect.World = world;
            // basicEffect.View = view;
            // basicEffect.Projection = projection;
            // -- enable per-polygon vertex colors
            // basicEffect.VertexColorEnabled = true;

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
            spaceshipModel = Content.Load<Model>("ship-with-texture");
        }


        private int previousMouseScroll = 0;

        protected override void Update(GameTime gameTime)
        {
            cameraTransforms.UpdateViewportDimensions(Window.ClientBounds.Width, Window.ClientBounds.Height);

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                cameraTransforms.IncrementCameraOrbitDegrees(-3);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                cameraTransforms.IncrementCameraOrbitDegrees(3);
            }

            // -- needs implementation (orbit)
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                // Debug.WriteLine($"mouse down");
            }

            // -- needs implementation (zoom)
            int currentMouseScroll = Mouse.GetState().ScrollWheelValue;
            if (previousMouseScroll > currentMouseScroll)
            {
                cameraTransforms.ZoomOut();
            }
            if (previousMouseScroll < currentMouseScroll)
            {
                cameraTransforms.ZoomIn();
            }
            previousMouseScroll = currentMouseScroll;


            // -- for the 'primitives' stuff
            // basicEffect.World = world;
            // basicEffect.Projection = projection;


            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            Matrix world = cameraTransforms.GetWorldMatrix();
            Matrix view = cameraTransforms.GetViewMatrix();
            Matrix projection = cameraTransforms.GetProjectionMatrix();


            // GraphicsDevice.Clear(Color.CornflowerBlue);
            GraphicsDevice.Clear(Color.Black);
            DrawModel(spaceshipModel, world, view, projection);


            // -- drawing primitives
            /*
            // -- key method, set the context with basicEffect definition
            basicEffect.CurrentTechnique.Passes[0].Apply();

            VertexPositionColor[] vertexList = new VertexPositionColor[3];
            vertexList[0] = new VertexPositionColor(new Vector3(0, 0, 0), Color.Green);
            vertexList[1] = new VertexPositionColor(new Vector3(0, 10f, 0), Color.Green);
            vertexList[2] = new VertexPositionColor(new Vector3(10f, 10f, 0), Color.Green);
            GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleList, vertexList, 0, 1);
            */

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
