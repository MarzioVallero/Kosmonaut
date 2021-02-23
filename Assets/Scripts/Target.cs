using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Target : MonoBehaviour
{
    public Material activeMaterial;
    public float activeTime = 0.5f;
    public bool isScreen = false;
    public Camera FpsCam;
    public Camera ScreenCam;
    public Camera FplCam;
    public GameObject UICanvas;

    private AudioSource buttonAudio;
    private Renderer _renderer;
    private Material originalMaterial;
    private WaitForSeconds pressDuration;
    private Outline outline;
    private bool fpsCam = true;
    private FirstPersonLook fpl;
    private bool disableOutline = false;
    private int vitalSuppCounter = 0;
    private int compFailCounter = 0;
    private GameObject Crosshair;

    void Start()
    {
        //UICanvas = GameObject.Find("UICanvas");
        Crosshair = GameObject.Find("Crosshair");
        if (SceneManager.GetActiveScene().name == "GianTutorial" && this.gameObject.name == "Button_9")
            Crosshair.SetActive(false);

        _renderer = GetComponent<Renderer>();
        originalMaterial = _renderer.material;
        pressDuration = new WaitForSeconds(activeTime);
        outline = gameObject.AddComponent<Outline>();

        outline.OutlineMode = Outline.Mode.OutlineVisible;
        outline.OutlineColor = Color.green;
        outline.OutlineWidth = 3f;

        outline.enabled = false;

        if (_renderer == null)
        {
            Debug.LogWarning($"There is no Renderer attached to GameObject named:{gameObject.name}");
        }
        if (isScreen)
        {
            if(this.name != "Button_7")
                outline.OutlineWidth = 10f;
            FpsCam.enabled = fpsCam;
            ScreenCam.enabled = !fpsCam;
            fpl = FplCam.GetComponent<FirstPersonLook>();
        }
        if (!isScreen || this.name == "Button_7")
            buttonAudio = GameObject.Find("ButtonAudio").GetComponent<AudioSource>();
    }

    public void Activate()
    {
        if (_renderer == null)
            return;

        if (isScreen)
            SwitchCamera();
        
        if(!isScreen || this.name == "Button_7")
        {
            string buttonNumber = gameObject.name.Substring(gameObject.name.Length - 1);
            ButtonSpecificActions(int.Parse(buttonNumber));
            //StartCoroutine(TimedAction());
        }
            
    }

    /// <summary>
    /// Handles hover activation on target
    /// </summary>
    /// <param name="entrance">Set true for hover entrance, false for hover exit</param>
    public void Hover(bool entrance)
    {
        if (disableOutline) return;
        outline.enabled = entrance;
    }

    private void SwitchCamera()
    {
        if (SceneManager.GetActiveScene().name == "Tutorial" || SceneManager.GetActiveScene().name == "GianTutorial")
        {
            TutorialControl tutorialControl = GameObject.Find("Soyuz").GetComponent<TutorialControl>();
            if (tutorialControl.click <= 2)
                tutorialControl.click++;
        }

        fpsCam = !fpsCam;
        UICanvas.SetActive(fpsCam);
        FpsCam.enabled = fpsCam;
        ScreenCam.enabled = !fpsCam;
        fpl.isActive = !fpl.isActive;
        disableOutline = !disableOutline;
        outline.enabled = !outline.enabled;
    }

    private void ButtonSpecificActions(int buttonNumber)
    {
        GameObject soyuz = GameObject.Find("Soyuz");
        JoystickController joystickController = soyuz.GetComponent<JoystickController>();
        UIdata uiData = soyuz.GetComponent<UIdata>();
        DotweenController dotweenController = GetComponent<DotweenController>();

        float thrustVariation = joystickController.InitialThrust * 0.5f;
        float torqueVariation = joystickController.InitialTorque * 0.5f;

        dotweenController.RunPressAnimation();
        buttonAudio.PlayOneShot(buttonAudio.clip);

        switch (buttonNumber)
        {
            case 8:
                if (joystickController.Thrust - thrustVariation > joystickController.MinThrust)
                {
                    joystickController.Thrust -= thrustVariation;
                    joystickController.Torque -= torqueVariation;
                }
                break;
            case 9:
                if (joystickController.Thrust + thrustVariation <= joystickController.MaxThrust)
                {
                    joystickController.Thrust += thrustVariation;
                    joystickController.Torque += torqueVariation;
                }
                break;
            case 2:
                if(uiData.language == "Russian")
                {
                    uiData.language = "English";
                    uiData.status = "SEEKING  ";
                    uiData.button2.text = "LANG";
                    uiData.button3.text = "UI";
                    uiData.button4.text = "ENGINES\nRESTART";
                    uiData.button4.fontSize = 0.004f;
                    uiData.button5.text = "RESET\nCOMMS";
                    uiData.button6.text = "RESET\nVS";
                    uiData.button7.text = "TPV";
                    uiData.button8.text = "LESS\nPOWER";
                    uiData.button9.text = "MORE\nPOWER";
                }  
                else if (uiData.language == "English")
                {
                    uiData.language = "Russian";
                    uiData.status = "ЗАХВАТ  ";
                    uiData.button2.text = "язык";
                    uiData.button3.text = "пи";
                    uiData.button4.text = "ПЕРЕЗАПУСК\nДВИГАТЕЛЯ";
                    uiData.button4.fontSize = 0.003f;
                    uiData.button5.text = "СБРОС\nСВЯЗИ";
                    uiData.button6.text = "СБРОС\nЖП";
                    uiData.button7.text = "втл";
                    uiData.button8.text = "меньше\nмощность";
                    uiData.button9.text = "более\nмощность";
                }
                break;
            case 3:
                if (SceneManager.GetActiveScene().name == "Main Scene")
                {
                    RandomFailures randScript = soyuz.GetComponent<RandomFailures>();
                    if (randScript.failureType == 5)
                        compFailCounter++;
                    else
                    {
                        uiData.UIenable = !uiData.UIenable;
                        Crosshair.SetActive(uiData.UIenable);
                    }

                    if (compFailCounter == 2)
                    {
                        randScript.failure = false;
                        uiData.UIenable = true;
                        Light InteriorLight = GameObject.Find("SoyuzInteriorLight").GetComponent<Light>();
                        InteriorLight.color = Color.black;
                        compFailCounter = 0;
                        Crosshair.SetActive(true);
                    }
                }
                else
                {
                    uiData.UIenable = !uiData.UIenable;
                    Crosshair.SetActive(uiData.UIenable);
                }
                break;
            case 7:
                if(SceneManager.GetActiveScene().name == "Main Scene")
                {
                    PropulsorEffectsController effControl = soyuz.GetComponent<PropulsorEffectsController>();
                    effControl.enableEffects = !effControl.enableEffects;
                    effControl.StopAll();
                }
                break;
            case 4:
                if (SceneManager.GetActiveScene().name == "Main Scene")
                {
                    RandomFailures randScript = GameObject.Find("Soyuz").GetComponent<RandomFailures>();
                    if(randScript.failureType == 1)
                    {
                        JoystickController jcScript = soyuz.GetComponent<JoystickController>();
                        jcScript.fault = false;
                        randScript.failure = false;
                    }
                }   
                break;
            case 6:
                if (SceneManager.GetActiveScene().name == "Main Scene")
                {
                    RandomFailures randScript = soyuz.GetComponent<RandomFailures>();

                    if (randScript.failureType == 3)
                        vitalSuppCounter++;

                    if(vitalSuppCounter == 4)
                    {
                        uiData.UIenable = true;
                        Light InteriorLight = GameObject.Find("SoyuzInteriorLight").GetComponent<Light>();
                        InteriorLight.color = Color.white;
                        vitalSuppCounter = 0;
                        randScript.failure = false;
                    }
                }
                break;
            case 5:
                if (SceneManager.GetActiveScene().name == "Main Scene")
                {
                    RandomFailures randScript = soyuz.GetComponent<RandomFailures>();
                    
                    if(randScript.failureType == 4)
                    {
                        randScript.failure = false;
                        uiData.UIenable = true;
                    }
                }
                break;
        }
    }

    private IEnumerator TimedAction()
    {
        _renderer.material = activeMaterial;
        yield return pressDuration;
        _renderer.material = originalMaterial;
    }
}
