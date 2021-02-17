using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Contact : MonoBehaviour
{    
    [SerializeField] private GameObject menuCanvas;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject endGame;
    [SerializeField] private GameObject gameOver;
    public delegate void ContactAction();
    public static event ContactAction FaultyContact;
    public static event ContactAction ExternalContact;
    public static event ContactAction ExcessiveContact;

    public GameObject ExternalTarget;
    public Rigidbody Soyuz;
    public float MaxSpeed = 0.08f;
    public float MaxAngleImpact = 0.035f;
    public bool autodestruct = false;

    private float repulseForce = 2000;
    private float repulseRotation = 200;
    private WaitForSeconds waitTime;

    private void OnTriggerEnter(Collider collision)
    {
        float impactAngle = Vector3.Angle(Soyuz.transform.position, ExternalTarget.transform.position);
        float velocity = Soyuz.velocity.z;
        if (collision.gameObject == ExternalTarget && impactAngle < 0.1 && velocity < 0.05)
        {
            Debug.Log("Alignment! " + impactAngle + " " + velocity);
            EndGame(true);            
            if (ExternalContact != null)
                ExternalContact();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject rb = collision.rigidbody.gameObject;
        if (rb == null)
            return;

        float speed = collision.relativeVelocity.magnitude;
        if (speed < MaxSpeed)
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
                Debug.Log("Excessive contact");
                EndGame(false);
        }
    }

    private void Update()
    {
        if (autodestruct && ExcessiveContact != null)
        {
            ExcessiveContact();
            EndGame(false);
            autodestruct = false;
        }
    }

    private void EndGame(bool victory)
    {
        menuCanvas.GetComponent<PauseMenu>().Pause();
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        waitTime = new WaitForSeconds(4f);
        StartCoroutine(TimedAction());

        if (SceneManager.GetActiveScene().name == "Main Scene")
        {
            Target endGameCamera = GameObject.Find("EndGameView").GetComponent<Target>();
            endGameCamera.Activate();
        }

        if (victory)
        {
            endGame.SetActive(true);
        }
        else
        {            
            gameOver.SetActive(true);            
        }
    }

    private IEnumerator TimedAction()
    {
        yield return waitTime;
        Time.timeScale = 0f;
    }
}