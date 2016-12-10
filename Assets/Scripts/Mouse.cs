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
        distances = new float[mySonars.Length];
        foreach(Sonar sonar in mySonars)
        {
            sonar.Init(distance);
        }

    }
	
	// Update is called once per frame
	void Update () {
        GetSensorValues();
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

    private void Move()
    {
        float move = speed * Time.deltaTime;
        transform.Translate(Vector3.forward * move , Space.Self);
    }

}
