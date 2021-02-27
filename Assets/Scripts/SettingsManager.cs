using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    static GameObject indestructableData;

    public bool enableVibration = true;
    public string controllerType = "XBOX";

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    private void Start()
    {
        if (indestructableData == null)
        {
            indestructableData = gameObject;            
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
