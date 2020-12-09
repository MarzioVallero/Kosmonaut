using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISSExploder : MonoBehaviour
{
    public Rigidbody ISS;
    public ParticleSystem explosion;

    private void OnEnable()
    {
        Contact.ExcessiveContact += Explode;
    }

    private void OnDisable()
    {
        Contact.ExcessiveContact -= Explode;
    }

    void Explode()
    {
        Debug.Log("Explosion!");
        foreach (Transform child in gameObject.GetComponentsInChildren<Transform>())
        {
            if (child.gameObject.TryGetComponent<Rigidbody>(out Rigidbody component) == true)
            {
                child.GetComponent<Rigidbody>().isKinematic = false;
                if (child.GetComponent<Collider>().GetType() == typeof(MeshCollider))
                {
                    Destroy(child.gameObject.GetComponent<Collider>());
                    child.gameObject.AddComponent<BoxCollider>();
                }
                child.GetComponent<Rigidbody>().AddExplosionForce(1f, ISS.position, 3f);
            }
        }

        explosion.Play();
        Destroy(explosion, explosion.main.duration);
        Contact.ExcessiveContact -= Explode;
    }
}
