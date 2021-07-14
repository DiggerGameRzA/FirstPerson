using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingDinoBG : MonoBehaviour
{
    int ranSpawn;
    float ranSpeed;
    Animator anim;
    private void Start()
    {
        ranSpawn = Random.Range(0, 20);
        if(ranSpawn >= 15)
        {
            Destroy(this.gameObject);
        }

        anim = GetComponent<Animator>();
        anim.SetBool("isFlying", true);
        anim.Play("Pterodactyl_Flap");

        ranSpeed = Random.Range(0.8f, 1.2f);

        Destroy(this.gameObject, 15f);
    }
    void Update()
    {
        
        transform.Translate(0, 0, 0.5f * ranSpeed);
    }
}
