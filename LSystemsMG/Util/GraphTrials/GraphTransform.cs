using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSystemsMG.Util.GraphTrials
{
    class MatrixTransform
    {
        public string transform;

        public MatrixTransform(string transform)
        {
            this.transform = transform;
        }
        public void SetTransform(string transform)
        {
            this.transform = transform;
        }

        public static MatrixTransform Identity()
        {
            return new MatrixTransform("[I]");
        }


    }
}
