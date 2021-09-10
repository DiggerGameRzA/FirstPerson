using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int id = 0;
    //HUD
    public GameObject textPrefab;

    //Vision
    public float visionRange;
    public float visionRange2;

    //Attack
    public float attackRange;
    public float attackDelay;
    public float damage;

    public float speed;

    //States
    public bool firstTimeSeen = true;

    public bool isDead = false;
    public bool isDied = false;
    public bool isPlayedDead = false;
    public bool isDeadDrop = false;
    public bool isDroped = false;
    public bool isSleep = false;

    public bool isHit = false;
    public bool isDealDamage = false;

    public bool isRoaring = false;
    public bool isRoared = false;
    public bool isInFight = false;
    public bool isStopPlayFight = false;

    public bool isInRange = false;
    public bool isInAtk = false;
    public bool isAttacking = false;

    public bool isEscaped = false;
    public bool isNest = false;

    //Sounds
    public AudioClip[] idleSound;
    public AudioClip[] runSound;
    public AudioClip[] attackSound;
    public AudioClip[] hitSound;
    public AudioClip[] sleepSound;
    public AudioClip[] deadSound;
    public AudioClip deadDrop;
    public AudioClip roarSound;

    //Temporary Time
    [HideInInspector]
    public float tempIdleTime, tempRunTime, tempAttackTime, tempHitTime, tempSleepTime = 0f;
    public void PlayIdleSound(AudioSource audioSource)
    {
        audioSource.volume = SoundManager.instance.masterVolume * SoundManager.instance.effectVolume;

        if (idleSound.Length > 0)
        {
            int index = Random.Range(0, idleSound.Length);
            audioSource.PlayOneShot(idleSound[index]);
        }
    }
    public void PlayRunSound(AudioSource audioSource)
    {
        audioSource.volume = SoundManager.instance.masterVolume * SoundManager.instance.effectVolume;

        int index = Random.Range(0, runSound.Length);
        audioSource.PlayOneShot(runSound[index]);
    }
    public void PlayAttackSound(AudioSource audioSource)
    {
        audioSource.volume = SoundManager.instance.masterVolume * SoundManager.instance.effectVolume;

        int index = Random.Range(0, attackSound.Length);
        audioSource.PlayOneShot(attackSound[index]);
    }
    public void PlayHitSound(AudioSource audioSource)
    {
        audioSource.volume = SoundManager.instance.masterVolume * SoundManager.instance.effectVolume;

        int index = Random.Range(0, hitSound.Length);
        audioSource.PlayOneShot(hitSound[index]);
    }
    public void PlaySleepSound(AudioSource audioSource)
    {
        audioSource.volume = SoundManager.instance.masterVolume * SoundManager.instance.effectVolume;

        int index = Random.Range(0, sleepSound.Length);
        audioSource.PlayOneShot(sleepSound[index]);
    }
    public void PlayDeadSound(AudioSource audioSource)
    {
        audioSource.volume = SoundManager.instance.masterVolume * SoundManager.instance.effectVolume;

        int index = Random.Range(0, deadSound.Length);
        audioSource.PlayOneShot(deadSound[index]);
    }
    public void PlayDeadDropSound(AudioSource audioSource)
    {
        audioSource.volume = SoundManager.instance.masterVolume * SoundManager.instance.effectVolume;

        audioSource.PlayOneShot(deadDrop);
    }
    public void PlayRoarSound(AudioSource audioSource)
    {
        audioSource.volume = SoundManager.instance.masterVolume * SoundManager.instance.effectVolume;

        audioSource.PlayOneShot(roarSound);
    }
}
