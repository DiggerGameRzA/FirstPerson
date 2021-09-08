using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Compy2 : MonoBehaviour
{
    //float
    public float visionRange; //Compy's detection range.
    public float speed; //Compy's speed.

    //bool
    public bool IsChase,Nest;

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
        anim = GetComponentInChildren<Animator>();
        //target = PlayerManager.instance.player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if(IsChase != true)
        if (distance > visionRange)
        {
            agent.SetDestination(transform.position);
            anim.SetBool("isIdling", true);
        }
        else if (distance < visionRange)
        {
            IsChase = true;
        }

        if(IsChase == true)
        {
            agent.SetDestination(nest.transform.position);
            anim.SetBool("isIdling", false);
            anim.SetBool("isRunning", true);
        }

        else if(Nest == true)
        {

        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Nest"))
        {
            IsChase = false;
            Nest = true;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }
}
