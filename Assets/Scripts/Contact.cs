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
            EndGame(1);            
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
                EndGame(2);
        }
    }

    private void Update()
    {
        if (autodestruct && ExcessiveContact != null)
        {
            ExcessiveContact();
            EndGame(2);
            autodestruct = false;
        }
    }

    public void EndGame(int endGameState)
    {
        menuCanvas.GetComponent<PauseMenu>().Pause();
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        waitTime = new WaitForSeconds(4f);
        StartCoroutine(TimedAction(endGameState));
    }

    private IEnumerator TimedAction(int endGameState)
    {
        yield return waitTime;
        Time.timeScale = 0f;
        if (SceneManager.GetActiveScene().name == "Main Scene")
        {
            Target endGameCamera = GameObject.Find("EndGameView").GetComponent<Target>();
            endGameCamera.Activate();
        }

        TMPro.TextMeshProUGUI tmpText = gameOver.GetComponentInChildren<TMPro.TextMeshProUGUI>();

        switch (endGameState)
        {
            case 1:
                endGame.SetActive(true);
                break;
            case 2:
                gameOver.SetActive(true);
                tmpText.text = "After a human error, the ISS has been destroyed.\nLuckyly, that wasn't the real one.";
                break;
            case 3:
                gameOver.SetActive(true);
                tmpText.text = "Soyuz escaped Earth's gravitational pull\nand is now stranded in space.";
                break;
            case 4:
                gameOver.SetActive(true);
                tmpText.text = "Soyuz lost too much altitude\nand fell back on Earth.";
                break;
            case 5:
                gameOver.SetActive(true);
                tmpText.text = "Soyuz missed the Rendevouz window and has been forced to perform reentry.";
                break;
            case 6:
                gameOver.SetActive(true);
                tmpText.text = "Soyuz missed the Rendevouz window and fell towards the austral emisphere.";
                break;
            case 7:
                gameOver.SetActive(true);
                tmpText.text = "Soyuz approached the ISS too fast, missing it and forcing a reentry.";
                break;
            case 8:
                gameOver.SetActive(true);
                tmpText.text = "Soyuz lost too much approach velocity and missed the ISS completely.";
                break;
            case 9:
                gameOver.SetActive(true);
                tmpText.text = "After a vital support failure, the crew didn't manage to reboot the systems.";
                break;
        }
    }
}