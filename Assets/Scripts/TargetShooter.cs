using UnityEngine;
using System.Collections;

public class TargetShooter : MonoBehaviour
{
    public float range = 50f;
    public float rate = 15f;
    public Camera fpscam;

    private WaitForSeconds shotDuration = new WaitForSeconds(0.3f);
    private bool laserLine;
    private float nextTimeToPress = 0f;
    private Target previousTarget;

    void Start()
    {
        previousTarget = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (laserLine)
        {
            Debug.DrawRay(fpscam.transform.position, fpscam.transform.forward * range, Color.blue);
        }
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToPress)
        {
            nextTimeToPress = Time.time + 1f/rate;
            StartCoroutine(DebugRay());
            ButtonInteraction(true);
        }

        ButtonInteraction(false);
    }

    /// <summary>
    /// Handles hover and click interactions with buttons
    /// </summary>
    /// <param name="activate">Set true if you want to activate the button, false if you want just the hover interaction</param>
    void ButtonInteraction(bool activate)
    {
        RaycastHit hit;        

        if (Physics.Raycast(fpscam.transform.position, fpscam.transform.forward, out hit, range))
        {
            Target target = hit.transform.GetComponent<Target>();

            if (previousTarget != target && previousTarget != null) previousTarget.Hover(false);

            previousTarget = target;

            if(target != null)
            {
                if (activate == true) target.Activate();
                else target.Hover(true);
            }            
        }
        else if(previousTarget != null) previousTarget.Hover(false);
    }

    private IEnumerator DebugRay()
    {        
        laserLine = true;
        yield return shotDuration;
        laserLine = false;
    }
}
