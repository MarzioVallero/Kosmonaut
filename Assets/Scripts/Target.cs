using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour
{
    public Material activeMaterial;
    public float activeTime = 0.5f;
    public bool isScreen = false;
    public Camera FpsCam;
    public Camera ScreenCam;
    public Camera FplCam;

    private Renderer _renderer;
    private Material originalMaterial;
    private WaitForSeconds pressDuration;
    private Outline outline;
    private bool fpsCam = true;
    private FirstPersonLook fpl;
    private bool disableOutline = false;

    void Start()
    {
        _renderer = GetComponent<Renderer>();
        originalMaterial = _renderer.material;
        pressDuration = new WaitForSeconds(activeTime);
        outline = gameObject.AddComponent<Outline>();

        outline.OutlineMode = Outline.Mode.OutlineVisible;
        outline.OutlineColor = Color.green;
        outline.OutlineWidth = 10f;

        outline.enabled = false;

        if (_renderer == null)
        {
            Debug.LogWarning($"There is no Renderer attached to GameObject named:{gameObject.name}");
        }
        if (isScreen)
        {
            FpsCam.enabled = fpsCam;
            ScreenCam.enabled = !fpsCam;
            fpl = FplCam.GetComponent<FirstPersonLook>();
        }
    }

    public void Activate()
    {
        if (_renderer == null)
            return;
        if (isScreen) SwitchCamera();
        else StartCoroutine(TimedAction());
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
        fpsCam = !fpsCam;
        FpsCam.enabled = fpsCam;
        ScreenCam.enabled = !fpsCam;
        fpl.isActive = !fpl.isActive;
        disableOutline = !disableOutline;
        outline.enabled = !outline.enabled;
    }

    private IEnumerator TimedAction()
    {
        _renderer.material = activeMaterial;
        yield return pressDuration;
        _renderer.material = originalMaterial;
    }
}
