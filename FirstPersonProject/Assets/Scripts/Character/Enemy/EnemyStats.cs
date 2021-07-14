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
    public bool isDead = false;
    public bool isSleep = false;
    public bool isHit = false;
    public bool isInRange = false;
    public bool isInAtk = false;
    public bool isAttacking = false;
    public bool isDealDamage = false;
    public bool isDied = false;

    public bool isEscaped = false;
    public bool isNest = false;

    //Sounds
    public AudioClip[] idleSound;
    public AudioClip[] runSound;
    public AudioClip[] attackSound;
    public AudioClip[] hitSound;
    public AudioClip[] sleepSound;
    public AudioClip[] deadSound;

    //Temporary Time
    [HideInInspector]
    public float tempIdleTime, tempRunTime, tempAttackTime, tempHitTime, tempSleepTime = 0f;
    public bool playedDead = false;
    public void PlayIdleSound(AudioSource audioSource)
    {
        int index = Random.Range(0, idleSound.Length);
        audioSource.PlayOneShot(idleSound[index]);
    }
    public void PlayRunSound(AudioSource audioSource)
    {
        int index = Random.Range(0, runSound.Length);
        audioSource.PlayOneShot(runSound[index]);
    }
    public void PlayAttackSound(AudioSource audioSource)
    {
        int index = Random.Range(0, attackSound.Length);
        audioSource.PlayOneShot(attackSound[index]);
    }
    public void PlayHitSound(AudioSource audioSource)
    {
        int index = Random.Range(0, hitSound.Length);
        audioSource.PlayOneShot(hitSound[index]);
    }
    public void PlaySleepSound(AudioSource audioSource)
    {
        int index = Random.Range(0, sleepSound.Length);
        audioSource.PlayOneShot(sleepSound[index]);
    }
    public void PlayDeadSound(AudioSource audioSource)
    {
        int index = Random.Range(0, deadSound.Length);
        audioSource.PlayOneShot(deadSound[index]);
    }
}
