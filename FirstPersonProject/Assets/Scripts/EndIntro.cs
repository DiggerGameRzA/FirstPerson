using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class EndIntro : MonoBehaviour
{
    [SerializeField] double time;
    [SerializeField] double currentTime;

    bool isEnded = false;
    void Start()
    {
        time = GetComponent<VideoPlayer>().clip.length;
        GetComponent<VideoPlayer>().Play();
    }
    void Update()
    {
        if (!isEnded)
        {
            currentTime = GetComponent<VideoPlayer>().time;
            if (currentTime >= time)
            {
                print("Cutscene is end.");
                GameManager.instance.LoadMainMenu();

                isEnded = true;
            }
        }
    }
}
