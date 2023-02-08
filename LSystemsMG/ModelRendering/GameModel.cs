using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/*
 * This is well organised but it's a mistake to calculate the Matrix transform,
 * in case additional scaling or rotating is desirable later
 *
 *
 *
 *
 *
 */
/*
// -- previously

    modelUnitSquare = Content.Load<Model>("unitsquare");
    modelCubeWedge0 = Content.Load<Model>("cube-wedge0");
    modelCubeWedge1 = Content.Load<Model>("cube-wedge1");
    modelAcaciaTree1 = Content.Load<Model>("acaciaTree1");
    modelAcaciaTree2 = Content.Load<Model>("acaciaTree2");
    modelBirchTree1 = Content.Load<Model>("birchTree1");
    modelBirchTree2 = Content.Load<Model>("birchTree2");
    modelCactus1 = Content.Load<Model>("cactus1");
    modelCactus2 = Content.Load<Model>("cactus2");
    // modelFern1 = Content.Load<Model>("fern1");
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
    gameModelRegister.RegisterGameModel("acaciaTree2", modelAcaciaTree2);
    gameModelRegister.RegisterGameModel("birchTree1", modelBirchTree1);
    gameModelRegister.RegisterGameModel("birchTree2", modelBirchTree2);
    gameModelRegister.RegisterGameModel("cactus1", modelCactus1);
    gameModelRegister.RegisterGameModel("cactus2", modelCactus2);
    // gameModelRegister.RegisterGameModel("fern1", modelFern1);
    gameModelRegister.RegisterGameModel("fern2", modelFern2);
    gameModelRegister.RegisterGameModel("flower1", modelFlower1);
    gameModelRegister.RegisterGameModel("flower2", modelFlower2);
    gameModelRegister.RegisterGameModel("flower3", modelFlower3);
    gameModelRegister.RegisterGameModel("flower4", modelFlower4);
    gameModelRegister.RegisterGameModel("mushroom1", modelMushroom1);
    gameModelRegister.RegisterGameModel("mushroom2", modelMushroom2);
    gameModelRegister.RegisterGameModel("mushroom3", modelMushroom3);
    gameModelRegister.RegisterGameModel("mushroom4", modelMushroom4);
    gameModelRegister.RegisterGameModel("mushroom5", modelMushroom5);
    gameModelRegister.RegisterGameModel("mushroom6", modelMushroom6);
    gameModelRegister.RegisterGameModel("pineTree1", modelPineTree1);
    gameModelRegister.RegisterGameModel("pineTree2", modelPineTree2);
    gameModelRegister.RegisterGameModel("smallPlant1", modelSmallPlant1);
    gameModelRegister.RegisterGameModel("plant1", modelPlant1);
    gameModelRegister.RegisterGameModel("reeds1", modelReeds1);
    gameModelRegister.RegisterGameModel("terrain1", modelTerrain1);

*/
namespace LSystemsMG.ModelRendering
{
    class GameModel
    {
        private string modelName;
        private Model model;

        private Vector3 modelScaling = new Vector3(1, 1, 1);
        private Vector3 modelTranslation = new Vector3(0, 0, 0);
        private float modelXRotationDegrees = 0;
        // private float modelYRotation = 0; // not needed for any model
        private float modelZRotationDegrees = 0;
        private Matrix defaultModelTransform;

        private Vector3 defaultAmbientLightingColor = new Vector3(0.4f, 0.3f, 0.3f);
        private Vector3 defaultLightingDirection = new Vector3(0.8f, 0.8f, -1);
        private Vector3 defaultGameModelDiffuseColorLighting = new Vector3(0.8f, 0.8f, 0.8f);


