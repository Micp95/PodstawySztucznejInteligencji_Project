using UnityEngine;
using System.Collections;

public class colider : MonoBehaviour {
    public Mouse mouse;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "block")
        {
            mouse.controller.restartMouse();
        }

    }
}
