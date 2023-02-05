using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

/*
 *
 */
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

        public void RegisterGameModel(string modelName, Model model)
        {
            GameModel gameModelRenderer = new GameModel(modelName, model);
            modelsLookup.Add(modelName, gameModelRenderer);
        }

        public GameModel GetGameModel(string modelName)
        {
            return modelsLookup[modelName];
        }
    }
}

