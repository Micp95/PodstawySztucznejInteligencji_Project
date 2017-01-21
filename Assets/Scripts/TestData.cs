using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{

    //klasa pomocnicza przechowująca dane do testowania - wektor wejsciowy i wektor wyjsciowy sieci
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
