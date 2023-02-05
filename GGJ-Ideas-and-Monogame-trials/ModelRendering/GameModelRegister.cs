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

        public List<GameModel> GetAgentGameModel(string agentType)
        {
            List<GameModel> agentGameModels = new();
            switch (agentType)
            {
                case "farmer":
                    agentGameModels.Add(modelsLookup["reeds1"]);
                    agentGameModels.Add(modelsLookup["reeds1"]);
                    agentGameModels.Add(modelsLookup["acaciaTree1"]);
                    agentGameModels.Add(modelsLookup["acaciaTree2"]);
                    return agentGameModels;

                case "blacksmith":
                    agentGameModels.Add(modelsLookup["plant1"]);
                    agentGameModels.Add(modelsLookup["plant1"]);
                    agentGameModels.Add(modelsLookup["acaciaTree1"]);
                    agentGameModels.Add(modelsLookup["acaciaTree2"]);
                    return agentGameModels;

                case "miner":
                    agentGameModels.Add(modelsLookup["cactus1"]);
                    agentGameModels.Add(modelsLookup["cactus2"]);
                    agentGameModels.Add(modelsLookup["acaciaTree1"]);
                    agentGameModels.Add(modelsLookup["acaciaTree2"]);
                    return agentGameModels;

                case "refiner":
                    agentGameModels.Add(modelsLookup["smallPlant1"]);
                    agentGameModels.Add(modelsLookup["smallPlant1"]);
                    agentGameModels.Add(modelsLookup["pineTree1"]);
                    agentGameModels.Add(modelsLookup["pineTree2"]);
                    return agentGameModels;

                case "woodcutter":
                    agentGameModels.Add(modelsLookup["birchTree1"]);
                    agentGameModels.Add(modelsLookup["birchTree2"]);
                    agentGameModels.Add(modelsLookup["pineTree1"]);
                    agentGameModels.Add(modelsLookup["pineTree2"]);
                    return agentGameModels;

                case "worker":
                    agentGameModels.Add(modelsLookup["fern1"]);
                    agentGameModels.Add(modelsLookup["fern2"]);
                    agentGameModels.Add(modelsLookup["pineTree1"]);
                    agentGameModels.Add(modelsLookup["pineTree2"]);
                    return agentGameModels;

                default:
                    throw new Exception($"unknown agent type {agentType}");
            }
        }
    }
}

