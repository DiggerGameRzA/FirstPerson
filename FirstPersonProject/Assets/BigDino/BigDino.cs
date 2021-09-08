using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BigDino : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    Animator anim;
    [SerializeField]
    Transform target;
    [SerializeField]
    GameObject targetObject;
    [SerializeField]
    Transform player;
    [SerializeField]
    NavMeshAgent agent;
    [SerializeField]
    ParticleSystem stomp;
    [SerializeField]
    AudioSource sound;
    
    [Header("Values")]
    [SerializeField]
    float damRange;         //Range of damage area.
    [SerializeField]
    float visRange;         //Rang of dino vision.
    [SerializeField]
    float damage;           //Damage player takes standing in damage area.
    [SerializeField]
    float stepRate;         //How frequent the feet move.
    [SerializeField]
    bool isMove;            //Check if dino is on the move.
    [SerializeField]
    float speed;            //Dino based speed.
    [SerializeField]
    float timer;

    private void Awake()
    {
        stomp.Stop();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.GetComponent<NavMeshAgent>().speed = speed;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = targetObject.transform;
    }

    void Update()
    {
        //Player distance from this dino.
        float distance = Vector3.Distance(player.position, transform.position);

        if(isMove)
        {
            anim.SetBool("isWalking", true);
            OnTheMove();

            timer += Time.deltaTime;
            if(timer > stepRate)
            {
                sound.Play();
                stomp.Play();
                timer = 0;

                if(distance < damRange)
                {
                    Debug.Log("TakeDAM");
                }
            }
        }

        if(distance < visRange)
        {
            isMove = true;
        }
    }

    void OnTheMove()
    {
        agent.SetDestination(target.position);
    }

    private void OnDrawGizmos()
    {
        //Draw wire sphere for damage range.
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damRange);

        //Draw wire sphere for vision range.
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, visRange);
    }
}
