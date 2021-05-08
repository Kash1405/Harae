// Adapted from a Brackey's Tutorial on YouTube

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsScript : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetQualiy(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
