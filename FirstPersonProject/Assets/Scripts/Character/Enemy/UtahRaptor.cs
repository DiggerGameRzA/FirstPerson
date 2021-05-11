using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UtahRaptor : MonoBehaviour
{
    //float
    public float visionRange;
    public float attackRange;
    public float attackDelay;
    public float damage;
    public float speed;

    //States
    bool isDead = false;
    bool isSleep = false;

    bool isInRange = false;
    bool isInAtk = false;

    float tempTime = 0f;

    Transform target;
    NavMeshAgent agent;
    Animator anim;
    IHealth health;
    ISedat sedat;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        anim = GetComponent<Animator>();
        target = FindObjectOfType<Player>().transform;
        health = GetComponent<IHealth>();
        sedat = GetComponent<SedatPoint>();
    }

    void Update()
    {
        tempTime -= Time.deltaTime;

        float distance = Vector3.Distance(target.position, transform.position);
        if (health.HealthPoint <= 0)
        {
            health.OnDead();
            isDead = true;
        }
        else if (sedat.SedatPoints <= 0)
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

        if (isDead)
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
        else if (isInAtk)
        {
            agent.SetDestination(transform.position);
            anim.SetBool("isAttacking", true);
            anim.SetBool("isRunning", false);
            anim.SetBool("isIdling", false);
            if (tempTime <= 0)
            {
                tempTime = attackDelay;
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
            anim.SetBool("isRunning", true);
            anim.SetBool("isIdling", false);
            anim.SetBool("isAttacking", false);
        }
        else if (!isInAtk && !isInRange)
        {
            agent.SetDestination(transform.position);
            anim.SetBool("isIdling", true);
            anim.SetBool("isRunning", false);
            anim.SetBool("isAttacking", false);
        }
        else
        {
            isInRange = false;
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
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, visionRange);
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
