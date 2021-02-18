using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropulsorEffectsController : MonoBehaviour
{
    public bool enableEffects = true;
    public bool fault = false;

    public string jType = "XBOX";

    public ParticleSystem right1;
    public ParticleSystem right2;
    public ParticleSystem left1;
    public ParticleSystem left2;
    public ParticleSystem top1;
    public ParticleSystem top2;
    public ParticleSystem bottom1;
    public ParticleSystem bottom2;
    public ParticleSystem topRight;
    public ParticleSystem bottomRight;
    public ParticleSystem topLeft;
    public ParticleSystem bottomLeft;
    public ParticleSystem frontRight;
    public ParticleSystem frontLeft;
    public ParticleSystem back1;
    public ParticleSystem back2;
    public ParticleSystem back3;
    public ParticleSystem back4;
    public ParticleSystem topFront1;
    public ParticleSystem topFront2;
    public ParticleSystem topBack1;
    public ParticleSystem topBack2;
    public ParticleSystem bottomFront1;
    public ParticleSystem bottomFront2;
    public ParticleSystem bottomBack1;
    public ParticleSystem bottomBack2;

    private void Start()
    {
        if (GameObject.Find("IndestructableData"))
        {
            jType = GameObject.Find("IndestructableData").GetComponent<SettingsManager>().controllerType;
        }
    }

    void OnEnable()
    {
        Contact.FaultyContact += TmpDisable;
        Contact.ExternalContact += Align;
    }

    void OnDisable()
    {
        Contact.FaultyContact += TmpDisable;
        Contact.ExternalContact += Align;
    }

    void Align()
    {
        enableEffects = false;
        fault = false;
    }

    void TmpDisable()
    {
        enableEffects = false;
        fault = true;
        StartCoroutine(waiter());
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(10);
        fault = false;
        enableEffects = true;
    }

    IEnumerator waiterForEndthrust()
    {
        yield return new WaitForSeconds(1.5f);
        back1.Stop();
        back2.Stop();
        back3.Stop();
        back4.Stop();
    }

    public string controllerParser(string axisName)
    {
        if (jType == "XBOX")
        {
            if (axisName == "HorizontalRight")
            {
                return "JoystickHorizontalRight";
            }
            else if (axisName == "VerticalRight")
            {
                return "JoystickVerticalRight";
            }
            else if (axisName == "TriggerRight")
            {
                return "JoystickTriggers";
            }
            else if (axisName == "TriggerLeft")
            {
                return "JoystickTriggers";
            }
        }
        else if (jType == "PS")
        {
            if (axisName == "HorizontalRight")
            {
                return "JoystickTriggers";
            }
            else if (axisName == "VerticalRight")
            {
                return "PSVerticalRight";
            }
            else if (axisName == "TriggerRight")
            {
                return "JoystickVerticalRight";
            }
            else if (axisName == "TriggerLeft")
            {
                return "JoystickHorizontalRight";
            }
        }

        return "";
    }

    void Update()
    {
        if (enableEffects && !fault)
        {
            //Handle the Input
            float h = Input.GetAxis("JoystickHorizontalLeft");
            float v = Input.GetAxis("JoystickVerticalLeft");
            float rh = Input.GetAxis(controllerParser("HorizontalRight"));
            float rv = Input.GetAxis(controllerParser("VerticalRight"));
            float trR = Input.GetAxis(controllerParser("TriggerRight"));
            float trL = Input.GetAxis(controllerParser("TriggerLeft"));
            bool rbumper = Input.GetButton("RB");
            bool lbumper = Input.GetButton("LB");

            if (h < 0.0f)
            {
                topLeft.Play();
                bottomRight.Play();
            }
            if (h > 0.0f)
            {
                topRight.Play();
                bottomLeft.Play();
            }
            if (v < 0.0f)
            {
                topBack1.Play();
                topBack2.Play();
                bottomFront1.Play();
                bottomFront2.Play();
            }
            if (v > 0.0f)
            {
                topFront1.Play();
                topFront2.Play();
                bottomBack1.Play();
                bottomBack2.Play();
            }
            if (rh < 0.0f)
            {
                right1.Play();
                right2.Play();
            }
            if (rh > 0.0f)
            {
                left1.Play();
                left2.Play();
            }
            if (rv < 0.0f)
            {
                top1.Play();
                top2.Play();
            }
            if (rv > 0.0f)
            {
                bottom1.Play();
                bottom2.Play();
            }
            if (trR > 0.0f)
            {
                back1.Play();
                back2.Play();
                back3.Play();
                back4.Play();
            }
            if ((trL < 0.0f && jType == "XBOX") || (trL > 0.0f && jType == "PS"))
            {
                frontRight.Play();
                frontLeft.Play();
            }
            if (rbumper == true && lbumper == true)
            {
                
            }
            else if (rbumper == true)
            {
                right1.Play();
                right2.Play();
                frontRight.Play();
            }
            else if (lbumper == true)
            {
                left1.Play();
                left2.Play();
                frontLeft.Play();
            }
            if (h == 0 && v == 0 && rh == 0 && rv == 0 && trR == 0 && rbumper == false && lbumper == false)
            {
                right1.Stop();
                right2.Stop();
                left1.Stop();
                left2.Stop();
                top1.Stop();
                top2.Stop();
                bottom1.Stop();
                bottom2.Stop();
                topRight.Stop();
                bottomRight.Stop();
                topLeft.Stop();
                bottomLeft.Stop();
                frontRight.Stop();
                frontLeft.Stop();
                back1.Stop();
                back2.Stop();
                back3.Stop();
                back4.Stop();
                topFront1.Stop();
                topFront2.Stop();
                topBack1.Stop();
                topBack2.Stop();
                bottomFront1.Stop();
                bottomFront2.Stop();
                bottomBack1.Stop();
                bottomBack2.Stop();
            }
        }
        else if (!enableEffects && !fault)
        {
            back1.Play();
            back2.Play();
            back3.Play();
            back4.Play();
            StartCoroutine(waiterForEndthrust());
        }
    }
}
