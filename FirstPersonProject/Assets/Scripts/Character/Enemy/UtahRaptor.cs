﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UtahRaptor : EnemyStats
{
    Transform target;
    NavMeshAgent agent;
    Animator anim;
    IHealth health;
    ISedat sedat;
    AudioSource audioSource;

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

        if (isHit)
        {
            visionRange = visionRange2;
        }

        float distance = Vector3.Distance(target.position, transform.position);
        if (health.HealthPoint <= 0 && !isDied)
        {
            health.OnDead();
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
            isInRange = true;
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
            if (!playedDead)
            {
                PlayDeadSound(audioSource);
                playedDead = true;
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
            if(tempSleepTime <= 0)
            {
                PlaySleepSound(audioSource);
                tempSleepTime = 6f;
            }
        }
        else if (isInAtk)
        {
            agent.SetDestination(transform.position);
            anim.SetBool("isRunning", false);
            anim.SetBool("isIdling", false);

            anim.SetBool("isAttacking", true);
            if (tempAttackTime <= 0)
            {
                PlayAttackSound(audioSource);
                tempAttackTime = attackDelay;
                Invoke("Attack", 1f);
            }
            else
            {
                anim.SetBool("isAttacking", false);
            }
        }
        else if (isInRange)
        {
            agent.SetDestination(target.position);
            anim.SetBool("isIdling", false);
            anim.SetBool("isAttacking", false);

            anim.SetBool("isRunning", true);
            if(tempRunTime <= 0)
            {
                PlayRunSound(audioSource);
                tempRunTime = 0.2f;
            }
        }
        else if (!isInAtk && !isInRange)
        {
            agent.SetDestination(transform.position);
            anim.SetBool("isRunning", false);
            anim.SetBool("isAttacking", false);

            anim.SetBool("isIdling", true);
            if(tempIdleTime <= 0)
            {
                PlayIdleSound(audioSource);
                tempIdleTime = 5f;
            }
        }
        else
        {
            isInRange = false;
        }

        if (health.HealthPoint <= 0 || sedat.SedatPoints <= 0)
        {
            GetComponent<GatherSyringe>().ShowUI(textPrefab);
        }
    }
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    void Attack()
    {
        anim.Play("Attack");
        target.GetComponent<IHealth>().TakeDamage(damage);
    }
    private void OnDestroy()
    {
        SaveInfo();
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, visionRange);
        Gizmos.DrawWireSphere(transform.position, attackRange);
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
