using UnityEngine;
using System.Collections;
using ArtificialIntelligence.NeuralNetwork;
using Assets.Scripts;
using System.Collections.Generic;


//kontroler do poruszania sie myszy
public class MouseController : MonoBehaviour {
    public Transform mousePosition;

    private Vector3 startPosuition;
    private Quaternion startRotation;
    public Mouse mouse;
    public float learnValue = 0.5f;
    public int learnIteration = 100;
    public float error = 0.5f;
    public TextMesh label;
    public TextMesh labelLearn;

    private int lapIteration;
    private int learnIterationSum;

    AdalineMPLNetworkController network;

    // Use this for initialization
    void Start () {
        mouse.controller = this;

        //stworzenie sieci neuronowej - 5 neuronow wejsciowych, 10 w warstwie ukrytej, 3 w warstwie wyjsciowej
        int[] layer = new int[] {5,10,3};
        network = new AdalineMPLNetworkController(layer, learnValue);

        startPosuition = mousePosition.position;
        startRotation = mousePosition.rotation;

        restartMouse();
    }

    public void restartMouse()
    {
        mousePosition.position = startPosuition;
        mousePosition.rotation = startRotation;

        //uczenie sieci - ilosc iteracji ustawiona w edytorze
        learnPro(learnIteration,error);
        lapIteration = 0;

        learnIterationSum += learnIteration;
        labelLearn.text = "Learn iterations: " + learnIterationSum;
        nextLap();
    }

    public void nextLap()
    {
        label.text = "Lap: " + lapIteration;
        lapIteration++;
    }
	
	// Update is called once per frame
	void Update () {
        controllMouse();
    }



    void controllMouse()
    {
        double[] mapTab = new double[5];
        int iter = 0;
        foreach (float v in mouse.distances)
            mapTab[iter++] = v;

        //odpytanie sieci
        double[] res = network.ask(mapTab);

        //sprawdzenie ktora odpowiedz jest najsilniejsza
        double max = -1;
        int win = 0;
        for( int act = 0; act < 3; act++)
        {
            if (res[act] > max)
            {
                max = res[act];
                win = act;
            }
        }

        //wykonanie ruchu zgonie ze zwycięską odpowiedzia
        if (win == 1)
            mouse.turnLeft();
        else if (win == 2)
            mouse.turnRight();
    }


    private void learnPro(int iterations, double error)
    {
        //pobranie danych do uczenia
        List<TestData> learnData = getTestData();

        //wykonanie iterations iteracji uczenia
        while (iterations != 0)
        {
            foreach (TestData actData in learnData)
            {
                network.learn(actData.x, actData.res);
            }
            iterations--;
        }
    }
    //funkcja zwracajana liste tablic uczacych
    List<TestData> getTestData()
    {
        List<TestData> res = new List<TestData>();
        double[] input;
        double[] output;

        //straight
        input = new double[] {1,1,0,1,1 };
        output = new double[] {1,0,0};
        res.Add(new TestData(input,output));

        input = new double[] { 1, 0, 0, 0, 1 };
        output = new double[] { 1, 0, 0 };
        res.Add(new TestData(input, output));

        input = new double[] { 0, 0, 0, 0, 0 };
        output = new double[] { 1, 0, 0 };
        res.Add(new TestData(input, output));


        //Left
        input = new double[] { 1, 0, 0, 1, 1 };
        output = new double[] { 0, 1, 0 };
        res.Add(new TestData(input, output));

        input = new double[] { 0, 1, 1, 1, 1 };
        output = new double[] { 0, 1, 0 };
        res.Add(new TestData(input, output));

        input = new double[] { 0, 0, 1, 1, 1 };
        output = new double[] { 0, 1, 0};
        res.Add(new TestData(input, output));

        input = new double[] { 0, 1, 1, 0, 1 };
        output = new double[] { 0, 1, 0 };
        res.Add(new TestData(input, output));

        input = new double[] { 0, 0, 1, 0, 1 };
        output = new double[] { 0, 1, 0 };
        res.Add(new TestData(input, output));



        //Right

        input = new double[] { 1, 1, 0, 0, 1 };
        output = new double[] { 0, 0, 1 };
        res.Add(new TestData(input, output));

        input = new double[] { 1, 1, 1, 1, 0 };
        output = new double[] { 0, 0, 1 };
        res.Add(new TestData(input, output));

        input = new double[] { 1, 1, 1, 0, 0 };
        output = new double[] { 0, 0, 1};
        res.Add(new TestData(input, output));

        input = new double[] { 1, 0, 1, 1, 0 };
        output = new double[] { 0, 0, 1 };
        res.Add(new TestData(input, output));

        input = new double[] { 1, 0, 1, 0, 0 };
        output = new double[] { 0, 0, 1 };
        res.Add(new TestData(input, output));

        return res;
    }


}
