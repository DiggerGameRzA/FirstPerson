﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pentaceratops : EnemyStats
{
    Transform target;
    NavMeshAgent agent;
    Animator anim;
    IHealth health;
    ISedat sedat;
    AudioSource audioSource;

    [SerializeField] bool playBGM = true;

    [SerializeField] Collider col;

    [SerializeField] bool isStun = false;
    float tempStunTime = 0f;

    [SerializeField] float knockbackHeight;
    [SerializeField] float knockbackStrength;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        anim = GetComponent<Animator>();
        target = FindObjectOfType<Player>().transform;
        health = GetComponent<IHealth>();
        sedat = GetComponent<SedatPoint>();
        audioSource = GetComponent<AudioSource>();

        GetInfo();
        if (health.HealthPoint <= 0)
        {
            isDied = true;
            health.OnDead();

            anim.Play("Died");
            anim.SetBool("isIdling", true);
            anim.SetBool("isRunning", false);
            anim.SetBool("isDead", false);
            anim.Play("Died");
        }
        else if (sedat.SedatPoints <= 0)
        {
            isDied = true;

            anim.Play("Died");
            anim.SetBool("isIdling", true);
            anim.SetBool("isRunning", false);
            anim.SetBool("isDead", false);
            anim.Play("Died");
        }
    }

    void Update()
    {
        tempIdleTime -= Time.deltaTime;
        tempRunTime -= Time.deltaTime;
        tempAttackTime -= Time.deltaTime;
        tempHitTime -= Time.deltaTime;
        tempSleepTime -= Time.deltaTime;

        tempStunTime -= Time.deltaTime;

        if (isHit)
        {
            visionRange = visionRange2;
        }

        float distance = Vector3.Distance(target.position, transform.position);
        if (health.HealthPoint <= 0 && !isDied)
        {
            isDead = true;
        }
        else if (sedat.SedatPoints <= 0 && !isDied)
        {
            isSleep = true;
        }
        else if (distance < attackRange)
        {
            isInAtk = true;
        }
        else if (distance < visionRange)
        {
            visionRange = visionRange2;
            isInRange = true;
        }

        if (isDead)
        {
            col.enabled = false;
        }
        else if (isSleep)
        {
            col.enabled = false;
        }
        else if (tempStunTime <= 0)
        {
            isStun = false;
            col.enabled = true;
        }
        else
        {
            isStun = true;
            col.enabled = false;
        }

        if (distance > visionRange)
        {
            isInRange = false;
        }
        else if (distance > attackRange)
        {
            isInAtk = false;
        }

        if (isDied)
        {
            agent.SetDestination(transform.position);
            anim.SetBool("isAttacking", false);
            anim.SetBool("isRunning", false);

            anim.SetBool("isIdling", true);
            anim.SetBool("isDead", true);

            anim.Play("Died");
        }
        else if (isDead)
        {
            agent.SetDestination(transform.position);
            anim.SetBool("isAttacking", false);
            anim.SetBool("isRunning", false);

            anim.SetBool("isIdling", true);
            anim.SetBool("isDead", true);
            anim.Play("Death");
            if (!isPlayedDead)
            {
                PlayDeadSound(audioSource);
                isPlayedDead = true;
            }
        }
        else if (isSleep)
        {
            agent.SetDestination(transform.position);
            anim.SetBool("isAttacking", false);
            anim.SetBool("isRunning", false);

            anim.SetBool("isIdling", true);
            anim.SetBool("isDead", true);
            anim.Play("Death");
            if (tempSleepTime <= 0)
            {
                PlaySleepSound(audioSource);
                tempSleepTime = 6f;
            }
        }
        else if (isStun)
        {
            agent.SetDestination(transform.position);
            anim.SetBool("isRunning", false);
            anim.SetBool("isIdling", true);
        }
        /*
        else if (isInAtk)
        {
            agent.SetDestination(transform.position);
            anim.SetBool("isRunning", false);
            anim.SetBool("isIdling", true);
            if (tempAttackTime <= 0)
            {
                Attack();
                isDealDamage = true;
                tempAttackTime = attackDelay;
            }
        }
        */
        else if (isInRange)
        {
            agent.SetDestination(target.position);
            anim.SetBool("isIdling", false);
            anim.SetBool("isAttacking", false);

            anim.SetBool("isRunning", true);
            if (tempRunTime <= 0)
            {
                PlayRunSound(audioSource);
                tempRunTime = 0.8f;
            }
        }
        else if (!isInAtk && !isInRange)
        {
            agent.SetDestination(transform.position);
            anim.SetBool("isRunning", false);
            anim.SetBool("isAttacking", false);

            anim.SetBool("isIdling", true);
            if (tempIdleTime <= 0)
            {
                PlayIdleSound(audioSource);
                tempIdleTime = 5f;
            }
        }
        else
        {
            isInRange = false;
        }

        if (!isInRange || isDead || isSleep)
        {
            if (!isStopPlayFight)
            {
                SoundManager.instance.StopPlayInFight();
                isStopPlayFight = true;
                isInFight = false;
            }
        }
        else if (isInRange && playBGM)
        {
            if (!isInFight)
            {
                SoundManager.instance.PlayInFight();
                isInFight = true;
                isStopPlayFight = false;
            }
        }

        if (isDeadDrop && !isDroped)
        {
            PlayDeadDropSound(audioSource);
            isDroped = true;
        }
    }
    void Attack()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        PlayAttackSound(audioSource);
        tempStunTime = 5f;
        if (distance < attackRange)
        {
            //target.GetComponent<IHealth>().TakeDamage(damage);
        }
    }
    private void OnDestroy()
    {
        SaveInfo();
    }
    /*
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, visionRange);
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    */
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            tempStunTime = 5f;
            col.enabled = false;

            Rigidbody rb = other.GetComponent<Rigidbody>();
            CharacterController cc = other.GetComponent<CharacterController>();
            cc.enabled = false;
            Invoke("EnablePlayerController", .5f);

            other.GetComponent<IHealth>().TakeDamage(damage);

            Vector3 dir = other.transform.position - transform.position;
            dir.y = knockbackHeight;

            rb.AddForce(dir * knockbackStrength);
        }
    }
    void EnablePlayerController()
    {
        FindObjectOfType<Player>().GetComponent<CharacterController>().enabled = true;
    }
    void GetInfo()
    {
        for (int i = 0; i < SaveManager.instance.dinos.Count; i++)
        {
            DinoInfo dinoInfo = SaveManager.instance.dinos[i];
            if (id == i)
            {
                health.HealthPoint = dinoInfo.healthPoint;
                sedat.SedatPoints = dinoInfo.sedatPoint;
                transform.position = dinoInfo.position;
                health.UpdateHealth(health.HealthPoint);
                break;
            }
        }
    }
    void SaveInfo()
    {
        for (int i = 0; i < SaveManager.instance.dinos.Count; i++)
        {
            DinoInfo dinoInfo = SaveManager.instance.dinos[i];
            if (id == i)
            {
                dinoInfo.healthPoint = health.HealthPoint;
                dinoInfo.sedatPoint = sedat.SedatPoints;
                dinoInfo.position = transform.position;
                break;
            }
        }
    }
}
