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

    //funkcje wykrywajaca kolizje dla myszy - rozroznia checkpoint i sciane
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "block")
        {
            mouse.controller.restartMouse();
        }else if (collision.gameObject.tag == "Finish")
        {
            mouse.controller.nextLap();
        }

    }
}
