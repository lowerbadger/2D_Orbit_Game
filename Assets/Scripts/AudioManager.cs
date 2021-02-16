using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider slider;
    //public string parameterName = "MasterVolume";
    // Start is called before the first frame update
    void Start()
    {
        //mixer.SetFloat("MasterVolume", 0f);
        slider.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
    }

    public void SetLevel (float sliderValue)
    {
        //float sliderValue = slider.value;
        mixer.SetFloat("MasterVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MasterVolume", sliderValue);
    }
}
