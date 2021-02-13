using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBillboard : MonoBehaviour
{
    public int state = 0;
    private ParticleSystem[] particles;

    void OnEnable()
    {
        particles = this.GetComponentsInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case 0:

                break;
            case 1:
                if (this.transform.position.y < 70)
                    this.transform.Translate(Vector3.up * 10 * Time.deltaTime, Space.World);
                else
                    state = 2;
                break;
            case 2:
                foreach (ParticleSystem particle in particles)
                    particle.Stop();
                break;
        }
    }
}
