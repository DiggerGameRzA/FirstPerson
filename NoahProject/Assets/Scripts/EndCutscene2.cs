﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class EndCutscene2 : MonoBehaviour
{
    public Button btt;
    [SerializeField] double time;
    [SerializeField] double currentTime;

    bool isStart = false;
    bool isEnded = false;
    void Start()
    {
        time = Mathf.Round((float)GetComponent<VideoPlayer>().clip.length);
        Invoke("StartVideo", 0.5f);
    }
    void Update()
    {
        if (isStart)
        {
            if (!isEnded)
            {
                currentTime = Mathf.Round((float)GetComponent<VideoPlayer>().time);
                if (currentTime >= time)
                {
                    print("Cutscene is end.");
                    btt.LoadTitle();

                    isEnded = true;
                }
            }
        }
    }
    public void StartVideo()
    {
        print("Start a cutscene");
        GetComponent<VideoPlayer>().Play();
        isStart = true;
    }
}
