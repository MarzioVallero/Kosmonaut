using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFailures : MonoBehaviour
{
    public int probability = 100000;
    public int failureType = 0;
    public int occourredFailures = 0;
    public bool failure = false;

    private JoystickController jcScript;
    private Contact contactScript;
    private UIdata UIScript;
    private Camera FrontCamera;
    private Light InteriorLight;
    private GameObject Crosshair;

    void Start()
    {
        jcScript = this.GetComponent<JoystickController>();
        UIScript = this.GetComponent<UIdata>();
        FrontCamera = GameObject.Find("FrontCamera").GetComponent<Camera>();
        InteriorLight = GameObject.Find("SoyuzInteriorLight").GetComponent<Light>();
        contactScript = this.GetComponent<Contact>();
        Crosshair = GameObject.Find("Crosshair");
    }

    IEnumerator waitCameraFailure()
    {
        yield return new WaitForSeconds(20);
        FrontCamera.clearFlags = CameraClearFlags.Skybox;
        failure = false;
        UIScript.UIenable = true;
    }

    IEnumerator waitVitalSupport()
    {
        yield return new WaitForSeconds(30);
        if(failureType == 3)
        {
            Debug.Log("You ran out of oxygen.");
            contactScript.EndGame(9);
        }
    }

    void Update()
    {
        if (!failure)
        {
            InteriorLight.color = Color.white;
            failureType = Random.Range(0, probability);
            switch (failureType)
            {
                case 1:
                    failure = true;
                    Debug.Log("Engine shutdown!");
                    jcScript.fault = true;
                    occourredFailures++;
                    break;
                case 2:
                    failure = true;
                    Debug.Log("Camera failure");
                    FrontCamera.clearFlags = CameraClearFlags.Nothing;
                    UIScript.UIenable = false;
                    StartCoroutine(waitCameraFailure());
                    occourredFailures++;
                    break;
                case 3:
                    failure = true;
                    Debug.Log("Vital support failure");
                    UIScript.UIenable = false;
                    StartCoroutine(waitVitalSupport());
                    occourredFailures++;
                    break;
                case 4:
                    failure = true;
                    Debug.Log("Communications failure");
                    occourredFailures++;
                    break;
                case 5:
                    failure = true;
                    Debug.Log("Onboard computer failure, no UI");
                    UIScript.UIenable = false;
                    InteriorLight.color = Color.black;
                    Crosshair.SetActive(false);
                    occourredFailures++;
                    break;
                default:
                    break;
            }
        }
        else
        {
            if (failureType == 3)
                InteriorLight.color = Color.Lerp(Color.red, Color.black, Mathf.PingPong(Time.time, 1));
        }
            
    }
}
