using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpatialDelimiter : MonoBehaviour
{
    private float limit = 300f;
    private Vector3 initPos;
    private Contact contactScript;
    private bool enableCheck = true;

    void Start()
    {
        initPos = this.transform.position;
        contactScript = GameObject.Find("Soyuz").GetComponent<Contact>();
    }

    void Update()
    {
        if (enableCheck) {
            if (this.transform.position.x > limit)
            {
                Debug.Log("Soyuz escaped Earth's gravitational pull and is now stranded in space, towards oblivion.");
                contactScript.EndGame(3);
                enableCheck = false;
            }
            else if (this.transform.position.x < -limit)
            {
                Debug.Log("Soyuz lost too much altitude and fell on Earth.");
                contactScript.EndGame(4);
                enableCheck = false;
            }
            else if (this.transform.position.y > limit)
            {
                Debug.Log("Soyuz missed the Rendevouz window and has been forced to perform reentry.");
                contactScript.EndGame(5);
                enableCheck = false;
            }
            else if (this.transform.position.y < -limit)
            {
                Debug.Log("Soyuz missed the Rendevouz window and fell towards the austral emisphere.");
                contactScript.EndGame(6);
                enableCheck = false;
            }
            else if (this.transform.position.z > limit)
            {
                Debug.Log("Soyuz approached the ISS too fast, missing it and forcing a reentry.");
                contactScript.EndGame(7);
                enableCheck = false;
            }
            else if (this.transform.position.z < -limit)
            {
                Debug.Log("Soyuz lost too much approach velocity and missed the ISS completely.");
                contactScript.EndGame(8);
                enableCheck = false;
            }
        }
    }
}
