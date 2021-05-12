using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("Guns")]
    public AudioClip pistolFire;
    public AudioClip pistolReload;
    public AudioClip pistolNoAmmo;

    AudioSource audioSource;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        //DontDestroyOnLoad(gameObject);
    }
    public void PlayPistolFire()
    {
        audioSource = FindObjectOfType<Player>().GetComponent<AudioSource>();
        audioSource.PlayOneShot(pistolFire);
    }
    public void PlayPistolReload()
    {
        audioSource = FindObjectOfType<Player>().GetComponent<AudioSource>();
        audioSource.PlayOneShot(pistolReload);
    }
    public void PlayPistolNoAmmo()
    {
        audioSource = FindObjectOfType<Player>().GetComponent<AudioSource>();
        audioSource.PlayOneShot(pistolNoAmmo);
    }
}
