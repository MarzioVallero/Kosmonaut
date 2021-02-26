using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoyuzSpawner : MonoBehaviour
{
    public Rigidbody Soyuz;
    private float RangePosition = 200f;
    private float RangeAngle = 180f;

    void Start()
    {
        Soyuz = GameObject.Find("Soyuz").GetComponent<Rigidbody>();

        Vector3 randomPosition = new Vector3(Random.Range(-RangePosition, RangePosition), Random.Range(-RangePosition, RangePosition), Random.Range(-RangePosition / 10, RangePosition / 10));
        Soyuz.transform.Translate(randomPosition, Space.Self);
        Vector3 randomAngle = new Vector3(Random.Range(-RangeAngle, RangeAngle), Random.Range(-RangeAngle, RangeAngle), Random.Range(-RangeAngle, RangeAngle));
        Soyuz.transform.Rotate(randomAngle , Space.Self);
        Soyuz.AddTorque(randomAngle, ForceMode.Impulse);
    }
}
