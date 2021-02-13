using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public bool enableVibration = true;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
