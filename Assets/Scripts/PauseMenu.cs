using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class PauseMenu : MonoBehaviour
{
    public bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;
    public GameObject fpcontroller;
    public GameObject UICanvas;
    public AudioSource backgroundAudio;

    private FirstPersonLook fpl;
    private TargetShooter targetShooter;
    private MoveBillboard billboard;
    private VideoPlayer videoPlayer;

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
        UICanvas.SetActive(false);
        Time.timeScale = 0f;
        fpl.Toggle(true);
        targetShooter.isRaycastActive = false;
        joystickController.ResetVibration(true);
        gameIsPaused = true;

        backgroundAudio.Pause();
        PauseResumeVideo(true);
    }

    public void Resume()
    {
        GameObject soyuz = GameObject.Find("Soyuz");
        JoystickController joystickController = soyuz.GetComponent<JoystickController>();

        if (optionsMenuUI.activeSelf) optionsMenuUI.SetActive(false);

        pauseMenuUI.SetActive(false);
        UICanvas.SetActive(true);
        Time.timeScale = 1f;
        fpl.Toggle(false);
        targetShooter.isRaycastActive = true;
        joystickController.ResetVibration(false);
        gameIsPaused = false;

        backgroundAudio.Play();
        PauseResumeVideo(false);
    }

    private void PauseResumeVideo(bool pause)
    {
        if (GameObject.Find("scifiBillboard"))
        {
            billboard = GameObject.Find("scifiBillboard").GetComponent<MoveBillboard>();
            videoPlayer = billboard.GetComponent<VideoPlayer>();

            if (pause) videoPlayer.Pause();
            else videoPlayer.Play();
        }
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadMainLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadTutorial()
    {
        SceneManager.LoadScene(2);
    }
}
