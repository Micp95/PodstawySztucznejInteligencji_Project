using System;
using System.Collections.Generic;

namespace ArtificialIntelligence.NeuralNetwork
{
    class AdalineMPL
    {
        //wejscie sieci
        List<AdalineMPL> inputs = new List<AdalineMPL>();
        //wagi polaczen 
        List<double> weights = new List<double>();
        private double b;


        public double output { get; set; }
        public double deltaError { get; set; }

        private double learnValue { get; set; }

        private double alpha = 0.6;

        public AdalineMPL(double learnValue, List<AdalineMPL> inputs)
        {
            this.inputs = inputs;
            this.learnValue = learnValue;
            if( inputs != null)
                InitializeWeights();
        }

        //odpytanie neuronu - ustawienie wyjscia
        public void Ask()
        {
            deltaError = 0;
            double s = b;
            for (int k = 0; k < inputs.Count; k++)
                s += weights[k] * inputs[k].output;
            output = function(s);
        }

        //obliczenie bledy i przekazanie bledu do poprzednich warstw
        public void Learn()
        {
            //send error to prev layer
            for (int k = 0; k < inputs.Count; k++)
            {
                inputs[k].deltaError += deltaError * weights[k];
            }
            errorFunction();
        }

        //obliczenie bledu dla ostatniej warstwy
        public void LearnInitialization(double z)
        {
            deltaError = z - output;
            Learn();
        }

        //funkcja uczenia - ustawienie wag
        void errorFunction()
        {
            for (int k = 0; k < inputs.Count; k++)
                weights[k] = weights[k] + (learnValue* deltaError * dFunction (output)* inputs[k].output);
            b = b + learnValue * dFunction(output) * deltaError;
        }


        //funkcja aktywacji
        double function(double x)
        {
            return 1.0 / (1.0+Math.Exp((-alpha)*x));
        }

        //pochodna funkcji aktywacji
        double dFunction(double y)
        {
            return y * (1.0 - y);
        }

        void InitializeWeights()
        {
            Random rand = new Random();
            weights.Clear();

            for( int k = 0;k < inputs.Count;k++)
                weights.Add(rand.NextDouble());

            b = rand.NextDouble();
        }
    }
}
