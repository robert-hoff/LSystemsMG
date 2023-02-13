using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using LSystemsMG.ModelFactory;
using LSystemsMG.ModelRendering;
using LSystemsMG.ModelSpecial;

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
        private CameraTransform cameraTransform;

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
            cameraTransform = new CameraTransform(screenWidth, screenHeight);
        }

        protected override void Initialize()
        {
            GraphicsDevice.PresentationParameters.MultiSampleCount = 2;
            base.Initialize();
        }

        // -- special models
        private TerrainRenderer terrainRenderer;
        // private Model modelCubeWedge0;
        // private Model modelCubeWedge1;
        // private GroundTiles groundTiles;

        // -- model factory
        private GameModelRegister gameModelRegister;

        protected override void LoadContent()
        {
            Content = new ContentManager(this.Services, "Content");
            LoadSpecialObjects();
            // modelCubeWedge0 = Content.Load<Model>("geometries/cube-wedge0");
            // modelCubeWedge1 = Content.Load<Model>("geometries/cube-wedge1");
            // groundTiles = new GroundTiles(cameraTransform, modelCubeWedge0, modelCubeWedge1);

            gameModelRegister = new GameModelRegister(GraphicsDevice, cameraTransform);
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
            // -- primitve models
            gameModelRegister.RegisterModelPrimitive("axismodel");

            InstantiateSceneGraph();
        }

        private void RegisterModel(string modelPathName)
        {
            Model model = Content.Load<Model>(modelPathName);
            string modelName = modelPathName.Substring(modelPathName.IndexOf('/') + 1);
            gameModelRegister.RegisterModelFbx(modelName, model);
        }

        private void LoadSpecialObjects()
        {
            terrainRenderer = new TerrainRenderer(Content, cameraTransform);
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
                Debug.WriteLine($"not assigned");
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




            // -- update specific to the demo scene
            // float rotZ = (float) gameTime.TotalGameTime.TotalMilliseconds / 50;
            // float rotY = (float) gameTime.TotalGameTime.TotalMilliseconds / 50;
            // sceneGraph.cubeBaseCoordFrame.SetTransform(Transforms.Ident().Rz(rotZ).Ry(rotY).T(2.5f, 2.5f, 0).Get());
            // sceneGraph.UpdateTransformations();

            if (counter == 0)
            {
                // sceneGraph.cubeBaseCoordFrame.AppendTransform(Transforms.RotZ(15));
                // sceneGraph.cubeBaseCoordFrame.AppendTransform(Transforms.RotY(15));
                // sceneGraph.cubeBaseCoordFrame.AppendTransform(Transforms.Translate(2.5f, 2.5f, 0));
                sceneGraph.plantsCoordFrame.AppendTransform(Transforms.Translate(0.1f, 0.1f, 0));
                Debug.WriteLine($"--");
            }


            sceneGraph.UpdateTransformations();
            counter++;
            base.Update(gameTime);
        }

        private int counter = 0;


        private GameModel skybox;
        private GameModel modelReeds1;
        private GameModel modelAcaciaTree1;
        private GameModel modePineTree3;
        private GameModel modelPolygonPlant2;
        private GameModel modelBirchTree1;
        private GameModel modelRockTile1;
        private GameModel modelOneSidedFlower;

        //private GameModel cubeModel0;
        //private GameModel cubeModel1;
        //private GameModel cubeModel2;
        //private GameModel axisModel0;
        //private GameModel axisModel1;
        //private GameModel modelFern0;
        //private GameModel modelFern1;

        SceneGraph sceneGraph;

        /*
         * not a scene graph yet, experimenting with transforms ..
         *
         */
        private void InstantiateSceneGraph()
        {
            skybox = gameModelRegister.CreateModel("skybox");

            // -- previous scene models
            modelReeds1 = gameModelRegister.CreateModel("reeds1");
            modelAcaciaTree1 = gameModelRegister.CreateModel("acaciatree1");
            modePineTree3 = gameModelRegister.CreateModel("pinetree3");
            modelPolygonPlant2 = gameModelRegister.CreateModel("polygon-plant2");
            modelBirchTree1 = gameModelRegister.CreateModel("birchtree1");
            modelRockTile1 = gameModelRegister.CreateModel("rocktile1");
            modelOneSidedFlower = gameModelRegister.CreateModel("plant-example");


            sceneGraph = new SceneGraph(gameModelRegister);
            sceneGraph.ShowWorldAxes(true, axesLen: 3);

            // Matrix t = sceneGraph.cubeBaseCoordFrame.coordinateFrameTransform;
            // int i = 0;



            /*
            // -- models for scene graph testing
            axisModel0 = gameModelRegister.CreateModel("axismodel");
            axisModel1 = gameModelRegister.CreateModel("axismodel");
            cubeModel0 = gameModelRegister.CreateModel("unitcube");
            cubeModel1 = gameModelRegister.CreateModel("unitcube");
            cubeModel2 = gameModelRegister.CreateModel("unitcube");
            modelFern0 = gameModelRegister.CreateModel("polygon-plant1");
            modelFern1 = gameModelRegister.CreateModel("polygon-plant1");
            */

            /*
            cubeModel0.SetBaseTransform(Transforms.Ident().T(0.5f, 0.5f, 0).S(5, 5, 0.25f).Get());
            // cubeModel0.Draw(Transforms.Ident().S(5, 5, 0.25f).T(2.5f, 2.5f, 0).Get());
            cubeModel1.SetBaseTransform(Transforms.Ident().T(0.5f, 0.5f, 0f).S(5, 5, 0.25f).Get());
            cubeModel2.SetBaseTransform(Transforms.Ident().T(-0.5f, -0.5f, 0f).S(5, 5, 0.25f).Get());
            axisModel0.SetBaseTransform(Transforms.Ident().S(5, 5, 5).Get());
            axisModel1.SetBaseTransform(Transforms.Ident().S(0.5f, 0.5f, 0.5f).T(2.5f, 2.5f, 0).Get());
            Vector3 TRL1 = new Vector3(0, 0, 0);
            // Vector3 TRL1 = new Vector3(0.25f, 0.25f, 0.5f);
            float RZ1 = 0;
            // float RZ1 = (float) gameTime.TotalGameTime.TotalMilliseconds / 50;
            modelFern0.SetBaseTransform(Transforms.Ident().T(1, 1, 0.25f).T(TRL1).Get());
            modelFern1.SetBaseTransform(Transforms.Ident().S(0.8f, 0.8f, 0.8f).Rz(RZ1).Ry(25).T(4.7f, 0.3f, 0.25f).T(TRL1).Get());
            */

            /*
            Vector3 T1 = new Vector3(0, 0, 0);
            float RZ1 = 0;

            axisModel0.SetBaseTransform(Transforms.Ident().S(1).Get());
            cubeModel0.SetBaseTransform(Transforms.Ident().S(5, 5, 0.25f).Get());
            // axisModel1.SetBaseTransform(Transforms.Ident().S(1f).Tz(0.25f).Get());
            axisModel1.SetBaseTransform(Transforms.Ident().S(1f).T(-2.5f, -2.5f, 0.25f).Get());

            // modelFern1.SetBaseTransform(Transforms.Ident().S(0.8f, 0.8f, 0.8f).Rz(RZ1).Ry(25).T(4.7f, 0.3f, 0.25f).T(T1).Get());
            // modelFern1.SetBaseTransform(Transforms.Ident().S(0.8f, 0.8f, 0.8f).Rz(RZ1).Ry(25).T(2.2f, -2.2f, 0.25f).T(T1).Get());
            // Matrix fernTransform = Transforms.Ident().S(0.8f, 0.8f, 0.8f).Ry(25).T(2.2f, -2.2f, 0.25f).Get();

            Matrix fernTransform = Transforms.Ident().S(0.8f, 0.8f, 0.8f).Ry(25).T(1, 1, 0.25f).Get();
            modelFern1.SetBaseTransform(fernTransform);
            */
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            // GraphicsDevice.BlendState = BlendState.NonPremultiplied;
            GraphicsDevice.Clear(CLEAR_COLOR);
            skybox.Draw();


            sceneGraph.Draw();

            // PreviousScene(gameTime);

            /*
            // modelFern0.Draw();
            modelFern1.Draw();

            if (SHOW_AXIS)
            {
                axisModel0.Draw();
            }
            axisModel1.Draw();

            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            cubeModel0.Draw();
            // cubeModel1.Draw();
            // cubeModel2.Draw();
            GraphicsDevice.BlendState = BlendState.Opaque;
            */

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

            modelAcaciaTree1.SetTransform(Transforms.Ident().T(-4, -13, 0).Get());
            modelAcaciaTree1.Draw();
            modelReeds1.SetTransform(Transforms.Ident().S(0.8f, 0.8f, 1.4f).Rz(40).T(4, 4, 0).Get());
            modelReeds1.Draw();

            modelPolygonPlant2.SetTransform(Transforms.Ident().T(2, 2, 0).Get());
            modelPolygonPlant2.Draw();
            modelBirchTree1.SetTransform(Transforms.Ident().T(-2, 4, 0).Get());
            modelBirchTree1.Draw();
            modelRockTile1.SetTransform(Transforms.Ident().T(2, 5, 0).Get());
            modelRockTile1.Draw();

            // -- spins the tree
            // float rotZ = (float) gameTime.TotalGameTime.TotalMilliseconds / 50;
            // -- stationary tree
            // float rotZ = 0;
            // modePineTree3.SetTransform(Transforms.Ident().Rz(rotZ).Get());
            // modePineTree3.Draw();

            // -- some one-sided models may need the CullNone setting
            GraphicsDevice.RasterizerState = RasterizerState.CullNone;
            modelOneSidedFlower.SetTransform(Transforms.Ident().Tx(1).Ry(-30).Rx(-130).S(2, 2, 2).T(3, 2, 0).Get());
            modelOneSidedFlower.Draw();
            GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
        }
    }
}

