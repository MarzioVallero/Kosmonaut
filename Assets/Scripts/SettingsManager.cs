using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public bool enableVibration = true;
    public string controllerType = "XBOX";

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
