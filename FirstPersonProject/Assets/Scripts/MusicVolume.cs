using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicVolume : MonoBehaviour
{
    public Slider musicSlider;
    public float musicValue;
    void Start()
    {
        musicSlider.value = SoundManager.instance.musicVolumeUI;
    }
    void Update()
    {
        musicValue = Mathf.Round(musicSlider.value * 100f);
        GetComponent<Text>().text = musicValue.ToString();

        SoundManager.instance.musicVolume = musicSlider.value * SoundManager.instance.masterVolume;
        SoundManager.instance.musicVolumeUI = musicSlider.value;
    }
}
