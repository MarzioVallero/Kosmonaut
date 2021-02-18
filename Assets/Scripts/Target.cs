﻿using UnityEngine;
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

    private Renderer _renderer;
    private Material originalMaterial;
    private WaitForSeconds pressDuration;
    private Outline outline;
    private bool fpsCam = true;
    private FirstPersonLook fpl;
    private bool disableOutline = false;

    void Start()
    {
        //UICanvas = GameObject.Find("UICanvas");

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

        switch (buttonNumber)
        {
            case 8:
                dotweenController.RunPressAnimation();
                if (joystickController.Thrust - thrustVariation > joystickController.MinThrust)
                {
                    joystickController.Thrust -= thrustVariation;
                    joystickController.Torque -= torqueVariation;
                }
                break;
            case 9:
                dotweenController.RunPressAnimation();
                if (joystickController.Thrust + thrustVariation <= joystickController.MaxThrust)
                {
                    joystickController.Thrust += thrustVariation;
                    joystickController.Torque += torqueVariation;
                }
                break;
            case 2:
                dotweenController.RunPressAnimation();
                if(uiData.language == "Russian")
                {
                    uiData.language = "English";
                    uiData.status = "SEEKING  ";
                    uiData.button2.text = "LANG";
                    uiData.button3.text = "UI";
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
                    uiData.button7.text = "втл";
                    uiData.button8.text = "меньше\nмощность";
                    uiData.button9.text = "более\nмощность";
                }
                break;
            case 3:
                dotweenController.RunPressAnimation();
                uiData.UIenable = !uiData.UIenable;
                break;
            case 7:
                dotweenController.RunPressAnimation();
                if(SceneManager.GetActiveScene().name == "Main Scene")
                {
                    PropulsorEffectsController effControl = soyuz.GetComponent<PropulsorEffectsController>();
                    effControl.enableEffects = !effControl.enableEffects;
                    effControl.StopAll();
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
