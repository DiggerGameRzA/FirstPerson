using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tyranosaurus : EnemyStats
{
    Transform target;
    NavMeshAgent agent;
    Animator anim;
    IHealth health;
    ISedat sedat;
    AudioSource audioSource;

    WaveSystem ws;

    [SerializeField] bool isRoared02 = false;
    [SerializeField] bool isRoared03 = false;
    [SerializeField] bool isRoared04 = false;

    [SerializeField] bool firstTimeRoar02 = true;
    [SerializeField] bool firstTimeRoar03 = true;
    [SerializeField] bool firstTimeRoar04 = true;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        anim = GetComponent<Animator>();
        target = FindObjectOfType<Player>().transform;
        health = GetComponent<IHealth>();
        sedat = GetComponent<SedatPoint>();
        audioSource = GetComponent<AudioSource>();

        ws = FindObjectOfType<WaveSystem>();

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
        if ((health.HealthPoint <= 0 || sedat.SedatPoints <= 0) && !GetComponent<GatherSyringe>().gathered)
        {
            health.OnDead();
            isDead = true;
        }
        else if (sedat.SedatPoints <= 0 && !isDied)
        {
            isSleep = true;
        }
        else if (HitPlayer())
        {
            isInAtk = true;
        }
        else if (distance < visionRange)
        {
            visionRange = visionRange2;
            isInRange = true;
        }

        if (distance > visionRange)
        {
            isInRange = false;
        }
        else if (!HitPlayer())
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
        }
        else if (isSleep)
        {
            agent.SetDestination(transform.position);
            anim.SetBool("isAttacking", false);
            anim.SetBool("isRunning", false);

            anim.SetBool("isIdling", true);
            anim.SetBool("isDead", true);
            anim.Play("Death");
        }

        // roar 4
        else if (health.HealthPoint <= 0.25f * health.MaxHealthPoint && firstTimeRoar04)
        {
            print("lower than 25%");
            ws.SpawnWave04();

            agent.SetDestination(transform.position);
            anim.SetBool("isRunning", false);
            anim.SetBool("isIdling", false);
            anim.SetBool("isRoaring", true);
            anim.SetBool("isAttacking", false);

            Invoke("StopPlayRoar04", 4.9f);
        }

        // roar 3
        else if (health.HealthPoint <= 0.5f * health.MaxHealthPoint && firstTimeRoar03)
        {
            print("lower than 50%");
            ws.SpawnWave03();

            agent.SetDestination(transform.position);
            anim.SetBool("isRunning", false);
            anim.SetBool("isIdling", false);
            anim.SetBool("isRoaring", true);
            anim.SetBool("isAttacking", false);

            Invoke("StopPlayRoar03", 4.9f);
        }

        // roar 2
        else if (health.HealthPoint <= 0.75f * health.MaxHealthPoint && firstTimeRoar02)
        {
            print("lower than 75%");
            ws.SpawnWave02();

            agent.SetDestination(transform.position);
            anim.SetBool("isRunning", false);
            anim.SetBool("isIdling", false);
            anim.SetBool("isRoaring", true);
            anim.SetBool("isAttacking", false);

            Invoke("StopPlayRoar02", 4.9f);
        }

        else if (isAttacking)
        {
            agent.SetDestination(transform.position);
            if (tempAttackTime <= 0)
            {
                Attack();
            }
        }
        else if (isInAtk)
        {
            agent.SetDestination(transform.position);
            anim.SetBool("isRunning", false);
            anim.SetBool("isIdling", false);

            anim.SetBool("isAttacking", true);
            anim.Play("Attack");
        }
        else if (isInRange)
        {
            if (firstTimeSeen)
            {
                agent.SetDestination(transform.position);
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdling", false);
                anim.SetBool("isRoaring", true);
                anim.Play("Roar");

                ws.SpawnWave01();

                Invoke("StopPlayRoar01", 4.9f);
            }
            else
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

        if (isDeadDrop && !isDroped)
        {
            PlayDeadDropSound(audioSource);
            isDroped = true;
        }

        if (isRoaring)
        {
            PlayRoarSound(audioSource);
        }
        if (health.HealthPoint <= 0 || sedat.SedatPoints <= 0)
        {
            GetComponent<GatherSyringe>().ShowUI(textPrefab);
        }
    }
    void Attack()
    {
        if (isDealDamage)
        {
            PlayAttackSound(audioSource);
            if (HitPlayer())
            {
                target.GetComponent<IHealth>().TakeDamage(damage);
            }
        }
        tempAttackTime = attackDelay;
    }
    bool HitPlayer()
    {
        foreach (var hurtBox in GetComponentsInChildren<HurtBox>())
        {
            if (hurtBox.hitPlayer)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
    private void OnDestroy()
    {
        SaveInfo();
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, visionRange);
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
    void StopPlayRoar01()
    {
        anim.SetBool("isRoaring", false);
        firstTimeSeen = false;
    }
    void StopPlayRoar02()
    {
        anim.SetBool("isRoaring", false);
        firstTimeRoar02 = false;
    }
    void StopPlayRoar03()
    {
        anim.SetBool("isRoaring", false);
        firstTimeRoar03 = false;
    }
    void StopPlayRoar04()
    {
        anim.SetBool("isRoaring", false);
        firstTimeRoar04 = false;
    }
}
