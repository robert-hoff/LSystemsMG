using System.Diagnostics;

namespace LSystemsMG.Util.GraphTrials
{

    class GraphModel
    {
        /*
         * the number of transforms on the model may be more or less, a practical scheme
         * for the models can be deduced separately
         */
        private MatrixTransform parentTransform = MatrixTransform.Identity();
        private MatrixTransform coordinateTransform = MatrixTransform.Identity();
        private MatrixTransform modelBaseTransform = MatrixTransform.Identity();

        private string modelName;

        public GraphModel(string modelName)
        {
            this.modelName= modelName;
        }

        public override string ToString()
        {
            return modelName;
        }
    }
}

