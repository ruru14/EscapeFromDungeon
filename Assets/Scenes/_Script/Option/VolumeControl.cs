using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public Slider effectVolumeSlider;
    public Slider backgroundVolumeSlider;
    public Text txtEffectVolume;
    public Text txtBgmVolume;
    public AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        effectVolumeSlider.value = (float)PlayerPrefs.GetInt(PrefsEntity.EffectVolume) / 100;
        backgroundVolumeSlider.value = (float)PlayerPrefs.GetInt(PrefsEntity.BackgroundVolume) / 100;
    }

    // Update is called once per frame
    void Update()
    {
        txtEffectVolume.text = ((int)(effectVolumeSlider.value * 100)).ToString();
        PlayerPrefs.SetInt(PrefsEntity.EffectVolume, (int)effectVolumeSlider.value * 100);
        txtBgmVolume.text = ((int)(backgroundVolumeSlider.value * 100)).ToString();
        PlayerPrefs.SetInt(PrefsEntity.BackgroundVolume, (int)backgroundVolumeSlider.value * 100);
    }
}
