using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure; // Required in C#

public class JoystickController : MonoBehaviour
{
    public delegate void ThrustAction(string name);
    public static event ThrustAction FireThruster;
    public static event ThrustAction StopThruster;

    public Camera Camera;
    public float Torque = 0.5f;
    public float Thrust = 129f;
    public Rigidbody rb;
    public GameObject ZvezdaExternalCollider;
    public bool enable = true;
    public bool fault = false;
    GamePadState state;
    PlayerIndex playerIndex;

    private float intensity = 0f;

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

    void TmpDisable()    {
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
                GamePad.SetVibration(playerIndex, intensity, 0.0f);
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
                GamePad.SetVibration(playerIndex, intensity, 0.0f);
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
                GamePad.SetVibration(playerIndex, intensity, 0.0f);
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
                GamePad.SetVibration(playerIndex, 0.0f, intensity);
                if (FireThruster != null)
                    FireThruster("RightThrusterAudio");
            }
            if (rh > 0.0f)
            {
                rb.AddRelativeForce(Vector3.right * Thrust);
                intensity += 0.005f;
                GamePad.SetVibration(playerIndex, 0.0f, intensity);
                if (FireThruster != null)
                    FireThruster("LeftThrusterAudio");
            }
            if (rv < 0.0f)
            {
                rb.AddRelativeForce(Vector3.down * Thrust);
                intensity += 0.005f;
                GamePad.SetVibration(playerIndex, 0.0f, intensity);
                if (FireThruster != null)
                    FireThruster("UpThrusterAudio");
            }
            if (rv > 0.0f)
            {
                rb.AddRelativeForce(Vector3.up * Thrust);
                intensity += 0.005f;
                GamePad.SetVibration(playerIndex, 0.0f, intensity);
                if (FireThruster != null)
                    FireThruster("DownThrusterAudio");
            }
            if (tr > 0.0f)
            {
                rb.AddRelativeForce(Vector3.forward * Thrust);
                GamePad.SetVibration(playerIndex, state.Triggers.Left, state.Triggers.Right);
                if (FireThruster != null)
                    FireThruster("BackThrusterAudio");
            }
            if (tr < 0.0f)
            {
                rb.AddRelativeForce(Vector3.back * Thrust);
                GamePad.SetVibration(playerIndex, state.Triggers.Left, state.Triggers.Right);
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
                GamePad.SetVibration(playerIndex, 0.0f, intensity);
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
                GamePad.SetVibration(playerIndex, intensity, 0.0f);
                if (FireThruster != null)
                {
                    FireThruster("RightThrusterAudio");
                    FireThruster("FrontThrusterAudio");
                }
            }
            if (h == 0 && v == 0 && rh == 0 && rv == 0 && tr == 0 && rbumper == false && lbumper == false)
            {
                if (intensity > 0.0f)
                    intensity -= 0.2f;
                else
                    intensity = 0.18f;
                GamePad.SetVibration(playerIndex, intensity, intensity);

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
                var desiredRotQ = Quaternion.Euler(0.0f, 0.0f, 44.126f);
                rb.transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotQ, Time.deltaTime);
                rb.transform.position = Vector3.Lerp(transform.position, (ZvezdaExternalCollider.transform.position - new Vector3 (0f, 0f, 0.85f)), 0.2f * Time.deltaTime);
            }
        }
    }
}