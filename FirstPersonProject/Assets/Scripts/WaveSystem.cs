using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    bool startFirst = false;
    bool startWave = false;
    public float firstWaveDelay = 2f;
    public float nextWaveDelay = 5f;
    public int enemyLeft = 0;
    public float tempTime = 0;

    public GameObject[] Wave01Dinos;
    public GameObject[] Wave02Dinos;
    public GameObject[] Wave03Dinos;
    void Start()
    {

    }
    void Update()
    {
        tempTime -= Time.deltaTime;

        if (startFirst)
        {
            if (startWave)
            {
                if (tempTime <= 0)
                {
                    for (int i = 0; i < Wave01Dinos.Length; i++)
                    {
                        print("spawn " + i);
                        Wave01Dinos[i].SetActive(true);
                        enemyLeft++;
                    }
                    startWave = false;
                } 
            }
        }
    }
    public void StartFirstWave()
    {
        startFirst = true;
        startWave = true;
        tempTime = firstWaveDelay;
    }
    void NextWave()
    {

    }
    public void DecreaseEnemy()
    {
        enemyLeft--;
    }
}
