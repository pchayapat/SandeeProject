using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioMixer audioMixer;

    public void SetMaster_Volume(float master_volume)
    {
        audioMixer.SetFloat("masterVolume", master_volume);
        
    }
    public void SetMusic_Volume(float music_volume)
    { audioMixer.SetFloat("musicVolume", music_volume);
    }


    public void SetGraphicsQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }


    }
