using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    public GameObject[] wave01Dinos;
    public GameObject[] wave02Dinos;
    public GameObject[] wave03Dinos;
    public GameObject[] wave04Dinos;

    public void SpawnWave01()
    {
        for (int i = 0; i < wave01Dinos.Length; i++)
        {
            wave01Dinos[i].SetActive(true);
        }
    }
    public void SpawnWave02()
    {
        for (int i = 0; i < wave02Dinos.Length; i++)
        {
            wave02Dinos[i].SetActive(true);
        }
    }
    public void SpawnWave03()
    {
        for (int i = 0; i < wave03Dinos.Length; i++)
        {
            wave03Dinos[i].SetActive(true);
        }
    }
    public void SpawnWave04()
    {
        for (int i = 0; i < wave04Dinos.Length; i++)
        {
            wave04Dinos[i].SetActive(true);
        }
    }
}
