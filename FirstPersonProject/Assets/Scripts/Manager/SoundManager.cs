using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public float masterVolume = 1f;
    public float effectVolume = 1f;
    public float musicVolume = 1f;

    public float effectVolumeUI = 1f;
    public float musicVolumeUI = 1f;

    [Header("BGM")]
    public AudioClip bgm;
    public AudioClip swampBGM;
    public AudioClip forestBGM;
    public AudioClip bossBGM;
    public AudioClip inFight;

    [Header("Guns")]
    public AudioClip pistolFire;
    public AudioClip pistolReload;
    public AudioClip pistolNoAmmo;

    public AudioClip arFire;
    public AudioClip dartFire;

    public AudioClip shotgunFire;
    public AudioClip shotgunReload;

    public AudioClip bazukaFire;
    public AudioClip bazukaExplode;

    public AudioClip doorOpen;

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
    public void Restart()
    {
        
    }
    void Update()
    {
        AudioSource Button = FindObjectOfType<Button>().GetComponent<AudioSource>();
        Button.volume = masterVolume * effectVolume;

        if (GameObject.Find("Gun sfx") != null)
        {
            AudioSource Gun = GameObject.Find("Gun sfx").GetComponent<AudioSource>();
            Gun.volume = masterVolume * effectVolume;
        }
        AudioSource UI = GameObject.Find("Button Function").GetComponent<AudioSource>();
        UI.volume = masterVolume * effectVolume;

        if (GameObject.Find("BGM sfx") != null)
        {
            AudioSource BGM = GameObject.Find("BGM sfx").GetComponent<AudioSource>();
            BGM.volume = masterVolume * musicVolume;
        }
    }
    #region Music
    public void PlayBGM()
    {
        audioSource = GameObject.Find("BGM sfx").GetComponent<AudioSource>();
        audioSource.Stop();
        audioSource.PlayOneShot(bgm);
    }
    public void PlaySwamp()
    {
        audioSource = GameObject.Find("BGM sfx").GetComponent<AudioSource>();
        audioSource.Stop();
        audioSource.PlayOneShot(swampBGM);
    }
    public void PlayForest()
    {
        audioSource = GameObject.Find("BGM sfx").GetComponent<AudioSource>();
        audioSource.Stop();
        audioSource.PlayOneShot(forestBGM);
    }
    public void PlayBossBGM()
    {
        audioSource = GameObject.Find("BGM sfx").GetComponent<AudioSource>();
        audioSource.Stop();
        audioSource.PlayOneShot(bossBGM);
    }
    public void PlayInFight()
    {
        audioSource = GameObject.Find("BGM sfx").GetComponent<AudioSource>();
        audioSource.PlayOneShot(inFight);
    }
    public void StopPlayInFight()
    {
        audioSource = GameObject.Find("BGM sfx").GetComponent<AudioSource>();
        audioSource.Stop();
        audioSource.PlayOneShot(bgm);
    }
    #endregion
    #region Guns
    public void PlayPistolFire()
    {
        audioSource = GameObject.Find("Gun sfx").GetComponent<AudioSource>();
        audioSource.PlayOneShot(pistolFire);
    }
    public void PlayPistolReload()
    {
        audioSource = GameObject.Find("Gun sfx").GetComponent<AudioSource>();
        audioSource.PlayOneShot(pistolReload);
    }
    public void PlayPistolNoAmmo()
    {
        audioSource = GameObject.Find("Gun sfx").GetComponent<AudioSource>();
        audioSource.PlayOneShot(pistolNoAmmo);
    }

    public void PlayARFire()
    {
        audioSource = GameObject.Find("Gun sfx").GetComponent<AudioSource>();
        audioSource.PlayOneShot(arFire);
    }
    public void PlayDartFire()
    {
        audioSource = GameObject.Find("Gun sfx").GetComponent<AudioSource>();
        audioSource.PlayOneShot(dartFire);
    }

    public void PlayBazukaFire()
    {
        audioSource = GameObject.Find("Gun sfx").GetComponent<AudioSource>();
        audioSource.PlayOneShot(bazukaFire);
    }
    public void PlayBazukaExplode()
    {
        audioSource = GameObject.Find("Gun sfx").GetComponent<AudioSource>();
        audioSource.PlayOneShot(bazukaExplode);
    }
    public void PlayShotgunFire()
    {
        audioSource = GameObject.Find("Gun sfx").GetComponent<AudioSource>();
        audioSource.PlayOneShot(shotgunFire);
    }
    public void PlayShotgunReload()
    {
        audioSource = GameObject.Find("Gun sfx").GetComponent<AudioSource>();
        audioSource.PlayOneShot(shotgunReload);
    }
    #endregion
    public void PlayDoorOpen(AudioSource audioSource)
    {
        audioSource.volume = SoundManager.instance.masterVolume * SoundManager.instance.effectVolume;

        audioSource.PlayOneShot(doorOpen);
    }
}
