using System.Diagnostics;

namespace LSystemsMG.Util.GraphDemo
{
    class GraphModel
    {
        /*
         * the number of transforms on the model may be more or less, a practical scheme
         * for the models can be deduced separately
         */
        public string modelBaseTransform = "[I]";
        public string coordinateTransform = "[I]";
        public string parentTransform = "[I]";
        public string worldTransform = "[I]";

        public string modelName;

        public GraphModel(string modelName)
        {
            this.modelName = modelName;
        }
        public GraphModel SetTransform(string coordinateTransform)
        {
            this.coordinateTransform = coordinateTransform;
            return this;
        }

        public override string ToString()
        {
            return modelName;
        }
    }
}

