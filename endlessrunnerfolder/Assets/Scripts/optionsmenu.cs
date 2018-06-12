using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class optionsmenu : MonoBehaviour {
    public AudioMixer mixer;

    public void VolumeSlider(float volume)
    {
        mixer.SetFloat("volume", volume);
    }

    public void SetQuality(int qualityindex)
    {
        QualitySettings.SetQualityLevel(qualityindex);
    }
}
