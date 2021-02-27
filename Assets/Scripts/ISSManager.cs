using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISSManager : MonoBehaviour
{
    private Rigidbody ISS = null;
    private ParticleSystem explosion = null;

    private void Start()
    {
        ISS = this.GetComponent<Rigidbody>();
        explosion = GameObject.Find("BigExplosion").GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        Contact.ExcessiveContact += Explode;
        Contact.ExternalContact += DeactivateColliders;
        Contact.ExcessiveContact += DeactivateColliders;
    }

    private void OnDisable()
    {
        Contact.ExcessiveContact -= Explode;
        Contact.ExternalContact -= DeactivateColliders;
        Contact.ExcessiveContact -= DeactivateColliders;
    }

    IEnumerator waitAfterExplosion()
    {
        yield return new WaitForSeconds(1.5f);
        explosion.Stop();
    }

    void DeactivateColliders()
    {
        Debug.Log("Colliders Deactivated.");
        foreach (Transform child in gameObject.GetComponentsInChildren<Transform>())
        {
            Destroy(child.gameObject.GetComponent<Collider>());
        }
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
                    Destroy(child.gameObject.GetComponent<Collider>());
                child.gameObject.AddComponent<BoxCollider>();
                child.GetComponent<Rigidbody>().AddExplosionForce(1f, ISS.position, 10f);
            }
        }

        explosion.Play();
        //Destroy(explosion, explosion.main.duration);
        Contact.ExcessiveContact -= Explode;
        StartCoroutine(waitAfterExplosion());
    }
}
