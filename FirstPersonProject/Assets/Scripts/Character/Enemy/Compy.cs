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
    bool isAtNest = false;

    //gameObject
    public GameObject nest;

    //Component
    Transform target;
    NavMeshAgent agent;
    Animator anim;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        anim = GetComponent<Animator>();
        target = FindObjectOfType<Player>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        float disToNest = Vector3.Distance(transform.position, nest.transform.position);

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
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Nest")
        {
            print("aaaa");
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }
}
