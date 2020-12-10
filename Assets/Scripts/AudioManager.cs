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

    void Playing(string name)
    {
        if (!m_MyAudioSource.isPlaying && this.gameObject.name.ToString() == name)
        {
            m_MyAudioSource.Play();
        }

    }

    void StopPlaying(string name)
    {
        if (m_MyAudioSource.isPlaying && this.gameObject.name.ToString() == name)
        {
            m_MyAudioSource.Stop();
        }
    }
}