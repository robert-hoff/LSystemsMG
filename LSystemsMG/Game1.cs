using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using LSystemsMG.ModelFactory;
using LSystemsMG.ModelRendering;
using LSystemsMG.ModelRendering.TestScenes;

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
        // dev settings
        // --
        private readonly static bool SHOW_AXIS = true;
        public readonly static bool RESTRICT_CAMERA = false;
        // --

        // -- cameraTransform is updated on changes in viewport and used to draw all models
        private CameraTransform cameraTransform;
        // -- models are instantiated on this class
        private GameModelRegister gameModelRegister;
        // -- models are assigned to sceneGraph as members of SceneGraphNode
        private SceneGraph sceneGraph;

        public Game1()
        {
            Content.RootDirectory = "Content";
            Window.Title = "LSystemsMG";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            const int DEFAULT_VIEWPORT_WIDTH = 1400;
            const int DEFAULT_VIEWPORT_HEIGHT = 800;

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
            cameraTransform = new CameraTransform(screenWidth, screenHeight);
        }

        protected override void Initialize()
        {
            GraphicsDevice.PresentationParameters.MultiSampleCount = 2;
            base.Initialize();
        }

        private void RegisterModel(string modelPathName)
        {
            gameModelRegister.RegisterModelFbx(
                modelName: modelPathName.Substring(modelPathName.IndexOf('/') + 1),
                model: Content.Load<Model>(modelPathName)
            );
        }
        protected override void LoadContent()
        {
            gameModelRegister = new GameModelRegister(GraphicsDevice, cameraTransform);
            Content = new ContentManager(this.Services, "Content");
            // -- models based off primitives
            gameModelRegister.RegisterModelPrimitive("axismodel");
            // -- fbx models
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
            RegisterModel("geometries/unitcube");

            // -- create scenes by extending SceneGraph,
            // expressing model placement and scene updates
            // sceneGraph = new Scene01RotatingPlatform(gameModelRegister);
            sceneGraph = new Scene02PlantsAndTerrain(gameModelRegister);

            if (SHOW_AXIS)
            {
                sceneGraph.ShowWorldAxes(true, axesLen: 5);
            }
        }

        private int previousMouseScroll = 0;
        private int mouseDragX = 0;
        private int mouseDragY = 0;
        private bool leftMouseIsReleased = true;

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
            if (Keyboard.GetState().IsKeyDown(Keys.S) && !keyIsDown[(int) Keys.S])
            {
                keyIsDown[(int) Keys.S] = true;
                return Keys.S;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.S) && keyIsDown[(int) Keys.S])
            {
                keyIsDown[(int) Keys.S] = false;
            }
            return Keys.None;
        }

        protected override void Update(GameTime gameTime)
        {
            cameraTransform.UpdateViewportDimensions(Window.ClientBounds.Width, Window.ClientBounds.Height);

            Keys keyEvent = KeyEvent();
            if (keyEvent == Keys.Escape)
            {
                Exit();
                return;
            }
            // -- Show camera position
            if (keyEvent == Keys.P)
            {
                Debug.WriteLine($"camera position {cameraTransform.cameraPosition}");
                Debug.WriteLine($"rotation {MathHelper.ToDegrees(cameraTransform.cameraRotation)}");
                Debug.WriteLine($"distance from origin {Vector3.Distance(cameraTransform.cameraPosition, new Vector3(0, 0, 0))}");
            }
            if (keyEvent == Keys.S)
            {
                Debug.WriteLine($"Keys.S (not assigned)");
            }
            if (Keyboard.GetState().IsKeyDown(Keys.T))
            {
                Debug.WriteLine($"Keys.T (not assigned, fires continuously)");
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
                    cameraTransform.IncrementCameraOrbitDegrees(diffX / 4);
                    cameraTransform.OrbitUpDown(diffY / 20);
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
                cameraTransform.ZoomOut();
                previousMouseScroll = currentMouseScroll;
            }
            if (previousMouseScroll < currentMouseScroll)
            {
                cameraTransform.ZoomIn();
                previousMouseScroll = currentMouseScroll;
            }

            // -- scene graph
            sceneGraph.Update(gameTime);
            sceneGraph.UpdateTransforms();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // FIXME - need to toggle this on the models that require it
            // GraphicsDevice.BlendState = BlendState.AlphaBlend;
            // GraphicsDevice.BlendState = BlendState.NonPremultiplied;
            // GraphicsDevice.RasterizerState = RasterizerState.CullNone;

            sceneGraph.Draw(GraphicsDevice);
            // PreviousScene(gameTime);

            base.Draw(gameTime);
        }
    }
}

