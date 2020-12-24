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
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        fpl.Toggle(true);
        targetShooter.isRaycastActive = false;
        gameIsPaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        fpl.Toggle(false);
        targetShooter.isRaycastActive = true;
        gameIsPaused = false;
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
