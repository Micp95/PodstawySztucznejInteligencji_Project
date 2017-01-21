using System.Collections.Generic;


namespace ArtificialIntelligence.NeuralNetwork
{

    //klasa do zarzadzania siecia
    class AdalineMPLNetworkController
    {
        //dwu-wymiarowa siatka neuronow
        private List<List<AdalineMPL>> neurons = new List<List<AdalineMPL>>();

        //konstruktor tworzy siec na podstawie parametrow
        public AdalineMPLNetworkController(int[] layers,double learnValue)
        {
            List<AdalineMPL> prevLayer = null;
            neurons.Clear();

            foreach (int k  in layers)
            {
                List<AdalineMPL> actLayer = new List<AdalineMPL>();
                for (int p = 0; p < k; p++)
                    actLayer.Add(new AdalineMPL(learnValue,prevLayer));
                neurons.Add(actLayer);
                prevLayer = actLayer;
            }
        }


        //nauka sieci - podanie wektora wejsciowego i wektora z oczekiwanym wyjsciem
        public void learn(double[] input, double[] res)
        {
            setInput(input);    //ustawienie wejscia
            ask(input);         //wyliczenie odpowiedzi sieci

            //wyliczenie bledu dla ostatniej warstwy (i ustawienie wag)
            int p = 0;
            foreach (AdalineMPL ad in neurons[neurons.Count-1])
            {
                ad.LearnInitialization(res[p++]);
            }

            //oblczenie bledu i ustawienie wag polaczen dla reszty sieci
            for (int k = neurons.Count - 2; k >0; k--)
            {
                foreach (AdalineMPL ad in neurons[k])
                {
                    ad.Learn();
                }
            }
        }

        //odpytanie sieci
        public double[] ask(double[] input)
        {
            setInput(input);    //ustawienie wejscia

            //odpytanie kolejnych neuronow
            for ( int k = 1; k < neurons.Count; k++)
            {
                foreach (AdalineMPL ad in neurons[k])
                {
                    ad.Ask();
                }
            }

            //zebranie wynikow do jednej tablicy
            double[] res = new double[neurons[neurons.Count - 1].Count];
            int p = 0;
            foreach (AdalineMPL ad in neurons[neurons.Count -1])
            {
                res[p++] = ad.output;
            }
            return res;
        }

        //ustawienie wejscia do sieci (pierwszej warstwy neuronow)
        private void setInput(double[] input)
        {
            int k = 0;
            foreach (AdalineMPL ad in neurons[0])
                ad.output = input[k++];
        }

    }
}
