using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace LSystemsMG.ModelSceneGraph
{
    class GameModelRegister2
    {
        private CameraTransforms cameraTransforms;
        private Dictionary<string, (Model, GameModelDefaults)> modelRegister = new();

        public GameModelRegister2(CameraTransforms cameraTransforms) {
            this.cameraTransforms = cameraTransforms;
        }

        public void RegisterModel(string modelName, Model model)
        {
            GameModelDefaults modelDefaults = new();

            switch (modelName)
            {
                case "unitcube":
                {
                    modelDefaults.modelAlpha = 0.5f;
                    break;
                }
            }
            modelRegister.Add(modelName, (model, modelDefaults));
        }

        public GameModel2 CreateModel(string modelName)
        {
            (Model, GameModelDefaults) modelDetail = modelRegister[modelName];
            Model model = modelDetail.Item1;
            GameModelDefaults modelDefaults = modelDetail.Item2;
            GameModel2 gameModel = new GameModel2(cameraTransforms, modelName, model, modelDefaults);
            return gameModel;
        }
    }
}
