using System.Diagnostics;
using System.Runtime.ExceptionServices;
using LSystemsMG.Environment;
using LSystemsMG.ModelRendering;
using LSystemsMG.Primitives;
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
namespace LSystemsMG
{
    public class Game1 : Game
    {
        // dev flags
        // --
        public readonly static bool SHOW_AXIS = false;
        public readonly static bool RESTRICT_CAMERA = false;
        // --

        private Color CLEAR_COLOR = Color.CornflowerBlue; // Using CornflowerBlue, Black, White
        private const int DEFAULT_VIEWPORT_WIDTH = 1400;
        private const int DEFAULT_VIEWPORT_HEIGHT = 800;
        private CameraTransforms cameraTransforms;

        private DrawLine drawLine;
        private Model modelSkybox;
        private Model modelAcaciaTree1;
        private Model modelAcaciaTree2;
        private Model modelPolygonPlant1;
        private Model modelPolygonPlant2;
        private Model modelPolygonPlant5;

        private Model modelReeds1;
        private Model modelTree1;
        private Model modelRockTile1;

        private Model testModel;

        private GameModelRegister gameModelRegister;
        private TerrainRenderer terrainRenderer;
        private RenderModel renderModel;

        public Game1()
        {
            Content.RootDirectory = "Content";
            Window.Title = "LSystemsMG";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;

            _ = new GraphicsDeviceManager(this)
            {
                IsFullScreen = false,
                PreferredBackBufferWidth = DEFAULT_VIEWPORT_WIDTH,
                PreferredBackBufferHeight = DEFAULT_VIEWPORT_HEIGHT,
                PreferredBackBufferFormat = SurfaceFormat.Color,
                PreferMultiSampling = true,
                // ! don't turn off the depth setting
                PreferredDepthStencilFormat = DepthFormat.Depth24,
                SynchronizeWithVerticalRetrace = true,
            };
            int screenWidth = Window.ClientBounds.Width;
            int screenHeight = Window.ClientBounds.Height;
            cameraTransforms = new CameraTransforms(screenWidth, screenHeight);
            gameModelRegister = new GameModelRegister(cameraTransforms);
        }

        protected override void Initialize()
        {
            GraphicsDevice.PresentationParameters.MultiSampleCount = 2;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Content = new ContentManager(this.Services, "Content");
            modelSkybox = Content.Load<Model>("skybox");
            modelAcaciaTree1 = Content.Load<Model>("acaciaTree1");
            modelAcaciaTree2 = Content.Load<Model>("acaciaTree2");
            modelPolygonPlant1 = Content.Load<Model>("polygon-nature/SM_Plant_01");
            modelPolygonPlant2 = Content.Load<Model>("polygon-nature/SM_Plant_02");
            modelPolygonPlant5 = Content.Load<Model>("polygon-nature/SM_Plant_05");
            modelReeds1 = Content.Load<Model>("plants/reeds1");
            modelTree1 = Content.Load<Model>("trees/tree1");
            modelRockTile1 = Content.Load<Model>("rocks/tile000");

            terrainRenderer = new TerrainRenderer(Content, cameraTransforms);
            gameModelRegister.RegisterGameModel("skybox", modelSkybox);
            gameModelRegister.RegisterGameModel("acaciaTree1", modelAcaciaTree1);
            gameModelRegister.RegisterGameModel("acaciaTree2", modelAcaciaTree2);
            gameModelRegister.RegisterGameModel("polygonPlant1", modelPolygonPlant1);
            gameModelRegister.RegisterGameModel("polygonPlant2", modelPolygonPlant2);
            gameModelRegister.RegisterGameModel("polygonPlant5", modelPolygonPlant5);

            renderModel = new RenderModel(cameraTransforms);
            testModel = Content.Load<Model>("plants/testobject");
        }

        private int previousMouseScroll = 0;
        private int mouseDragX = 0;
        private int mouseDragY = 0;
        private bool leftMouseIsReleased = true;

        protected override void Update(GameTime gameTime)
        {
            cameraTransforms.UpdateViewportDimensions(Window.ClientBounds.Width, Window.ClientBounds.Height);

            // TESTING ONLY, print camera position
            if (Keyboard.GetState().IsKeyDown(Keys.P))
            {
                Debug.WriteLine($"camera position {cameraTransforms.cameraPosition}");
                Debug.WriteLine($"rotation {MathHelper.ToDegrees(cameraTransforms.cameraRotation)}");
                Debug.WriteLine($"distance from origin {Vector3.Distance(cameraTransforms.cameraPosition, new Vector3(0, 0, 0))}");
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                if (leftMouseIsReleased)
                {
                    leftMouseIsReleased = false;
                    mouseDragX = Mouse.GetState().X;
                    mouseDragY = Mouse.GetState().Y;
                }
                else
                {
                    float diffX = Mouse.GetState().X - mouseDragX;
                    float diffY = Mouse.GetState().Y - mouseDragY;
                    cameraTransforms.IncrementCameraOrbitDegrees(diffX / 4);
                    cameraTransforms.OrbitUpDown(diffY / 20);
                    mouseDragX = Mouse.GetState().X;
                    mouseDragY = Mouse.GetState().Y;
                }
            }
            if (Mouse.GetState().LeftButton == ButtonState.Released)
            {
                leftMouseIsReleased = true;
            }

            int currentMouseScroll = Mouse.GetState().ScrollWheelValue;
            if (previousMouseScroll > currentMouseScroll)
            {
                cameraTransforms.ZoomOut();
                previousMouseScroll = currentMouseScroll;
            }
            if (previousMouseScroll < currentMouseScroll)
            {
                cameraTransforms.ZoomIn();
                previousMouseScroll = currentMouseScroll;
            }

            base.Update(gameTime);
        }


        // float rotate = 0;

        protected override void Draw(GameTime gameTime)
        {
            // rotate += 1f;
            // Debug.WriteLine($"{rotate}");


            GraphicsDevice.Clear(CLEAR_COLOR);
            gameModelRegister.GetGameModel("skybox").DrawModelWithDefaultValues(cameraTransforms, ModelTransforms.Scale(1000, 1000, 1000));
            for (int i = -7; i <= 7; i++)
            {
                for (int j = -7; j <= 7; j++)
                {
                    terrainRenderer.DrawRandom(i, j);
                }
            }


            gameModelRegister.GetGameModel("acaciaTree1").DrawModelWithDefaultValues(cameraTransforms, ModelTransforms.Translation(-4, -13, -0.5f));
            gameModelRegister.GetGameModel("polygonPlant2").DrawModelWithDefaultValues(cameraTransforms, ModelTransforms.Translation(0, 0, 0));
            gameModelRegister.GetGameModel("polygonPlant5").DrawModelWithDefaultValues(cameraTransforms, ModelTransforms.Translation(2, 2, 0));

            // fixme - rotation is a bit messed up
            // gameModelRegister.GetGameModel("acaciaTree2").DrawModelWithDefaultValues(cameraTransforms, ModelTransforms.Translation(-2, 4, 0));


            renderModel.R(40).S(2f, 2f, 2f).T(4, 4, 0).Draw(modelReeds1);
            renderModel.T(8, -9, 0).Draw(modelTree1);
            renderModel.T(2, 5, 0).Draw(modelRockTile1);

            renderModel.T(5, 5, 0).Draw(testModel);



            if (SHOW_AXIS)
            {
                drawLine.DrawAxis(GraphicsDevice);
            }
            base.Draw(gameTime);
        }
    }
}


