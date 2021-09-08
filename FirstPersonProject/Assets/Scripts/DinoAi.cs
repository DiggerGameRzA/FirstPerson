using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DinoAi : MonoBehaviour
{
    //Values
    public float runRange,walkRange,atkRange; //Range.
    public float walkSpd, runSpd;             //Walk and Run Speed.

    //Components
    NavMeshAgent agent;
    Animator anim;

    //GameObjects
    public Transform target;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance > walkRange && distance > runRange)
        {
            agent.SetDestination(transform.position);
            anim.SetBool("isWalking", false);
            anim.SetBool("isRunning", false);
        }

        else if (distance < runRange && distance > walkRange)
        {
            agent.SetDestination(target.transform.position);
            agent.GetComponent<NavMeshAgent>().speed = runSpd;
            anim.SetBool("isRunning", true);
        }
        
        else if(distance < walkRange && distance > atkRange)
        {
            agent.SetDestination(target.position);
            agent.GetComponent<NavMeshAgent>().speed = walkSpd;
            anim.SetBool("isWalking", true);
            anim.SetBool("isRunning", false);
        }
        else if(distance < walkRange && distance < atkRange)
        {
            agent.SetDestination(transform.position);
            anim.SetBool("isWalking", false);
            anim.SetBool("isEating", true);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, walkRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, runRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, atkRange);
    }
}
