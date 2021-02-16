using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contact : MonoBehaviour
{    
    [SerializeField] private GameObject menuCanvas;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject endGame;
    public delegate void ContactAction();
    public static event ContactAction FaultyContact;
    public static event ContactAction ExternalContact;
    public static event ContactAction ExcessiveContact;

    public GameObject ExternalTarget;
    public Rigidbody Soyuz;
    public float MaxForce = 0.05f;
    public float MaxAngleImpact = 0.035f;
    public bool autodestruct = false;

    private float repulseForce = 2000;
    private float repulseRotation = 200;

    private void OnTriggerEnter(Collider collision)
    {
        float impactAngle = Vector3.Angle(Soyuz.transform.position, ExternalTarget.transform.position);
        float velocity = Soyuz.velocity.z;
        if (collision.gameObject == ExternalTarget && impactAngle < 0.1 && velocity < 0.5)
        {
            Debug.Log("Alignment!");
            menuCanvas.GetComponent<PauseMenu>().Pause();
            pauseMenu.SetActive(false);
            endGame.SetActive(true);
            if (ExternalContact != null)
                ExternalContact();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject rb = collision.rigidbody.gameObject;
        if (rb == null)
            return;

        float force = collision.relativeVelocity.magnitude;
        if (force < MaxForce)
        {
            Vector3 repulseDir = Soyuz.transform.position - rb.transform.position;
            repulseDir.Normalize();
            Debug.Log("Failed Contact. Manual control damage reported. Wait for manual control reboot");
            Soyuz.AddForce(repulseDir * repulseForce, ForceMode.Impulse);
            Soyuz.AddTorque(repulseDir * repulseRotation, ForceMode.Impulse);
            if (FaultyContact != null)
                FaultyContact();
        }
        else
        {
            if (ExcessiveContact != null)
                ExcessiveContact();
        }
    }

    private void Update()
    {
        if (autodestruct && ExcessiveContact != null)
                ExcessiveContact();
    }
}