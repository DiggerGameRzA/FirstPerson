using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPteraBG : MonoBehaviour
{
    public Transform[] pos;
    public GameObject ptera;
    public float waitTime;
    float tempTime = 0;
    void Update()
    {
        tempTime -= Time.deltaTime;
        if (tempTime <= 0)
        {
            tempTime = waitTime;
            for (int i = 0; i < pos.Length; i++)
            {
                Instantiate(ptera, pos[i]);
            }
        }
    }
}
