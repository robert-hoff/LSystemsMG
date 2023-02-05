using System.Diagnostics;
using System.IO;
using RootNomics.Environment;
using RootNomics.Primitives;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RootNomics.SimulationRender;

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
namespace RootNomics
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

        private DrawLine drawLine;
        private DrawTriangle drawTriangle;
        private Model modelUnitSquare;
        private DrawCube drawCube;

        private Model spaceshipModel;
        private Model modelCubeWedge0;
        private Model modelCubeWedge1;

        private Model modelAcaciaTree1;
        private Model modelAcaciaTree2;
        private Model modelBirchTree1;
        private Model modelBirchTree2;
        private Model modelCactus1;
        private Model modelCactus2;
        private Model modelFern1;
        private Model modelFern2;
        private Model modelFlower1;
        private Model modelFlower2;
        private Model modelFlower3;
        private Model modelFlower4;
        private Model modelMushroom1;
        private Model modelMushroom2;
        private Model modelMushroom3;
        private Model modelMushroom4;
        private Model modelMushroom5;
        private Model modelMushroom6;
        private Model modelMushroom7;
        private Model modelPineTree1;
        private Model modelPineTree2;
        private Model modelPlant1;
        private Model modelReeds1;
        private Model modelSmallPlant1;
        private Model modelTombstone;
        private Model modelTerrain1;

        private GroundTiles groundTiles;
        private PlantModels fernModels;

        private SimulationRenderer simulationRenderer = new SimulationRenderer();

        public Game1()
        {
            Content.RootDirectory = "Content";
            Window.Title = "Monogame Trials";
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
            // enable these lines if using SpriteBatch
            // GraphicsDevice.BlendState = BlendState.Opaque;
            // GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            // GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;

            // -- INCLUDE BACKSIDES
            // GraphicsDevice.RasterizerState = RasterizerState.CullNone;
            // GraphicsDevice.PresentationParameters.MultiSampleCount = 2;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            drawLine = new DrawLine(GraphicsDevice, cameraTransforms);
            drawTriangle = new DrawTriangle(GraphicsDevice, cameraTransforms);
            drawCube = new DrawCube(GraphicsDevice, cameraTransforms);

            Content = new ContentManager(this.Services, "Content");
            modelUnitSquare = Content.Load<Model>("unitsquare");
            modelCubeWedge0 = Content.Load<Model>("cube-wedge0");
            modelCubeWedge1 = Content.Load<Model>("cube-wedge1");
            modelAcaciaTree1 = Content.Load<Model>("acaciaTree1");
            modelAcaciaTree2 = Content.Load<Model>("acaciaTree2");
            modelBirchTree1 = Content.Load<Model>("birchTree1");
            modelBirchTree2 = Content.Load<Model>("birchTree2");
            modelCactus1 = Content.Load<Model>("cactus1");
            modelCactus2 = Content.Load<Model>("cactus2");
            modelFern1 = Content.Load<Model>("fern1");
            modelFern2 = Content.Load<Model>("fern2");
            modelFlower1 = Content.Load<Model>("flower1");
            modelFlower2 = Content.Load<Model>("flower2");
            modelFlower3 = Content.Load<Model>("flower3");
            modelFlower4 = Content.Load<Model>("flower4");
            modelMushroom1 = Content.Load<Model>("mushroom1");
            modelMushroom2 = Content.Load<Model>("mushroom2");
            modelMushroom3 = Content.Load<Model>("mushroom3");
            modelMushroom4 = Content.Load<Model>("mushroom4");
            modelMushroom5 = Content.Load<Model>("mushroom5");
            modelMushroom6 = Content.Load<Model>("mushroom6");
            modelMushroom7 = Content.Load<Model>("mushroom7");
            modelPineTree1 = Content.Load<Model>("pineTree1");
            modelPineTree2 = Content.Load<Model>("pineTree2");
            modelSmallPlant1 = Content.Load<Model>("smallPlant1");
            modelPlant1 = Content.Load<Model>("plant1");
            modelReeds1 = Content.Load<Model>("reeds1");
            modelTombstone = Content.Load<Model>("tombstone");
            modelTerrain1 = Content.Load<Model>("terrain1");


            groundTiles = new GroundTiles(modelCubeWedge0, modelCubeWedge1);
            simulationRenderer.SetGroundTiles(groundTiles);

            simulationRenderer.RegisterCameraTransforms(cameraTransforms);
            simulationRenderer.RegisterGameModel("acaciaTree1", modelAcaciaTree1);
            simulationRenderer.RegisterGameModel("acaciaTree2", modelAcaciaTree2);
            simulationRenderer.RegisterGameModel("birchTree1", modelBirchTree1);
            simulationRenderer.RegisterGameModel("birchTree2", modelBirchTree2);
            simulationRenderer.RegisterGameModel("cactus1", modelCactus1);
            simulationRenderer.RegisterGameModel("cactus2", modelCactus2);
            simulationRenderer.RegisterGameModel("fern1", modelFern1);
            simulationRenderer.RegisterGameModel("fern2", modelFern2);
            simulationRenderer.RegisterGameModel("flower1", modelFlower1);
            simulationRenderer.RegisterGameModel("flower2", modelFlower2);
            simulationRenderer.RegisterGameModel("flower3", modelFlower3);
            simulationRenderer.RegisterGameModel("flower4", modelFlower4);
            simulationRenderer.RegisterGameModel("mushroom1", modelMushroom1);
            simulationRenderer.RegisterGameModel("mushroom2", modelMushroom2);
            simulationRenderer.RegisterGameModel("mushroom3", modelMushroom3);
            simulationRenderer.RegisterGameModel("mushroom4", modelMushroom4);
            simulationRenderer.RegisterGameModel("mushroom5", modelMushroom5);
            simulationRenderer.RegisterGameModel("mushroom6", modelMushroom6);
            simulationRenderer.RegisterGameModel("pineTree1", modelPineTree1);
            simulationRenderer.RegisterGameModel("pineTree2", modelPineTree2);
            simulationRenderer.RegisterGameModel("smallPlant1", modelSmallPlant1);
            simulationRenderer.RegisterGameModel("plant1", modelPlant1);
            simulationRenderer.RegisterGameModel("reeds1", modelReeds1);
            simulationRenderer.RegisterGameModel("terrain1", modelTerrain1);
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

        protected override void Draw(GameTime gameTime)
        {
            Matrix world = cameraTransforms.worldMatrix;
            Matrix view = cameraTransforms.viewMatrix;
            Matrix projection = cameraTransforms.projectionMatrix;
            GraphicsDevice.Clear(CLEAR_COLOR);


            // simulationRenderer.DrawGroundTiles();

            simulationRenderer.GetGameModel("acaciaTree1").DrawModelWithDefaultValues(cameraTransforms, ModelTransforms.Translation(-5,6,0));
            simulationRenderer.GetGameModel("acaciaTree2").DrawModelWithDefaultValues(cameraTransforms, ModelTransforms.Translation(-5,8,0));
            simulationRenderer.GetGameModel("birchTree1").DrawModelWithDefaultValues(cameraTransforms, ModelTransforms.Translation(-8, -8, 0));
            simulationRenderer.GetGameModel("birchTree2").DrawModelWithDefaultValues(cameraTransforms, ModelTransforms.Translation(-8, -6, 0));
            simulationRenderer.GetGameModel("cactus1").DrawModelWithDefaultValues(cameraTransforms, ModelTransforms.Translation(-8, -4, 0));
            simulationRenderer.GetGameModel("cactus2").DrawModelWithDefaultValues(cameraTransforms, ModelTransforms.Translation(-8, -2, 0));
            simulationRenderer.GetGameModel("fern1").DrawModelWithDefaultValues(cameraTransforms, ModelTransforms.Translation(-8, 0, 0));
            simulationRenderer.GetGameModel("fern2").DrawModelWithDefaultValues(cameraTransforms, ModelTransforms.Translation(-8, 2, 0));
            simulationRenderer.GetGameModel("flower1").DrawModelWithDefaultValues(cameraTransforms, ModelTransforms.Translation(-8, 2, 0));
            simulationRenderer.GetGameModel("flower2").DrawModelWithDefaultValues(cameraTransforms, ModelTransforms.Translation(-8, 2, 0));
            simulationRenderer.GetGameModel("flower3").DrawModelWithDefaultValues(cameraTransforms, ModelTransforms.Translation(-8, 2, 0));
            simulationRenderer.GetGameModel("flower4").DrawModelWithDefaultValues(cameraTransforms, ModelTransforms.Translation(-8, 2, 0));
            simulationRenderer.GetGameModel("mushroom1").DrawModelWithDefaultValues(cameraTransforms, ModelTransforms.Translation(-8, 2, 0));
            simulationRenderer.GetGameModel("mushroom2").DrawModelWithDefaultValues(cameraTransforms, ModelTransforms.Translation(-8, 4, 0));
            simulationRenderer.GetGameModel("mushroom3").DrawModelWithDefaultValues(cameraTransforms, ModelTransforms.Translation(-8, 6, 0));
            simulationRenderer.GetGameModel("mushroom4").DrawModelWithDefaultValues(cameraTransforms, ModelTransforms.Translation(-8, 8, 0));
            simulationRenderer.GetGameModel("mushroom5").DrawModelWithDefaultValues(cameraTransforms, ModelTransforms.Translation(-8, 10, 0));
            simulationRenderer.GetGameModel("mushroom6").DrawModelWithDefaultValues(cameraTransforms, ModelTransforms.Translation(-4, -8, 0));
            simulationRenderer.GetGameModel("pineTree1").DrawModelWithDefaultValues(cameraTransforms, ModelTransforms.Translation(-4, -6, 0));
            simulationRenderer.GetGameModel("pineTree2").DrawModelWithDefaultValues(cameraTransforms, ModelTransforms.Translation(-4, -4, 0));
            simulationRenderer.GetGameModel("smallPlant1").DrawModelWithDefaultValues(cameraTransforms, ModelTransforms.Translation(-4, -2, 0));
            simulationRenderer.GetGameModel("plant1").DrawModelWithDefaultValues(cameraTransforms, ModelTransforms.Translation(-4, 0, 0));
            simulationRenderer.GetGameModel("reeds1").DrawModelWithDefaultValues(cameraTransforms, ModelTransforms.Translation(-4, 2, 0));


            if (SHOW_AXIS)
            {
                drawLine.DrawAxis(GraphicsDevice);
            }
            base.Draw(gameTime);
        }
    }
}


