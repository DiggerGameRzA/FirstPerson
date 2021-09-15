using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterVolume : MonoBehaviour
{
    public Slider masterSlider;
    public float masterValue;
    void Start()
    {
        masterSlider.value = SoundManager.instance.masterVolume;
    }
    void Update()
    {
        masterValue = Mathf.Round(masterSlider.value * 100f);
        GetComponent<Text>().text = masterValue.ToString();

        SoundManager.instance.masterVolume = masterSlider.value;
    }
}
