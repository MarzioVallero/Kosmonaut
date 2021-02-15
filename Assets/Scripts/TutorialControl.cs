using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine;

public class TutorialControl : MonoBehaviour
{
    public bool reset = false;
    public bool roll = false;
    public bool pitch = false;
    public bool yaw = false;
    public bool horizontal = false;
    public bool vertical = false;
    public bool accelerate = false;
    public bool firstPlay = false;
    private Transform t;
    private Rigidbody r;
    private Vector3 startPosition;
    private MoveBillboard billboard;
    private VideoPlayer videoPlayer;
    public ParticleSystem billboardTrailLeft;
    public ParticleSystem billboardTrailRight;
    [SerializeField] private int angleForCompletion = 45;
    [SerializeField] private float metersForCompletion = 1;
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
    }

    // Update is called once per frame
    void Update()
    {
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
