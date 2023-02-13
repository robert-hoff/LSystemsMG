using Microsoft.Xna.Framework;
using LSystemsMG.ModelFactory;

namespace LSystemsMG.ModelRendering
{
    class SceneGraph
    {
        private GameModelRegister gameModelRegister;
        public SceneGraphNode rootNode = SceneGraphNode.CreateRootNode();

        public SceneGraph(GameModelRegister gameModelRegister)
        {
            this.gameModelRegister = gameModelRegister;
            this.worldAxes = gameModelRegister.CreateModel("axismodel");
            LoadExampleScene();
        }

        private bool showWorldAxes = false;
        private GameModel worldAxes;
        public void ShowWorldAxes(bool showWorldAxes, float axesLen = 1f)
        {
            this.showWorldAxes = showWorldAxes;
            worldAxes.SetBaseTransform(Transforms.Scale(axesLen));
        }

        // key models
        public SceneGraphNode cubeBaseCoordFrame;
        public SceneGraphNode plantsCoordFrame;
        public GameModel modelFern0;

        private void LoadExampleScene()
        {
            // World axis
            //GameModel axisModel0 = gameModelRegister.CreateModel("axismodel");
            //axisModel0.SetBaseTransform(Transforms.Ident().S(5, 5, 5).Get());
            //models.Add(axisModel0);

            // the coordinate system starts coincident with world coordinates
            cubeBaseCoordFrame = rootNode.AddNode(Matrix.Identity, "base");

            GameModel cubeBaseModel = gameModelRegister.CreateModel("unitcube");
            cubeBaseCoordFrame.AddModel(cubeBaseModel);

            // scale the platform
            cubeBaseModel.SetBaseTransform(Transforms.Ident().S(5, 5, 0.25f).Get());
            // the plants coordinates frame is related to this scaling,
            // setting it manually here
            // plantsCoordFrame = new SceneGraphNode(Transforms.Ident().T(-2.5f, -2.5f, 0.25f).Get(), cubeBaseCoordFrame.transform);

            plantsCoordFrame = cubeBaseCoordFrame.AddNode(Transforms.Translate(-2.5f, -2.5f, 0.25f), "plants");
            // cubeBaseCoordFrame.AddNode(plantsCoordFrame);

            GameModel plantsAxis = gameModelRegister.CreateModel("axismodel");
            plantsAxis.SetTransform(Transforms.Ident().S(0.5f).Get());
            plantsCoordFrame.AddModel(plantsAxis);

            modelFern0 = gameModelRegister.CreateModel("polygon-plant1");
            // modelFern0.SetBaseTransform(Transforms.Ident().Get());
            modelFern0.SetBaseTransform(Transforms.Ident().S(0.8f, 0.8f, 0.8f).Ry(30).Get());
            // modelFern0.AppendBaseTransform(Transforms.Ident().T(4.7f, 0.3f, 0).Get());
            modelFern0.SetTransform(Transforms.Ident().T(4.7f, 0.3f, 0).Get());
            plantsCoordFrame.AddModel(modelFern0);

            GameModel modelFern1 = gameModelRegister.CreateModel("polygon-plant1");
            modelFern1.SetBaseTransform(Transforms.Ident().S(0.8f, 0.8f, 0.8f).T(1, 1, 0).Get());
            plantsCoordFrame.AddModel(modelFern1);

            // call after populating scene, evaluating all transformations
            UpdateTransformations();
        }

        public void UpdateTransformations()
        {
            rootNode.UpdateTransforms(Matrix.Identity);
        }

        public void Draw()
        {
            if (showWorldAxes)
            {
                worldAxes.Draw();
            }
            plantsCoordFrame.DrawModels();
            cubeBaseCoordFrame.DrawModels();
        }
    }
}

