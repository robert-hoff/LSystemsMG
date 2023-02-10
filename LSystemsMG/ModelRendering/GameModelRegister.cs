using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LSystemsMG.ModelRendering
{
    class GameModelRegister
    {
        private CameraTransforms cameraTransforms;
        private Dictionary<string, GameModel> modelsLookup = new();

        public GameModelRegister(CameraTransforms cameraTransforms)
        {
            this.cameraTransforms = cameraTransforms;
        }

        public GameModel RegisterGameModel(string modelnamepath, Model model)
        {
            string modelName = modelnamepath.Substring(modelnamepath.IndexOf('/') + 1);
            GameModel gameModel = new GameModel(cameraTransforms, modelName, model);
            modelsLookup.Add(modelName, gameModel);

            switch (modelName)
            {
                case "polygonPlant1":
                    break;

                case "polygonPlant2":
                    break;

                case "polygonPlant5":
                    break;

                case "acaciatree1":
                    gameModel.Rdeg(-30).S(1.1f, 1.1f, 1.1f).Apply();
                    break;

                case "acaciatree2":
                    break;

                case "birchtree1":
                    break;

                case "birchtree2":
                    break;

                case "cactus1":
                    break;

                case "cactus2":
                    break;

                case "fern1":
                    gameModel.S(1.5f, 1.5f, 1.5f).T(0, 0, 0.1f).Apply();
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
                    gameModel.S(0.5f, 0.5f, 0.5f).Apply();
                    break;

                case "pineTree2":
                    gameModel.S(0.5f, 0.5f, 0.5f).Apply();
                    break;

                case "smallPlant1":
                    gameModel.S(2f, 2f, 2f).Apply();
                    break;

                case "plant1":
                    gameModel.Rdeg(rX: -90).S(3.5f, 3.5f, 4.5f).Apply();
                    break;

                case "reeds1":
                    gameModel.S(2f, 2f, 2f).Apply();
                    break;

                case "terrain1":
                    break;

                case "skybox":
                    gameModel.SetAmbientColor(new Vector3(0.7f, 0.7f, 0.7f));
                    gameModel.SetLight0Direction(new Vector3(0, 1f, 0));
                    gameModel.SetLight0Diffuse(new Vector3(0.8f, 0.8f, 0.8f));
                    gameModel.S(1000, 1000, 1000).Apply();
                    break;
            }
            return gameModel;
        }

        public GameModel GetGameModel(string modelName)
        {
            return modelsLookup[modelName];
        }
    }
}

