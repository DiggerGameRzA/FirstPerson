using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtBox : MonoBehaviour
{
    public bool hitPlayer = false;
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            hitPlayer = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        hitPlayer = false;
    }
}
