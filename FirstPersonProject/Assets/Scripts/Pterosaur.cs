using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pterosaur : MonoBehaviour
{
    //Values
    public bool fly;
    
    //Components
    Animator anim;

    //GameObject
    public GameObject skyPoint;
    void Start()
    {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
     if(fly != false)
        {
            anim.SetBool("isFlapping", true);
            transform.position = Vector3.MoveTowards(transform.position, skyPoint.transform.position, 0.06f);
            transform.LookAt(skyPoint.transform.position, Vector3.left);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        fly = true;
    }
}
