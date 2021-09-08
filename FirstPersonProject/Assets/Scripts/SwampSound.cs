using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwampSound : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            FindObjectOfType<SoundManager>().PlaySwamp();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<SoundManager>().PlayBGM();
        }
    }
}
