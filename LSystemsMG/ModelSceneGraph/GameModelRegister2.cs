using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LSystemsMG.ModelFactory;
using LSystemsMG.ModelFactory.Models;

namespace LSystemsMG.ModelSceneGraph
{
    class GameModelRegister2
    {
        private CameraTransforms cameraTransforms;
        private Dictionary<string, InstantiateGameModel> modelRegister = new();
        private delegate GameModel2 InstantiateGameModel(string modelName);

        public GameModelRegister2(CameraTransforms cameraTransforms)
        {
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

            switch (modelName)
            {
                case "unitcube":
                    modelAlpha = 0.5f;
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
                return gameModel;
            });
        }

        public void RegisterModelPrimitive(GraphicsDevice graphicsDevice, string modelName)
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
