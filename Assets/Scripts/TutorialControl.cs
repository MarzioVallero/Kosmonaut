using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine;

public class TutorialControl : MonoBehaviour
{
    private bool reset = false;
    private bool roll = false;
    private bool pitch = false;
    private bool yaw = false;
    private bool horizontal = false;
    private bool vertical = false;
    private bool accelerate = false;
    private bool firstPlay = false;
    /*[HideInInspector]*/ public int click = 0;
    private bool timed1 = false;
    private bool timed2 = false;
    private WaitForSeconds slideDuration;    
    private Transform t;
    private Rigidbody r;
    private Vector3 startPosition;
    private MoveBillboard billboard;
    private VideoPlayer videoPlayer;
    private GameObject tutorialCanvas;
    private GameObject one;
    private GameObject two;
    private GameObject three;
    private GameObject four;
    public ParticleSystem billboardTrailLeft;
    public ParticleSystem billboardTrailRight;    
    [SerializeField] private int angleForCompletion = 45;
    [SerializeField] private float metersForCompletion = 1f;
    [SerializeField] private float activeTime = 3f;
    [SerializeField] private VideoClip rollClip;
    [SerializeField] private VideoClip pitchClip;
    [SerializeField] private VideoClip yawClip;
    [SerializeField] private VideoClip horizontalClip;
    [SerializeField] private VideoClip verticalClip;
    [SerializeField] private VideoClip accelerateClip;


    // Start is called before the first frame update
    void Start()
    {
        t = this.GetComponent<Transform>();
        r = this.GetComponent<Rigidbody>();
        startPosition = t.position;
        billboard = GameObject.Find("scifiBillboard").GetComponent<MoveBillboard>();
        videoPlayer = billboard.GetComponent<VideoPlayer>();
        tutorialCanvas = GameObject.Find("TutorialCanvas");
        one = GameObject.Find("1");
        two = GameObject.Find("2");
        three = GameObject.Find("3");
        four = GameObject.Find("4");
        two.SetActive(false);
        three.SetActive(false);
        four.SetActive(false);
        slideDuration = new WaitForSeconds(activeTime);
    }

    void TutorialIntro()
    {
        switch (click)
        {
            case 1:
                one.SetActive(false);
                two.SetActive(true);
                break;
            case 2:
                two.SetActive(false);
                three.SetActive(true);
                break;
        }
        if (three.activeSelf && !timed1)
        {
            StartCoroutine(TimedAction1());
        }
        if (four.activeSelf && !timed2)
        {
            StartCoroutine(TimedAction2());
        }
    }

    private IEnumerator TimedAction1()
    {
        Debug.Log("start1");
        timed1 = true;
        yield return slideDuration;
        three.SetActive(false);
        four.SetActive(true);
        Debug.Log("end1");
    }

    private IEnumerator TimedAction2()
    {
        Debug.Log("start2");
        timed2 = true;
        yield return slideDuration;
        four.SetActive(false);
        tutorialCanvas.SetActive(false);
        Debug.Log("end2");
    }

    // Update is called once per frame
    void Update()
    {
        TutorialIntro();
        if (reset)
        {
            Vector3 dir = startPosition - t.position;
            t.Translate(dir * 2.0f * Time.deltaTime, Space.World);
            t.rotation = Quaternion.RotateTowards(t.rotation, Quaternion.Euler(Vector3.forward), 1.0f);
            if ((1 - Mathf.Abs(Quaternion.Dot(t.rotation, Quaternion.Euler(Vector3.forward)))) < 0.001f && Vector3.Distance(t.position, startPosition) < 0.2f)
                reset = false;
        }
        else if (!roll)
        {
            videoPlayer.clip = rollClip;
            if (Quaternion.Angle(t.rotation, Quaternion.Euler(Vector3.forward)) > angleForCompletion)
            {
                r.velocity = Vector3.zero;
                r.angularVelocity = Vector3.zero;
                roll = true;
                reset = true;
            }
        }
        else if (!pitch)
        {
            videoPlayer.clip = pitchClip;
            if (Quaternion.Angle(t.rotation, Quaternion.Euler(Vector3.right)) > angleForCompletion)
            {
                r.velocity = Vector3.zero;
                r.angularVelocity = Vector3.zero;
                pitch = true;
                reset = true;
            }
        }
        else if (!yaw)
        {
            videoPlayer.clip = yawClip;
            if (Quaternion.Angle(t.rotation, Quaternion.Euler(Vector3.up)) > angleForCompletion)
            {
                r.velocity = Vector3.zero;
                r.angularVelocity = Vector3.zero;
                yaw = true;
                reset = true;
            }
        }
        else if (!horizontal)
        {
            videoPlayer.clip = horizontalClip;
            if (t.position.x > startPosition.x + metersForCompletion || t.position.x < startPosition.x - metersForCompletion)
            {
                r.velocity = Vector3.zero;
                r.angularVelocity = Vector3.zero;
                horizontal = true;
                reset = true;
            }
        }
        else if (!vertical)
        {
            videoPlayer.clip = verticalClip;
            if (t.position.y > startPosition.y + metersForCompletion || t.position.y < startPosition.y - metersForCompletion)
            {
                r.velocity = Vector3.zero;
                r.angularVelocity = Vector3.zero;
                vertical = true;
                reset = true;
            }
        }
        else if (!accelerate)
        {
            videoPlayer.clip = accelerateClip;
            if (t.position.z > startPosition.z + metersForCompletion || t.position.z < startPosition.z - metersForCompletion)
            {
                r.velocity = Vector3.zero;
                r.angularVelocity = Vector3.zero;
                accelerate = true;
                reset = true;
            }
        }
        else if (!firstPlay)
        {
            billboard.state = 1;
            billboardTrailLeft.Play();
            billboardTrailRight.Play();
            firstPlay = true;
        }
    }    
}
