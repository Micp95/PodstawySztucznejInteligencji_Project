using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{

    struct TestData
    {
        public double[] res;
        public double[] x;
        public TestData(double[] x, double[] res)
        {
            this.res = res;
            this.x = x;
        }
    }
}
