using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    public float AngularSpeed = 0.004178074f;
    private Transform earth;
    private float rotY;

    private void Start()
    {
        earth = GetComponent<Transform>();
        rotY = earth.rotation.eulerAngles.y;
    }
    void Update()
    {
        rotY += AngularSpeed * Time.deltaTime;
        transform.rotation = Quaternion.AngleAxis(rotY, Vector3.up);
    }
}
