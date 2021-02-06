using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject fpcontroller;

    private FirstPersonLook fpl;
    private TargetShooter targetShooter;

    private void Start()
    {
        targetShooter = fpcontroller.GetComponent<TargetShooter>();
        fpl = fpcontroller.GetComponentInChildren<FirstPersonLook>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused) Resume();
            else Pause();
        }
    }

    public void Pause()
    {
        GameObject soyuz = GameObject.Find("Soyuz");
        JoystickController joystickController = soyuz.GetComponent<JoystickController>();

        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        fpl.Toggle(true);
        targetShooter.isRaycastActive = false;
        joystickController.ResetVibration(true);
        gameIsPaused = true;        
    }

    public void Resume()
    {
        GameObject soyuz = GameObject.Find("Soyuz");
        JoystickController joystickController = soyuz.GetComponent<JoystickController>();

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        fpl.Toggle(false);
        targetShooter.isRaycastActive = true;
        joystickController.ResetVibration(false);
        gameIsPaused = false;
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
