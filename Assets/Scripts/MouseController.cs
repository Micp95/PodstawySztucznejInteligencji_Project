using UnityEngine;
using System.Collections;
using ArtificialIntelligence.NeuralNetwork;
using Assets.Scripts;
using System.Collections.Generic;

public class MouseController : MonoBehaviour {
    public Transform mousePosition;

    private Vector3 startPosuition;
    private Quaternion startRotation;
    public Mouse mouse;
    public float lernValue = 0.5f;
    public int learnIteration = 100;
    public float error = 0.5f;
    public TextMesh label;
    public TextMesh labelLern;

    private int lapIteration;
    private int lernIteration;

    AdalineMPLNetworkController network;

    // Use this for initialization
    void Start () {
        mouse.controller = this;

        int[] layer = new int[] {5,10,3};

        network = new AdalineMPLNetworkController(layer, lernValue);

        startPosuition = mousePosition.position;
        startRotation = mousePosition.rotation;




        restartMouse();
    }

    public void restartMouse()
    {
        mousePosition.position = startPosuition;
        mousePosition.rotation = startRotation;


        lernPro(learnIteration,error);
        lapIteration = 0;

        lernIteration += learnIteration;
        labelLern.text = "Lern iterations: " + lernIteration;
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
        double[] res = network.ask(mapTab);

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

        if (win == 1)
            mouse.turnLeft();
        else if (win == 2)
            mouse.turnRight();
   //     else if (win == 3)
   //         restartMouse();
    }


    private void lernPro(int iterations, double error)
    {
        List<TestData> lernData = getTestData();

        while (iterations != 0)
        {
            foreach (TestData actData in lernData)
            {
                network.lern(actData.x, actData.res);
            }
            iterations--;
        }
    }
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


        //input = new double[] { 1, 0, 0, 1, 1 };
        //output = new double[] {1, 0, 0 };
        //res.Add(new TestData(input, output));

        //input = new double[] { 1, 1, 0, 0, 1 };
        //output = new double[] { 1, 0, 0 };
        //res.Add(new TestData(input, output));


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


        //no movement

        //input = new double[] { 1, 1, 1, 1, 1 };
        //output = new double[] { 0, 0, 0 };
        //res.Add(new TestData(input, output));


        return res;
    }


}
