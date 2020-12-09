using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure; // Required in C#

public class JoystickController : MonoBehaviour
{
    public Camera Camera;
    public float Torque = 0.5f;
    public float Thrust = 129f;
    public Rigidbody rb;
    public bool enable = true;
    public bool fault = false;
    GamePadState state;
    PlayerIndex playerIndex;

    private float intensity = 0f;
    private Vector3 finalPosition = new Vector3 (-2.3073f, 34.21982f, 253.379f);

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

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(10);
        fault = false;
        enable = true;
    }

    void Update()
    {
        if (enable && !fault)
        {
            //Handle the Input
            float h = Input.GetAxis("JoystickHorizontalLeft");
            float v = Input.GetAxis("JoystickVerticalLeft");
            float rh = Input.GetAxis("JoystickHorizontalRight");
            float rv = Input.GetAxis("JoystickVerticalRight");
            float tr = Input.GetAxis("JoystickTriggers");
            bool rbumper = Input.GetButton("RB");
            bool lbumper = Input.GetButton("LB");
            state = GamePad.GetState(playerIndex);

            if (h < 0.0f)
            {
                rb.AddRelativeTorque(Vector3.forward * Torque, ForceMode.Impulse);
                intensity += 0.005f;
                GamePad.SetVibration(playerIndex, intensity, 0.0f);
            }
            if (h > 0.0f)
            {
                rb.AddRelativeTorque(Vector3.back * Torque, ForceMode.Impulse);
                intensity += 0.005f;
                GamePad.SetVibration(playerIndex, intensity, 0.0f);
            }
            if (v < 0.0f)
            {
                rb.AddRelativeTorque(Vector3.right * Torque, ForceMode.Impulse);
                intensity += 0.005f;
                GamePad.SetVibration(playerIndex, intensity, 0.0f);
            }
            if (v > 0.0f)
            {
                rb.AddRelativeTorque(Vector3.left * Torque, ForceMode.Impulse);
                intensity += 0.005f;
                GamePad.SetVibration(playerIndex, intensity, 0.0f);
            }
            if (rh < 0.0f)
            {
                rb.AddRelativeForce(Vector3.left * Thrust);
                intensity += 0.005f;
                GamePad.SetVibration(playerIndex, 0.0f, intensity);
            }
            if (rh > 0.0f)
            {
                rb.AddRelativeForce(Vector3.right * Thrust);
                intensity += 0.005f;
                GamePad.SetVibration(playerIndex, 0.0f, intensity);
            }
            if (rv < 0.0f)
            {
                rb.AddRelativeForce(Vector3.down * Thrust);
                intensity += 0.005f;
                GamePad.SetVibration(playerIndex, 0.0f, intensity);
            }
            if (rv > 0.0f)
            {
                rb.AddRelativeForce(Vector3.up * Thrust);
                intensity += 0.005f;
                GamePad.SetVibration(playerIndex, 0.0f, intensity);
            }
            if (tr > 0.0f)
            {
                rb.AddRelativeForce(Vector3.forward * Thrust);
                GamePad.SetVibration(playerIndex, state.Triggers.Left, state.Triggers.Right);
            }
            if (tr < 0.0f)
            {
                rb.AddRelativeForce(Vector3.back * Thrust);
                GamePad.SetVibration(playerIndex, state.Triggers.Left, state.Triggers.Right);
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
                GamePad.SetVibration(playerIndex, 0.0f, intensity);
            }
            else if (lbumper == true)
            {
                rb.AddRelativeTorque(Vector3.down * Torque, ForceMode.Impulse);
                intensity += 0.005f;
                GamePad.SetVibration(playerIndex, intensity, 0.0f);
            }
            if (h == 0 && v == 0 && rh == 0 && rv == 0 && tr == 0 && rbumper == false && lbumper == false)
            {
                if (intensity > 0.0f)
                    intensity -= 0.2f;
                else
                    intensity = 0.18f;
                GamePad.SetVibration(playerIndex, intensity, intensity);
            }
        }
        else if (!enable && !fault)
        {
            if (Vector3.Angle(rb.transform.forward, finalPosition) > 0.1f || Vector3.Distance(rb.transform.position, finalPosition) > 0.01f)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                var desiredRotQ = Quaternion.Euler(0.0f, 0.0f, 45.3f);
                rb.transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotQ, Time.deltaTime);
                Vector3 finalPosition = new Vector3(-2.3073f, 34.21982f, 253.379f);
                rb.transform.position = Vector3.Lerp(transform.position, finalPosition, 0.005f * Time.deltaTime);
            }
        }
    }
}