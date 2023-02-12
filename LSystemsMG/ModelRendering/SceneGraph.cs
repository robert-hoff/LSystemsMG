using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using LSystemsMG.ModelFactory;
using static LSystemsMG.ModelRendering.SceneGraph;

namespace LSystemsMG.ModelRendering
{
    class SceneGraph : SceneGraphMember
    {
        private GameModelRegister gameModelRegister;
        public List<SceneGraphNode> nodes = new();
        public List<GameModel> models = new();

        // key models
        public SceneGraphNode cubeBaseCoordFrame;
        public SceneGraphNode plantsCoordFrame;


        public SceneGraph(GameModelRegister gameModelRegister)
        {
            this.gameModelRegister = gameModelRegister;
            LoadExampleScene();
        }

        private void LoadExampleScene()
        {
            // World axis
            GameModel axisModel0 = gameModelRegister.CreateModel("axismodel");
            axisModel0.SetBaseTransform(Transforms.Ident().S(5, 5, 5).Get());
            models.Add(axisModel0);

            // the coordinate system starts coincident with world coordinates
            cubeBaseCoordFrame = new SceneGraphNode(Matrix.Identity, this);
            nodes.Add(cubeBaseCoordFrame);
            GameModel cubeBaseModel = gameModelRegister.CreateModel("unitcube");
            cubeBaseCoordFrame.AddModel(cubeBaseModel);

            // scale the platform
            cubeBaseModel.SetBaseTransform(Transforms.Ident().S(5, 5, 0.25f).Get());
            // the plants coordinates frame is related to this scaling,
            // setting it manually here
            plantsCoordFrame = new SceneGraphNode(Transforms.Ident().T(-2.5f, -2.5f, 0.25f).Get(), cubeBaseCoordFrame);
            cubeBaseCoordFrame.AddNode(plantsCoordFrame);

            GameModel plantsAxis = gameModelRegister.CreateModel("axismodel");
            plantsAxis.SetTransform(Transforms.Ident().S(0.5f).Get());
            plantsCoordFrame.AddModel(plantsAxis);

            GameModel modelFern0 = gameModelRegister.CreateModel("polygon-plant1");
            modelFern0.SetBaseTransform(Transforms.Ident().S(0.8f, 0.8f, 0.8f).Ry(30).Get());
            modelFern0.SetTransform(Transforms.Ident().T(4.7f, 0.3f, 0).Get());
            plantsCoordFrame.AddModel(modelFern0);

            GameModel modelFern1 = gameModelRegister.CreateModel("polygon-plant1");
            modelFern1.SetBaseTransform(Transforms.Ident().S(0.8f, 0.8f, 0.8f).T(1, 1, 0).Get());
            plantsCoordFrame.AddModel(modelFern1);
        }

        /*
         * The transform of the root node is always the identity matrix
         */
        public Matrix CoordinateTransform()
        {
            return Matrix.Identity;
        }

        public void Draw()
        {
            DrawModels();
            plantsCoordFrame.DrawModels();
            cubeBaseCoordFrame.DrawModels();
        }

        public void DrawModels()
        {
            foreach (GameModel gameModel in models)
            {
                gameModel.Draw();
            }
        }

        public interface SceneGraphMember
        {
            public Matrix CoordinateTransform();
        }
    }
}

