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

            simulationRenderer.RegisterGameModel("acaciaTree1", modelAcaciaTree1);

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


            /*
            // -- render game components
            // DrawModel2(spaceshipModel, world, view, projection);
            // DrawModel2(modelCubeWedge0, world, view, projection);
            // DrawModel2(modelCubeWedge1, world, view, projection);

            // DrawModel2(fern1Model, world, view, projection);
            // DrawModel2(plant3Model, world, view, projection);
            // DrawModel2(plant4Model, world, view, projection);
            // DrawModel2(plant5Model, world, view, projection);
            // DrawModel2(spaceshipModel, world, view, projection);
            DrawModelTranslationAndColor(modelAcaciaTree1, world, view, projection, 2, 7.3f, 0, new Vector3(0, 0.4f, 0), scaleX: 0.4f, scaleY: 0.4f, scaleZ: 0.4f, rot: 40);
            DrawModelTranslationAndColor(modelAcaciaTree1, world, view, projection, 4.5f, 8.6f, 0, new Vector3(0, 0.4f, 0), scaleX: 0.5f, scaleY: 0.5f, scaleZ: 0.5f);
            // -- models to work with
            DrawModelTranslationAndColor(modelCactus1, world, view, projection, 4, 3, 0, new Vector3(0, 0.4f, 0), scaleX: 1f, scaleY: 1f, scaleZ: 1.5f);
            DrawModelTranslationAndColor(modelCactus1, world, view, projection, 4.4f, 2.6f, 0, new Vector3(0, 0.4f, 0), scaleX: 0.5f, scaleY: 0.5f, scaleZ: 0.7f);
            DrawModelTranslationAndColor(modelCactus2, world, view, projection, 3.4f, 4.6f, 0, new Vector3(0, 0.4f, 0), scaleX: 1.5f, scaleY: 1.5f, scaleZ: 1.7f);
            DrawModelTranslationAndColor(modelReeds1, world, view, projection, -2.4f, 4.6f, 0, new Vector3(0, 0.4f, 0), scaleX: 2.5f, scaleY: 2.5f, scaleZ: 3.7f);
            DrawModelTranslationAndColor(modelTombstone, world, view, projection, -5.4f, -6.6f, 0, new Vector3(0, 0.4f, 0.5f), scaleX: 1.2f, scaleY: 1.2f, scaleZ: 1.2f, rot: 30);
            DrawModelTranslationAndColor(modelBirchTree1, world, view, projection, -4.4f, -3.6f, 0, new Vector3(0, 0.4f, 0.5f), scaleX: 1.2f, scaleY: 1.2f, scaleZ: 1.2f, rot: 30);
            DrawModelTranslationAndColor(modelTerrain1, world, view, projection, -10f, -10f, -0.4f, new Vector3(0, 0.4f, 0.5f), scaleX: 0.5f, scaleY: 0.5f, scaleZ: 0.1f, rot: 0);
            //DrawModelTranslationAndColor(modelTerrain1, world, view, projection, 0f, 10f, -0.4f, new Vector3(0, 0.4f, 0.5f), scaleX: 0.5f, scaleY: 0.5f, scaleZ: 0.1f, rot: 0);
            //DrawModelTranslationAndColor(modelTerrain1, world, view, projection, 0f, 0f, -0.4f, new Vector3(0, 0.4f, 0.5f), scaleX: 0.5f, scaleY: 0.5f, scaleZ: 0.1f, rot: 0);
            //DrawModelTranslationAndColor(modelTerrain1, world, view, projection, 10f, 0f, -0.4f, new Vector3(0, 0.4f, 0.5f), scaleX: 0.5f, scaleY: 0.5f, scaleZ: 0.1f, rot: 0);
            // DrawModel2(cactus1Model, world, view, projection);
            DrawModelTranslationAndColor(modelMushroom1, world, view, projection, 4.4f, 1f, 0, new Vector3(0, 0.4f, 0), scaleX: 1.2f, scaleY: 1.2f, scaleZ: 1.2f);
            DrawModelTranslationAndColor(modelMushroom2, world, view, projection, 4.7f, 1f, 0, new Vector3(0, 0.4f, 0), scaleX: 1.2f, scaleY: 1.2f, scaleZ: 1.2f);
            DrawModelTranslationAndColor(modelMushroom3, world, view, projection, 3.7f, 3.1f, 0, new Vector3(0, 0.4f, 0), scaleX: 1.2f, scaleY: 1.2f, scaleZ: 1.2f);
            DrawModelTranslationAndColor(modelMushroom4, world, view, projection, 2.7f, 1.5f, 0, new Vector3(0, 0.4f, 0), scaleX: 1.2f, scaleY: 1.2f, scaleZ: 1.2f);

            DrawModelTranslationAndColor(modelSmallPlant1, world, view, projection, 4.7f, -3.5f, 0, new Vector3(0, 0.4f, 0), scaleX: 2.5f, scaleY: 2.5f, scaleZ: 2.5f);

            DrawModelTranslationAndColor(modelFlower1, world, view, projection, 5.37f, -4.5f, 0, new Vector3(0, 0.4f, 0), scaleX: 2f, scaleY: 2f, scaleZ: 2f);
            */







            // -- Game simulation


            // DrawModelTranslationAndColor(modelUnitSquare, world, view, projection, 0, 0, 0f, new Vector3(0, 0.4f, 0), scaleX: 50f, scaleY: 50f);

            if (SHOW_AXIS)
            {
                drawLine.DrawAxis(GraphicsDevice);
            }
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
                    Matrix.CreateTranslation(0, 0, 0, out Matrix translation);
                    basicEffect.World = Matrix.Multiply(translation, basicEffect.World);

                }
                mesh.Draw();
            }
        }


        private void DrawModelTranslationAndColor(Model model, Matrix world, Matrix view, Matrix projection,
            float tX, float tY, float tZ, Vector3 color, float scaleX = 1f, float scaleY = 1f, float scaleZ = 1f, float rot = 0f)
        {
            // int count = 0;
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (Effect effect in mesh.Effects)
                {
                    BasicEffect basicEffect = (BasicEffect) effect;
                    basicEffect.EnableDefaultLighting();
                    CommonBasicEffects.SetEffects(basicEffect);

                    //basicEffect.AmbientLightColor = new Vector3(0.7f, 0.5f, 0.4f);
                    //basicEffect.DirectionalLight0.Enabled = true;
                    //basicEffect.DirectionalLight0.Direction = new Vector3(1, 1, -1);


                    Matrix S = Matrix.CreateScale(scaleX, scaleY, scaleZ);
                    Matrix R = Matrix.CreateRotationZ(MathHelper.ToRadians(rot));
                    Matrix T = Matrix.CreateTranslation(tX, tY, tZ);
                    Matrix transform = Matrix.Multiply(R, T);
                    transform = Matrix.Multiply(S, transform);

                    basicEffect.World = world;
                    basicEffect.View = view;
                    basicEffect.Projection = projection;



                    basicEffect.World = Matrix.Multiply(transform, basicEffect.World);
                    // basicEffect.Projection = Matrix.Multiply(T, basicEffect.World);
                    // basicEffect.DiffuseColor = color;

                    // basicEffect.World = Matrix.Multiply(T, basicEffect.World);

                }
                mesh.Draw();
            }
        }



        float rotateModel = 0f;

        private void DrawModel2(Model model, Matrix world, Matrix view, Matrix projection)
        {
            rotateModel += 0.01f;

            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (Effect effect in mesh.Effects)
                {
                    BasicEffect basicEffect = (BasicEffect) effect;
                    // CommonBasicEffects.SetEffects(basicEffect);
                    basicEffect.EnableDefaultLighting();
                    basicEffect.AmbientLightColor = new Vector3(0.8f, 0.8f, 0.8f);
                    basicEffect.DirectionalLight0.Enabled = true;
                    basicEffect.DirectionalLight0.Direction = new Vector3(0.4f, 0.3f, 0.3f);
                    basicEffect.DirectionalLight0.DiffuseColor = new Vector3(0.8f, 0.8f, 0.8f);


                    // Matrix rotation = Matrix.CreateRotationZ(rotateModel);
                    // Matrix rotation = Matrix.CreateRotationX(rotateModel);

                    // basicEffect.TextureEnabled = true;
                    basicEffect.World = world;
                    basicEffect.View = view;
                    basicEffect.Projection = projection;
                    // basicEffect.DiffuseColor = Color.Red.ToVector3();
                    // basicEffect.World = Matrix.Multiply(rotation, basicEffect.World);
                }
                GraphicsDevice.RasterizerState = RasterizerState.CullNone;
                // GraphicsDevice.RasterizerState = RasterizerState.CullClockwise;
                mesh.Draw();
                // GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
            }
        }
    }
}


