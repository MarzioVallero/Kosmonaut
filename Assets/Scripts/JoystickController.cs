﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure; // Required in C#

public class JoystickController : MonoBehaviour
{
    public delegate void ThrustAction(string name);
    public static event ThrustAction FireThruster;
    public static event ThrustAction StopThruster;
    private SettingsManager settingsManager;

    public Camera Camera;
    public float InitialTorque = 0.5f;
    public float InitialThrust = 129f;
    public float MinTorque = 0.25f;
    public float MinThrust = 64.5f;
    public float MaxTorque = 1f;
    public float MaxThrust = 258f;
    public Rigidbody rb;
    public GameObject ZvezdaExternalCollider;
    public bool enable = true;    
    public bool fault = false;
    public bool enableVibration = true;
    public string jType = "XBOX";
    [HideInInspector] public float Thrust;
    [HideInInspector] public float Torque;
    bool lastVibrationSetting = true;
    GamePadState state;
    PlayerIndex playerIndex;

    private float intensity = 0f;
    private bool cheatCodeActive = false;

    void Start()
    {
        rb = GameObject.Find("Soyuz").GetComponent<Rigidbody>();
        ZvezdaExternalCollider = GameObject.Find("ZvezdaExternalCollider");

        Thrust = InitialThrust;
        Torque = InitialTorque;

        if (GameObject.Find("IndestructableData"))
        {
            GameObject tmp = GameObject.Find("IndestructableData");
            settingsManager = tmp.GetComponent<SettingsManager>();
            jType = settingsManager.controllerType;
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
        enable = false;
        fault = false;
        GamePad.SetVibration(playerIndex, 0.0f, 0.0f);
    }

    void TmpDisable()
    {
        enable = false;
        fault = true;
        StartCoroutine(waiter());
    }

    void VibrateController(PlayerIndex playerIndex, float leftMotor, float rightMotor)
    {
        if (!enableVibration) return;
        GamePad.SetVibration(playerIndex, leftMotor, rightMotor);
    }

    public void SetVibration(bool setting)
    {
        enableVibration = setting;
        lastVibrationSetting = setting;
    }

    public void ResetVibration(bool pause)
    {
        if (pause)
        {
            enableVibration = false;
            GamePad.SetVibration(playerIndex, 0, 0);
        }
        else
        {
            enableVibration = lastVibrationSetting;
        }
        
    }

    void CheatCodes()
    {
        if (Input.GetKey(KeyCode.M) && Input.GetKey(KeyCode.G) && Input.GetKey(KeyCode.A))
            cheatCodeActive = true;
        if (cheatCodeActive)
        {
            if (Input.GetKeyDown(KeyCode.Z))
                enable = !enable;
            if (Input.GetKeyDown(KeyCode.X))
                gameObject.GetComponent<Contact>().autodestruct = true;
        }        
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(10);
        fault = false;
        enable = true;
    }

    public string controllerParser(string axisName)
    {        
        if(jType == "XBOX")
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
        CheatCodes();
        if (enable && !fault)
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
            state = GamePad.GetState(playerIndex);

            if (h < 0.0f)
            {
                rb.AddRelativeTorque(Vector3.forward * Torque, ForceMode.Impulse);
                intensity += 0.005f;
                VibrateController(playerIndex, intensity, 0.0f);
                if (FireThruster != null)
                {
                    FireThruster("UpThrusterAudio");
                    FireThruster("RightThrusterAudio");
                }
            }
            if (h > 0.0f)
            {
                rb.AddRelativeTorque(Vector3.back * Torque, ForceMode.Impulse);
                intensity += 0.005f;
                VibrateController(playerIndex, intensity, 0.0f);
                if (FireThruster != null)
                {
                    FireThruster("LeftThrusterAudio");
                    FireThruster("DownThrusterAudio");
                }
            }
            if (v < 0.0f)
            {
                rb.AddRelativeTorque(Vector3.right * Torque, ForceMode.Impulse);
                intensity += 0.005f;
                VibrateController(playerIndex, intensity, 0.0f);
                if (FireThruster != null)
                {
                    FireThruster("UpThrusterAudio");
                    FireThruster("FrontThrusterAudio");
                }
            }
            if (v > 0.0f)
            {
                rb.AddRelativeTorque(Vector3.left * Torque, ForceMode.Impulse);
                intensity += 0.005f;
                VibrateController(playerIndex, intensity, 0.0f);
                if (FireThruster != null)
                {
                    FireThruster("DownThrusterAudio");
                    FireThruster("FrontThrusterAudio");
                }
            }
            if (rh < 0.0f)
            {
                rb.AddRelativeForce(Vector3.left * Thrust);
                intensity += 0.005f;
                VibrateController(playerIndex, 0.0f, intensity);
                if (FireThruster != null)
                    FireThruster("RightThrusterAudio");
            }
            if (rh > 0.0f)
            {
                rb.AddRelativeForce(Vector3.right * Thrust);
                intensity += 0.005f;
                VibrateController(playerIndex, 0.0f, intensity);
                if (FireThruster != null)
                    FireThruster("LeftThrusterAudio");
            }
            if (rv < 0.0f)
            {
                rb.AddRelativeForce(Vector3.down * Thrust);
                intensity += 0.005f;
                VibrateController(playerIndex, 0.0f, intensity);
                if (FireThruster != null)
                    FireThruster("UpThrusterAudio");
            }
            if (rv > 0.0f)
            {
                rb.AddRelativeForce(Vector3.up * Thrust);
                intensity += 0.005f;
                VibrateController(playerIndex, 0.0f, intensity);
                if (FireThruster != null)
                    FireThruster("DownThrusterAudio");
            }
            if (trR > 0.0f)
            {
                rb.AddRelativeForce(Vector3.forward * Thrust);
                VibrateController(playerIndex, state.Triggers.Left, state.Triggers.Right);
                if (FireThruster != null)
                    FireThruster("BackThrusterAudio");
            }
            if ((trL < 0.0f && jType == "XBOX") || (trL > 0.0f && jType == "PS"))
            {
                rb.AddRelativeForce(Vector3.back * Thrust);
                VibrateController(playerIndex, state.Triggers.Left, state.Triggers.Right);
                if (FireThruster != null)
                    FireThruster("FrontThrusterAudio");
            }
            if (rbumper == true && lbumper == true)
            {
                rb.velocity *= 0.998f;
                rb.angularVelocity *= 0.998f;
                if (rb.velocity.magnitude < 0.01)
                {
                    rb.velocity = Vector3.zero;
                }
                if (rb.angularVelocity.magnitude < 0.01)
                {
                    rb.angularVelocity = Vector3.zero;
                }
            }
            else if (rbumper == true)
            {
                rb.AddRelativeTorque(Vector3.up * Torque, ForceMode.Impulse);
                intensity += 0.005f;
                VibrateController(playerIndex, 0.0f, intensity);
                if (FireThruster != null)
                {
                    FireThruster("LeftThrusterAudio");
                    FireThruster("BackThrusterAudio");
                }
            }
            else if (lbumper == true)
            {
                rb.AddRelativeTorque(Vector3.down * Torque, ForceMode.Impulse);
                intensity += 0.005f;
                VibrateController(playerIndex, intensity, 0.0f);
                if (FireThruster != null)
                {
                    FireThruster("RightThrusterAudio");
                    FireThruster("FrontThrusterAudio");
                }
            }
            if (h == 0 && v == 0 && rh == 0 && rv == 0 && trR == 0 && rbumper == false && lbumper == false)
            {
                if (intensity > 0.0f)
                    intensity -= 0.2f;
                else
                    intensity = 0.18f;
                VibrateController(playerIndex, intensity, intensity);

                foreach (AudioSource child in rb.gameObject.GetComponentsInChildren<AudioSource>())
                {
                    if (child.playOnAwake == false && StopThruster != null)
                        StopThruster(child.gameObject.name.ToString());
                }
            }
        }
        else if (!enable && !fault)
        {
            if (Vector3.Angle(rb.transform.forward, ZvezdaExternalCollider.transform.forward) > 90.001f || Vector3.Angle(rb.transform.forward, ZvezdaExternalCollider.transform.forward) < 89.999f || Vector3.Distance(rb.transform.position, ZvezdaExternalCollider.transform.position) > 0.85f)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                var desiredRotQ = Quaternion.AngleAxis(44.126f - ZvezdaExternalCollider.transform.rotation.eulerAngles.x, -ZvezdaExternalCollider.transform.right);
                rb.transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotQ, Time.deltaTime);
                rb.transform.position = Vector3.Lerp(transform.position, (ZvezdaExternalCollider.transform.position - new Vector3(0f, 0f, 0.85f)), 0.2f * Time.deltaTime);
            }
        }
    }
}