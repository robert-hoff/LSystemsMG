
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using RootNomics.Environment;
using RootNomicsGame.Simulation;

/*
 * previously
 *
 *  groundTiles.DrawGroundTiles(cameraTransforms);
 *  fernModels.DrawFerns(cameraTransforms);
 *
 *
 * fernModels = new PlantModels(modelFern1, modelPlant1, groundTiles.tileHeights);
 *
 */
namespace RootNomics.SimulationRender
{
    class SimulationRenderer
    {
        private CameraTransforms cameraTransforms;
        private GroundTiles groundTiles;
        private List<AgentRenderingModel> models = new List<AgentRenderingModel>();

        // populate on Update(Agent)
        private Dictionary<string, AgentRenderingModel> agentRenderingModels = new();

        public SimulationRenderer() { }

        public void RegisterCameraTransforms(CameraTransforms cameraTransforms)
        {
            this.cameraTransforms = cameraTransforms;
        }

        public void SetGroundTiles(GroundTiles groundTiles)
        {
            this.groundTiles = groundTiles;
        }

        private Dictionary<string, GameModel> modelsLookup = new();

        public void RegisterGameModel(string modelName, Model model)
        {
            // AgentRenderingModel agentRenderingModel = new AgentRenderingModel(model);

            GameModel gameModelRenderer = new GameModel(modelName, model);
            modelsLookup.Add(modelName, gameModelRenderer);
        }


        public void DrawGroundTiles()
        {
            groundTiles.DrawGroundTiles(cameraTransforms);
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
                    agentGameModels.Add(modelsLookup["acacieTree1"]);
                    agentGameModels.Add(modelsLookup["acacieTree2"]);
                    return agentGameModels;

                case "blacksmith":
                    agentGameModels.Add(modelsLookup["plant1"]);
                    agentGameModels.Add(modelsLookup["plant1"]);
                    agentGameModels.Add(modelsLookup["acacieTree1"]);
                    agentGameModels.Add(modelsLookup["acacieTree2"]);
                    return agentGameModels;

                case "miner":
                    agentGameModels.Add(modelsLookup["cactus1"]);
                    agentGameModels.Add(modelsLookup["cactus2"]);
                    agentGameModels.Add(modelsLookup["acacieTree1"]);
                    agentGameModels.Add(modelsLookup["acacieTree2"]);
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

        // ! Duplicated Agent class in SimulationRenderer.Agent
        public void Update(List<Agent> agents)
        {

        }
    }
}



