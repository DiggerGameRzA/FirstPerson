using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Compy : EnemyStats
{
    //gameObject
    public GameObject nest;
    public GameObject escape;

    //Component
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
        }
        else if(sedat.SedatPoints <= 0)
        {
            isDied = true;

            anim.Play("Died");
            anim.SetBool("isIdling", true);
            anim.SetBool("isRunning", false);
            anim.SetBool("isDead", false);
        }
    }

    void Update()
    {
        tempIdleTime -= Time.deltaTime;
        tempRunTime -= Time.deltaTime;

        float distance = Vector3.Distance(target.position, transform.position);
        float disToNest = Vector3.Distance(transform.position, nest.transform.position);
        float disToEsc = Vector3.Distance(transform.position, escape.transform.position);

        if (isHit)
        {
            visionRange = visionRange2;
        }

        if ((health.HealthPoint <= 0 || sedat.SedatPoints <= 0) && !GetComponent<GatherSyringe>().gathered)
        {
            GetComponent<GatherSyringe>().ShowUI(textPrefab);
        }

        if (disToEsc < 1)
        {
            isEscaped = true;
        }
        
        if (health.HealthPoint <= 0 && !isDied)
        {
            health.OnDead();
            isDead = true;
        }
        else if (sedat.SedatPoints <= 0 && !isDied)
        {
            isSleep = true;
        }
        else if (disToNest < 2)
        {
            isNest = true;
        }
        
        if (distance < visionRange)
        {
            isInRange = true;
        }
        else if (distance > visionRange)
        {
            isInRange = false;
        }

        if (isEscaped)
        {
            gameObject.SetActive(false);
        }

        if (isDied)
        {
            agent.SetDestination(transform.position);
            agent.speed = 0;
        }
        else if (isDead)
        {
            agent.SetDestination(transform.position);
            agent.speed = 0;
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
            agent.speed = 0;
            anim.SetBool("isDead", true);
            anim.Play("Death");
            if (!isPlayedDead)
            {
                PlayDeadSound(audioSource);
                isPlayedDead = true;
            }
        }
        else if (isNest && isInRange)
        {
            agent.SetDestination(escape.transform.position);
            anim.SetBool("isIdling", false);
            anim.SetBool("isRunning", true);

            if (tempRunTime <= 0)
            {
                PlayRunSound(audioSource);
                tempRunTime = 1f;
            }
        }
        else if (isNest)
        {
            agent.SetDestination(transform.position);
            anim.SetBool("isIdling", true);
            anim.SetBool("isRunning", false);
        }
        else if (isInRange)
        {
            agent.SetDestination(nest.transform.position);
            anim.SetBool("isIdling", false);
            anim.SetBool("isRunning", true);

            if(tempRunTime <= 0)
            {
                PlayRunSound(audioSource);
                tempRunTime = 1f;
            }
        }
        else if (!isInRange)
        {
            agent.SetDestination(transform.position);
            anim.SetBool("isIdling", true);
            anim.SetBool("isRunning", false);
            if (tempIdleTime <= 0)
            {
                PlayIdleSound(audioSource);
                tempIdleTime = 5f;
            }
        }

        if (health.HealthPoint <= 0 || sedat.SedatPoints <= 0)
        {
            GetComponent<GatherSyringe>().ShowUI(textPrefab);
        }
    }
    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }
    */
    private void OnDestroy()
    {
        SaveInfo();
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
                isEscaped = dinoInfo.escaped;
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
                dinoInfo.escaped = isEscaped;
                break;
            }
        }
    }
}
