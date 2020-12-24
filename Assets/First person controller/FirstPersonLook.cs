using UnityEngine;

public class FirstPersonLook : MonoBehaviour
{
    [SerializeField]
    public Transform character;
    Vector2 currentMouseLook;
    Vector2 appliedMouseDelta;
    public float sensitivity = 1;
    public float smoothing = 2;
    public bool isActive = true;

    [SerializeField] private Camera ScreenCam;
    private float xRot;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        xRot = character.rotation.eulerAngles.x;
    }

    void Update()
    {
        if (!isActive) return;
        // Get smooth mouse look.
        Vector2 smoothMouseDelta = Vector2.Scale(new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")), Vector2.one * sensitivity * smoothing);
        appliedMouseDelta = Vector2.Lerp(appliedMouseDelta, smoothMouseDelta, 1 / smoothing);
        currentMouseLook += appliedMouseDelta;
        currentMouseLook.y = Mathf.Clamp(currentMouseLook.y, -90, 90);

        // Rotate camera and controller.
        //character.localRotation = Quaternion.AngleAxis(currentMouseLook.x, character.up);
        //transform.localRotation = Quaternion.AngleAxis(-currentMouseLook.y, Vector3.right);
        //character.Rotate(new Vector3(xRot, 0, 0));
        transform.localRotation = Quaternion.AngleAxis(currentMouseLook.x, Vector3.up) * Quaternion.AngleAxis(-currentMouseLook.y, Vector3.right);
    }

    public void Toggle(bool pausing)
    {
        if (pausing)
        {
            isActive = false;
            Cursor.lockState = CursorLockMode.None;
        }
        else if(ScreenCam.enabled)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            isActive = true;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
