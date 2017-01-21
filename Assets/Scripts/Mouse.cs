using UnityEngine;
using System.Collections;

public class Mouse : MonoBehaviour {

    public float distance;
    public Sonar[] mySonars;

    public float[] distances;

    public float speed = 0.1f;
    public float speedRotation = 80f;

    public MouseController controller;

	// Use this for initialization
	void Start () {
        //tworzenie tablicy, gdzie przechowywane sa wyniki odczytow z sensorow
        distances = new float[mySonars.Length];

        //inicjalizacja sensorow - nadanie im maksymalnego zasiegu zgodnie z ustawieniami w edytorze (public float distance)
        foreach (Sonar sonar in mySonars)
        {
            sonar.Init(distance);
        }
    }
	
	// Update is called once per frame
	void Update () {
        GetSensorValues();

        //mysz sama w sobie porusza sie tylko do przodu
        Move();
    }

    public void turnRight()
    {

        transform.Rotate(Vector3.up * Time.deltaTime* speedRotation, Space.World);
    }
    public void turnLeft()
    {

        transform.Rotate(Vector3.up * Time.deltaTime *-1* speedRotation, Space.World);
    }

    //pobranie i znormalizowanie odczytanych wynikow z sensorow
    private void GetSensorValues()
    {

        for (int k = 0; k < mySonars.Length; k++)
        {
            //normalize
            float tmp = mySonars[k].myHit;

            if (tmp == -1)
                tmp = distance;
            distances[k] = 1- tmp / distance;
        }
    }

    //poruszenie sie do przodu
    private void Move()
    {
        float move = speed * Time.deltaTime;
        transform.Translate(Vector3.forward * move , Space.Self);
    }

}
