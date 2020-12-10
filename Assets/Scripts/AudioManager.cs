using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource m_MyAudioSource;

    void Start()
    {
        m_MyAudioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        JoystickController.FireThruster += Playing;
        JoystickController.StopThruster += StopPlaying;
    }

    void OnDisable()
    {
        JoystickController.FireThruster -= Playing;
        JoystickController.StopThruster -= StopPlaying;
    }

    void Playing()
    {
        Debug.Log("Start Playing!");
        if (!m_MyAudioSource.isPlaying)
            m_MyAudioSource.Play();
    }

    void StopPlaying()
    {
        Debug.Log("Stop Playing!");
        if (m_MyAudioSource.isPlaying)
            m_MyAudioSource.Stop();
    }
}