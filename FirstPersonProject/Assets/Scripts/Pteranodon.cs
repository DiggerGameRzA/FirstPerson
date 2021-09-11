using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pteranodon : MonoBehaviour
{
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        anim.SetBool("isFlying", true);
        transform.Translate(0, 0, 1f);
    }
}
