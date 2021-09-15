using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonState : MonoBehaviour
{
    public float Damage = 1;
    public float tempPoisonTime = 0;
    public float tempTime = 1;

    void Update()
    {
        tempPoisonTime -= Time.deltaTime;

        if(tempPoisonTime > 0)
        {
            FindObjectOfType<UIManager>().ShowPoison(true);
            tempTime -= Time.deltaTime;
            if (tempTime <= 0)
            {
                FindObjectOfType<Player>().GetComponent<IPlayer>().GetHealth().TakeDamage(Damage);
                tempTime = 1;
            }
        }
        else
        {
            FindObjectOfType<UIManager>().ShowPoison(false);
            tempTime = 0;
        }
    }
}
