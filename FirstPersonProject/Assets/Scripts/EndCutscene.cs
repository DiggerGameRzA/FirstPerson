using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class EndCutscene : MonoBehaviour
{
    public Button btt;
    double time;
    double currentTime;
    void Start()
    {
        time = GetComponent<VideoPlayer>().clip.length;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = Time.time;
        if(currentTime >= time)
        {
            print("Cutscene is end.");
            btt.LoadLevel01();
        }
    }
}