        public GameModel(string modelName, Model model)
        {
            this.modelName = modelName;
            this.model = model;


            switch (modelName)
            {
                case "polygonPlant1":
                    break;

                case "polygonPlant2":
                    break;

                case "polygonPlant5":
                    break;

                case "acaciaTree1":
                    modelScaling = new Vector3(0.7f, 0.7f, 0.7f);
                    break;

                // transforms weren't applied to this model in the Blender export
                case "acaciaTree2":
                    modelXRotationDegrees = -90;
                    modelScaling = new Vector3(0.007f, 0.007f, 0.007f);
                    break;

                case "birchTree1":
                    break;

                case "birchTree2":
                    break;

                case "cactus1":
                    break;

                case "cactus2":
                    break;

                case "fern1":
                    modelScaling = new Vector3(1.5f, 1.5f, 1.5f);
                    modelTranslation = new Vector3(0, 0, 0.1f);
                    break;

                case "fern2":
                    break;

                case "flower1":
                    break;

                case "flower2":
                    break;

                case "flower3":
                    break;

                case "flower4":
                    break;

                case "mushroom1":
                    break;

                case "mushroom2":
                    break;

                case "mushroom3":
                    break;

                case "mushroom4":
                    break;

                case "mushroom5":
                    break;

                case "mushroom6":
                    break;

                case "pineTree1":
                    modelScaling = new Vector3(0.5f, 0.5f, 0.5f);
                    break;

                case "pineTree2":
                    modelScaling = new Vector3(0.5f, 0.5f, 0.5f);
                    break;

                case "smallPlant1":
                    modelScaling = new Vector3(2f, 2f, 2f);
                    break;

                case "plant1":
                    modelXRotationDegrees = -90;
                    modelScaling = new Vector3(3.5f, 3.5f, 4.5f);
                    break;

                case "reeds1":
                    modelScaling = new Vector3(2f, 2f, 2f);
                    break;

                case "terrain1":
                    break;

                case "skybox":
                    defaultAmbientLightingColor = new Vector3(0.7f, 0.7f, 0.7f);
                    defaultLightingDirection = new Vector3(0, 1f, 0);
                    defaultGameModelDiffuseColorLighting = new Vector3(0.8f, 0.8f, 0.8f);
                    break;

                default:
                    throw new Exception($"unknown model name gives = {modelName}");
            }
            Matrix T = Matrix.CreateTranslation(modelTranslation);
            Matrix rotationXTransform = Matrix.CreateRotationX(MathHelper.ToDegrees(modelXRotationDegrees));
            Matrix rotationZTransform = Matrix.CreateRotationZ(MathHelper.ToDegrees(modelZRotationDegrees));
            Matrix S = Matrix.CreateScale(modelScaling);
            Matrix transform = Matrix.Multiply(rotationXTransform, T);
            transform = Matrix.Multiply(rotationZTransform, transform);
            transform = Matrix.Multiply(S, transform);
            defaultModelTransform = transform;

        }


        public void DrawModelWithDefaultValues(CameraTransforms cameraTransforms)
        {
            DrawModelWithDefaultValues(cameraTransforms, Matrix.Identity);
        }

        public void DrawModelWithDefaultValues(CameraTransforms cameraTransforms, Matrix transform)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (Effect effect in mesh.Effects)
                {
                    BasicEffect basicEffect = (BasicEffect) effect;
                    basicEffect.EnableDefaultLighting();
                    basicEffect.AmbientLightColor = defaultAmbientLightingColor;
                    basicEffect.DirectionalLight0.Enabled = true;
                    basicEffect.DirectionalLight0.Direction = defaultLightingDirection;
                    basicEffect.DirectionalLight0.DiffuseColor = defaultGameModelDiffuseColorLighting;

                    // basicEffect.TextureEnabled = false;

                    basicEffect.World = cameraTransforms.worldMatrix;
                    basicEffect.View = cameraTransforms.viewMatrix;
                    basicEffect.Projection = cameraTransforms.projectionMatrix;

                    transform = Matrix.Multiply(defaultModelTransform, transform);
                    basicEffect.World = Matrix.Multiply(transform, basicEffect.World);
                }
                mesh.Draw();
            }
        }
    }
}

