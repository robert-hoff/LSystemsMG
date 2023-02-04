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
        private Matrix view = Matrix.CreateLookAt(new Vector3(0, 0, 5), new Vector3(0, 0, 0), Vector3.UnitY);
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
            basicEffect.AmbientLightColor = Vector3.One;
            basicEffect.DirectionalLight0.Enabled = true;
            basicEffect.DirectionalLight0.DiffuseColor = Vector3.One;
            basicEffect.DirectionalLight0.Direction = Vector3.Normalize(Vector3.One);
            basicEffect.World = world;
            basicEffect.View = view;
            basicEffect.Projection = projection;
            // use my own vertex colors
            basicEffect.VertexColorEnabled = true;
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
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            // GraphicsDevice.Clear(Color.CornflowerBlue);
            GraphicsDevice.Clear(Color.Black);
            // DrawModel(model, world, view, projection);



            basicEffect.CurrentTechnique.Passes[0].Apply();


            float left = 0f;
            float bottom = 0f;
            float right = 10f;
            float top = 10f;
            VertexPositionColor[] primitiveList = new VertexPositionColor[4];
            primitiveList[0] = new VertexPositionColor(new Vector3(left, 0, bottom), Color.Green);
            primitiveList[1] = new VertexPositionColor(new Vector3(left, 0, top), Color.Green);
            primitiveList[2] = new VertexPositionColor(new Vector3(right, 0, top), Color.Green);

            short[] lineListIndices = new short[3];
            lineListIndices[0] = 0;
            lineListIndices[1] = 1;
            lineListIndices[2] = 2;

            // draw a vertex-buffer
            VertexBuffer vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor), 4, BufferUsage.None);
            vertexBuffer.SetData<VertexPositionColor>(primitiveList);
            IndexBuffer lineListIndexBuffer = new IndexBuffer(
                GraphicsDevice,
                IndexElementSize.SixteenBits,
                sizeof(short) * lineListIndices.Length,
                BufferUsage.None);
            lineListIndexBuffer.SetData<short>(lineListIndices);
            GraphicsDevice.Indices = lineListIndexBuffer;
            GraphicsDevice.SetVertexBuffer(vertexBuffer);
            // GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.LineList, 0, 0, 8, 0, 7);
            // GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.LineList, 0, 0, 1, 0, 1);
            GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.LineList, 0, 4, 3);





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
