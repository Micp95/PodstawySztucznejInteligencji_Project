using UnityEngine;
using System.Collections;

public class Sonar : MonoBehaviour {

    public float myDistance = 1f;
    public float myHit = -1;

    public  void Init(float distance)
    {
        myDistance = distance;

    }

    // Use this for initialization
    void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {

        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.forward, myDistance);
        if (hits.Length != 0)
        {
            float min = float.MaxValue;
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.tag != "Finish" && hit.distance < min)
                    min = hit.distance;
            }
            myHit = min;
        }
        else
            myHit = -1;
    }
}
