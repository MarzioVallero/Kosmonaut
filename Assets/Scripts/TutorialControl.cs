using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialControl : MonoBehaviour
{
    public bool reset = false;
    private bool roll = false;
    private bool pitch = false;
    private bool yaw = false;
    private bool horizontal = false;
    private bool vertical = false;
    private bool accelerate = false;
    private bool firstPlay = false;
    private Transform t;
    private Rigidbody r;
    private Vector3 startPosition;
    private MoveBillboard billboard;
    public ParticleSystem billboardTrailLeft;
    public ParticleSystem billboardTrailRight;


    // Start is called before the first frame update
    void Start()
    {
        t = this.GetComponent<Transform>();
        r = this.GetComponent<Rigidbody>();
        startPosition = t.position;
        billboard = GameObject.Find("scifiBillboard").GetComponent<MoveBillboard>();
    }

    // Update is called once per frame
    void Update()
    {
        if (reset)
        {
            Vector3 dir = startPosition - t.position;
            t.Translate(dir * 2.0f * Time.deltaTime, Space.World);
            t.rotation = Quaternion.RotateTowards(t.rotation, Quaternion.Euler(Vector3.forward), 1.0f);
            if ((1 - Mathf.Abs(Quaternion.Dot(t.rotation, Quaternion.Euler(Vector3.forward)))) < 0.002f && Vector3.Distance(t.position, startPosition) < 0.0005f)
                reset = false;
        }
        else if (!roll)
        {
            if (Quaternion.Angle(t.rotation, Quaternion.Euler(Vector3.forward)) > 90)
            {
                r.velocity = Vector3.zero;
                r.angularVelocity = Vector3.zero;
                roll = true;
                reset = true;
            }
        }
        else if (!pitch)
        {
            if (Quaternion.Angle(t.rotation, Quaternion.Euler(Vector3.right)) > 90)
            {
                r.velocity = Vector3.zero;
                r.angularVelocity = Vector3.zero;
                pitch = true;
                reset = true;
            }
        }
        else if (!yaw)
        {
            if (Quaternion.Angle(t.rotation, Quaternion.Euler(Vector3.up)) > 90)
            {
                r.velocity = Vector3.zero;
                r.angularVelocity = Vector3.zero;
                yaw = true;
                reset = true;
            }
        }
        else if (!horizontal)
        {
            if (t.position.x > startPosition.x + 2 || t.position.x < startPosition.x - 2)
            {
                r.velocity = Vector3.zero;
                r.angularVelocity = Vector3.zero;
                horizontal = true;
                reset = true;
            }
        }
        else if (!vertical)
        {
            if (t.position.y > startPosition.y + 2 || t.position.y < startPosition.y - 2)
            {
                r.velocity = Vector3.zero;
                r.angularVelocity = Vector3.zero;
                vertical = true;
                reset = true;
            }
        }
        else if (!accelerate)
        {
            if (t.position.z > startPosition.z + 2 || t.position.z < startPosition.z - 2)
            {
                r.velocity = Vector3.zero;
                r.angularVelocity = Vector3.zero;
                accelerate = true;
                reset = true;
            }
        }
        else if (!firstPlay)
        {
            billboard.state = 1;
            billboardTrailLeft.Play();
            billboardTrailRight.Play();
            firstPlay = true;
        }
    }
}
