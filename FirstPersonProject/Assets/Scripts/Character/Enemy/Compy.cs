using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Compy : MonoBehaviour
{
    //float
    public float visionRange; //Compy's detection range.
    public float speed; //Compy's speed.

    //states
    bool isDead = false;
    bool isSleep = false;

    bool isInRange = false;
    bool isNest = false;
    
    //gameObject
    public GameObject nest;
    public GameObject escape;

    //Component
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

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        float disToNest = Vector3.Distance(transform.position, nest.transform.position);

        if (health.HealthPoint <= 0)
        {
            health.OnDead();
            isDead = true;
        }
        else if (sedat.SedatPoints <= 0)
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

        if (isDead)
        {
            agent.SetDestination(transform.position);
            agent.speed = 0;
            anim.SetBool("isDead", true);
            anim.Play("Death");
        }
        else if (isSleep)
        {
            agent.SetDestination(transform.position);
            agent.speed = 0;
            anim.SetBool("isDead", true);
            anim.Play("Death");
        }
        else if (isNest && isInRange)
        {
            agent.SetDestination(escape.transform.position);
            anim.SetBool("isIdling", false);
            anim.SetBool("isRunning", true);
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
        }
        else if (!isInRange)
        {
            agent.SetDestination(transform.position);
            anim.SetBool("isIdling", true);
            anim.SetBool("isRunning", false);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }
}
