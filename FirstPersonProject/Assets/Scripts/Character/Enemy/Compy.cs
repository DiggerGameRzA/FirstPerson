using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Compy : MonoBehaviour
{
    //float
    public float visionRange; //Compy's detection range.
    public float speed; //Compy's speed.

    //bool
    public bool IsChase = false;

    //gameObject
    public GameObject nest;

    //Component
    Transform target;
    NavMeshAgent agent;
    Animator anim;
    IHealth health;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        anim = GetComponent<Animator>();
        target = FindObjectOfType<Player>().transform;
        health = GetComponent<IHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        float disToNest = Vector3.Distance(transform.position, nest.transform.position);

        if (health.HealthPoint <= 0)
        {
            agent.SetDestination(transform.position);
            agent.speed = 0;
            anim.SetBool("isDead", true);
            anim.Play("Velociraptor_Death");
        }
        else
        {
            if (distance < visionRange && disToNest > 2)
            {
                IsChase = true;
            }
            else if (disToNest < 2)
            {
                IsChase = false;
            }

            if (!IsChase)
            {
                agent.SetDestination(transform.position);
                anim.SetBool("isIdling", true);
                anim.SetBool("isRunning", false);
            }
            else if (IsChase)
            {
                agent.SetDestination(nest.transform.position);
                anim.SetBool("isIdling", false);
                anim.SetBool("isRunning", true);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }
}
