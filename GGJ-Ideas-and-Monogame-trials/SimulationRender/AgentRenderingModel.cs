using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.Xna.Framework.Graphics;
using RootNomics.Environment;

namespace RootNomics.SimulationRender
{
    class AgentRenderingModel
    {
        public string Id;
        //"worker",
        //"blacksmith",
        //"miner",
        //"refiner",
        //"woodcutter"
        public string Type;
        public float Wealth;

        List<GameModel> agentGameModels;
        GroundTilesOccupancy groundTilesOccupancy;

        int boardX;
        int boardY;


        public AgentRenderingModel(List<GameModel> agentGameModels, GroundTilesOccupancy groundTilesOccupancy)
        {
            this.agentGameModels = agentGameModels;
            this.groundTilesOccupancy= groundTilesOccupancy;

        }



    }
}




