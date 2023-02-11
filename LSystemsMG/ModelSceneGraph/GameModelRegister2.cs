using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LSystemsMG.ModelFactory;
using LSystemsMG.ModelFactory.Models;
using LSystemsMG.ModelRendering;

namespace LSystemsMG.ModelSceneGraph
{
    class GameModelRegister2
    {
        private GraphicsDevice graphicsDevice;
        private CameraTransforms cameraTransforms;
        private Dictionary<string, InstantiateGameModel> modelRegister = new();
        private delegate GameModel2 InstantiateGameModel(string modelName);

        public GameModelRegister2(GraphicsDevice graphicsDevice, CameraTransforms cameraTransforms)
        {
            this.graphicsDevice = graphicsDevice;
            this.cameraTransforms = cameraTransforms;
        }

        private Vector3 DEFAULT_AMBIENT_COLOR = new Vector3(0.4f, 0.3f, 0.3f);
        private bool DEFAULT_LIGHT0_ENABLED = true;
        private Vector3 DEFAULT_LIGHT0_DIRECTION = new Vector3(0.8f, 0.8f, -1);
        private Vector3 DEFAULT_LIGHT0_DIFFUSE = new Vector3(0.8f, 0.8f, 0.8f);
        private float DEFAULT_ALPHA = 1.0F;

        public void RegisterModelFbx(string modelName, Model model)
        {
            Vector3 modelAmbientColor = DEFAULT_AMBIENT_COLOR;
            bool modelLight0Enabled = DEFAULT_LIGHT0_ENABLED;
            Vector3 modelLight0Direction = DEFAULT_LIGHT0_DIRECTION;
            Vector3 modelLight0Diffuse = DEFAULT_LIGHT0_DIFFUSE;
            float modelAlpha = DEFAULT_ALPHA;
            Matrix baseTransform = Matrix.Identity;

            switch (modelName)
            {
                case "unitcube":
                    modelAlpha = 0.5f;
                    break;
                case "acaciatree1":
                    baseTransform = BuildTransform.Ident().S(1.1f, 1.1f, 1.1f).Rz(-30).Get();
                    break;
                case "fern1":
                    baseTransform = BuildTransform.Ident().S(1.5f, 1.5f, 1.5f).Tz(0.1f).Get();
                    break;
                case "pineTree1":
                    baseTransform = BuildTransform.Ident().S(0.5f, 0.5f, 0.5f).Get();
                    break;
                case "pineTree2":
                    baseTransform = BuildTransform.Ident().S(0.5f, 0.5f, 0.5f).Get();
                    break;
                case "plant1":
                    baseTransform = BuildTransform.Ident().Rx(-90).S(3.5f, 3.5f, 4.5f).Get();
                    break;
                case "reeds1":
                    baseTransform = BuildTransform.Ident().S(2, 2, 2).Get();
                    break;
                case "skybox":
                    baseTransform = BuildTransform.Ident().S(1000, 1000, 1000).Get();
                    modelAmbientColor = new Vector3(0.7f, 0.7f, 0.7f);
                    modelLight0Direction = new Vector3(0, 1f, 0);
                    modelLight0Diffuse = new Vector3(0.8f, 0.8f, 0.8f);
                    break;
            }
            modelRegister.Add(modelName, (string modelName) =>
            {
                ModelFbx gameModel = new ModelFbx(cameraTransforms, modelName, model);
                gameModel.SetAmbientColor(modelAmbientColor);
                gameModel.SetLight0Enabled(modelLight0Enabled);
                gameModel.SetLight0Direction(modelLight0Direction);
                gameModel.SetLight0Diffuse(modelLight0Diffuse);
                gameModel.SetAlpha(modelAlpha);
                gameModel.SetBaseTransform(baseTransform);
                return gameModel;
            });
        }

        public void RegisterModelPrimitive(string modelName)
        {
            switch (modelName)
            {
                case "axismodel":
                    modelRegister.Add(modelName, (string modelName) =>
                    {
                        ModelAxisPrimitive gameModel = new ModelAxisPrimitive(graphicsDevice, cameraTransforms, modelName);
                        return gameModel;
                    });
                    break;
                default:
                    throw new Exception($"unknown modelName in RegisterModelPrimitive {modelName}");
            }
        }

        public GameModel2 CreateModel(string modelName)
        {
            return modelRegister[modelName](modelName);
        }
    }
}
