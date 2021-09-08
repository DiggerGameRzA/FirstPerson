using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectVolume : MonoBehaviour
{
    public Slider effectSlider;
    public float effectValue;
    void Start()
    {
        effectSlider.value = SoundManager.instance.effectVolumeUI;
    }
    void Update()
    {
        effectValue = Mathf.Round(effectSlider.value * 100f);
        GetComponent<Text>().text = effectValue.ToString();

        SoundManager.instance.effectVolume = effectSlider.value * SoundManager.instance.masterVolume;
        SoundManager.instance.effectVolumeUI = effectSlider.value;
    }
}
