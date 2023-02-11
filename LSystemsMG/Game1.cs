using System.Diagnostics;
using System.Runtime.ExceptionServices;
using Environment;
using LSystemsMG.Environment;
using LSystemsMG.ModelRendering;
using LSystemsMG.ModelSceneGraph;
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
        public readonly static bool SHOW_AXIS = true;
        public readonly static bool RESTRICT_CAMERA = false;
        // --

        private Color CLEAR_COLOR = Color.CornflowerBlue; // Using CornflowerBlue, Black, White
        private const int DEFAULT_VIEWPORT_WIDTH = 1400;
        private const int DEFAULT_VIEWPORT_HEIGHT = 800;
        private CameraTransforms cameraTransforms;


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
        }

        protected override void Initialize()
        {
            GraphicsDevice.PresentationParameters.MultiSampleCount = 2;
            base.Initialize();
        }


        // -- models and model collections
        // private ModelLinePrimitive modelLine;
        private ModelAxisPrimitive modelAxis;

        private TerrainRenderer terrainRenderer;

        private Model modelCubeWedge0;
        private Model modelCubeWedge1;
        private GroundTiles groundTiles;

        private GameModel modelFern;
        private GameModel modelReeds1;
        private GameModel modelAcaciaTree1;
        private GameModel modePineTree3;

        private GameModelRegister gameModelRegister;
        private GameModelRegister2 gameModelRegister2;



        protected override void LoadContent()
        {
            gameModelRegister = new GameModelRegister(cameraTransforms);
            gameModelRegister2 = new GameModelRegister2(cameraTransforms);

            Content = new ContentManager(this.Services, "Content");

            terrainRenderer = new TerrainRenderer(Content, cameraTransforms);

            // modelLine = new ModelLinePrimitive(GraphicsDevice, cameraTransforms);
            modelAxis = new ModelAxisPrimitive(GraphicsDevice, cameraTransforms);

            RegisterModel("various/skybox");
            RegisterModel("plants/reeds1");
            RegisterModel("polygon-nature/polygon-plant1");
            RegisterModel("polygon-nature/polygon-plant2");
            RegisterModel("polygon-nature/polygon-plant3");
            RegisterModel("trees/acaciatree1");
            RegisterModel("trees/acaciatree2");
            RegisterModel("trees/birchtree1");
            RegisterModel("trees/birchtree2");
            RegisterModel("trees/pinetree1");
            RegisterModel("trees/pinetree2");
            RegisterModel("trees/pinetree3");
            RegisterModel("rocks/rocktile1");
            RegisterModel("plants/plant-example");
            RegisterModel("trees/tree-example");

            modelReeds1 = gameModelRegister.GetGameModel("reeds1");
            modelAcaciaTree1 = gameModelRegister.GetGameModel("acaciatree1");
            modePineTree3 = gameModelRegister.GetGameModel("pinetree3");

            // modelCubeWedge0 = Content.Load<Model>("geometries/cube-wedge0");
            // modelCubeWedge1 = Content.Load<Model>("geometries/cube-wedge1");
            // groundTiles = new GroundTiles(cameraTransforms, modelCubeWedge0, modelCubeWedge1);

            RegisterModel("geometries/unitcube");
            LoadSelectedModels();
        }

        private GameModel RegisterModel(string modelPathName)
        {
            Model model = Content.Load<Model>(modelPathName);
            string modelName = modelPathName.Substring(modelPathName.IndexOf('/') + 1);
            gameModelRegister2.RegisterModel(modelName, model);
            return gameModelRegister.LoadModelFromFile(modelName, model);
        }

        private int previousMouseScroll = 0;
        private int mouseDragX = 0;
        private int mouseDragY = 0;
        private bool leftMouseIsReleased = true;


        protected override void Update(GameTime gameTime)
        {
            cameraTransforms.UpdateViewportDimensions(Window.ClientBounds.Width, Window.ClientBounds.Height);

            Keys keyEvent = KeyEvent();
            if (keyEvent == Keys.Escape)
            {
                Exit();
                return;
            }
            // -- Show camera position
            if (keyEvent == Keys.P)
            {
                Debug.WriteLine($"camera position {cameraTransforms.cameraPosition}");
                Debug.WriteLine($"rotation {MathHelper.ToDegrees(cameraTransforms.cameraRotation)}");
                Debug.WriteLine($"distance from origin {Vector3.Distance(cameraTransforms.cameraPosition, new Vector3(0, 0, 0))}");
            }

            if (this.IsActive && Mouse.GetState().LeftButton == ButtonState.Pressed)
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

        private bool[] keyIsDown = new bool[100];
        private Keys KeyEvent()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                return Keys.Escape;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.P) && !keyIsDown[(int) Keys.P])
            {
                keyIsDown[(int) Keys.P] = true;
                return Keys.P;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.P) && keyIsDown[(int) Keys.P])
            {
                keyIsDown[(int) Keys.P] = false;
            }
            return Keys.None;
        }


        private GameModel2 cubeModel0;
        private GameModel2 cubeModel1;
        private GameModel2 cubeModel2;

        private void LoadSelectedModels()
        {
            modelFern = gameModelRegister.GetGameModel("polygon-plant1");
            cubeModel0 = gameModelRegister2.CreateModel("unitcube");
            cubeModel1 = gameModelRegister2.CreateModel("unitcube");
            cubeModel1.SetTransform(BuildTransform.Ident().T(0.5f, 0.5f, 0f).S(5, 5, 0.25f).Get());
            cubeModel2 = gameModelRegister2.CreateModel("unitcube");
            cubeModel2.SetTransform(BuildTransform.Ident().T(-0.5f, -0.5f, 0f).S(5, 5, 0.25f).Get());
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(CLEAR_COLOR);
            gameModelRegister.GetGameModel("skybox").Draw();
            // PreviousScene(gameTime);

            Vector3 TRL1 = new Vector3(0.25f, 0.25f, 0.5f);
            float RZ1 = (float) gameTime.TotalGameTime.TotalMilliseconds / 50;

            modelFern.Draw(BuildTransform.Ident().T(1, 1, 0.25f).T(TRL1).Get());
            modelFern.Draw(BuildTransform.Ident().S(0.8f, 0.8f, 0.8f).Rz(30).Ry(25).T(4.7f, 0.3f, 0.25f).T(TRL1).Get());

            if (SHOW_AXIS)
            {
                modelAxis.Draw(BuildTransform.Ident().S(5, 5, 5).Get());
            }
            modelAxis.Draw(BuildTransform.Ident().S(0.5f, 0.5f, 0.5f).T(2.5f, 2.5f, 0).Get());

            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            // cubeModel0.Draw(BuildTransform.Ident().T(0.5f, 0.5f, 0).S(5, 5, 0.25f).Get());
            cubeModel0.Draw(BuildTransform.Ident().S(5, 5, 0.25f).T(2.5f, 2.5f, 0).Get());
            // cubeModel1.Draw();
            // cubeModel2.Draw();
            GraphicsDevice.BlendState = BlendState.Opaque;

            base.Draw(gameTime);
        }

        private void PreviousScene(GameTime gameTime)
        {
            // groundTiles.DrawGroundTiles();
            for (int i = -7; i <= 7; i++)
            {
                for (int j = -7; j <= 7; j++)
                {
                    terrainRenderer.DrawRandom(i, j);
                }
            }
            modelAcaciaTree1.Draw(BuildTransform.Ident().T(-4, -13, 0).Get());
            modelReeds1.Draw(BuildTransform.Ident().S(0.8f, 0.8f, 1.4f).Rz(40).T(4, 4, 0).Get());
            gameModelRegister.GetGameModel("polygon-plant2").Draw(BuildTransform.Ident().T(2, 2, 0).Get());
            gameModelRegister.GetGameModel("birchtree1").Draw(BuildTransform.Ident().T(-2, 4, 0).Get());
            gameModelRegister.GetGameModel("rocktile1").Draw(BuildTransform.Ident().T(2, 5, 0).Get());

            // -- spins the tree
            float rotZ = (float) gameTime.TotalGameTime.TotalMilliseconds / 50;
            modePineTree3.Draw(BuildTransform.Ident().Rz(rotZ).Get());

            // -- some one-sided models may need the CullNone setting
            GraphicsDevice.RasterizerState = RasterizerState.CullNone;
            Matrix transform1 = BuildTransform.Ident().Tx(1).Ry(-30).Rx(-130).S(2,2,2).T(2,2,0).Get();
            gameModelRegister.GetGameModel("plant-example").Draw(transform1);
            GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
        }
    }
}

