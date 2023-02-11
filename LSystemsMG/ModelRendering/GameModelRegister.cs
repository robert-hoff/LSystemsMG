using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LSystemsMG.ModelRendering
{
    public class GameModelRegister
    {
        private CameraTransforms cameraTransforms;
        private Dictionary<string, GameModel> modelRegister = new();

        public GameModelRegister(CameraTransforms cameraTransforms)
        {
            this.cameraTransforms = cameraTransforms;
        }

        public GameModel LoadModelFromFile(string modelnamepath, Model model)
        {
            string modelName = modelnamepath.Substring(modelnamepath.IndexOf('/') + 1);
            GameModel gameModel = new GameModel(cameraTransforms, modelName, model);
            modelRegister.Add(modelName, gameModel);

            switch (modelName)
            {

                case "unitcube":
                {
                    gameModel.SetAlpha(0.5f);
                    break;
                }

                case "acaciatree1":
                {
                    Matrix defaultTransform = BuildTransform.Ident().S(1.1f, 1.1f, 1.1f).Rz(-30).Get();
                    gameModel.SetDefaultTransform(defaultTransform);
                    break;
                }

                case "fern1":
                {
                    Matrix defaultTransform = BuildTransform.Ident().S(1.5f, 1.5f, 1.5f).Tz(0.1f).Get();
                    gameModel.SetDefaultTransform(defaultTransform);
                    break;
                }

                case "pineTree1":
                {
                    Matrix defaultTransform = BuildTransform.Ident().S(0.5f, 0.5f, 0.5f).Get();
                    gameModel.SetDefaultTransform(defaultTransform);
                    break;
                }

                case "pineTree2":
                {
                    Matrix defaultTransform = BuildTransform.Ident().S(0.5f, 0.5f, 0.5f).Get();
                    gameModel.SetDefaultTransform(defaultTransform);
                    break;
                }

                case "plant1":
                {
                    Matrix defaultTransform = BuildTransform.Ident().Rx(-90).S(3.5f, 3.5f, 4.5f).Get();
                    gameModel.SetDefaultTransform(defaultTransform);
                    break;
                }

                case "reeds1":
                {
                    Matrix defaultTransform = BuildTransform.Ident().S(2, 2, 2).Get();
                    gameModel.SetDefaultTransform(defaultTransform);
                    break;
                }

                case "skybox":
                {
                    Matrix defaultTransform = BuildTransform.Ident().S(1000, 1000, 1000).Get();
                    gameModel.SetDefaultTransform(defaultTransform);
                    gameModel.SetAmbientColor(new Vector3(0.7f, 0.7f, 0.7f));
                    gameModel.SetLight0Direction(new Vector3(0, 1f, 0));
                    gameModel.SetLight0Diffuse(new Vector3(0.8f, 0.8f, 0.8f));
                    break;
                }
            }
            return gameModel;
        }

        public bool HasGameModel(string modelName)
        {
            return modelRegister.ContainsKey(modelName);
        }

        public GameModel GetGameModel(string modelName)
        {
            return modelRegister[modelName];
        }
    }
}

