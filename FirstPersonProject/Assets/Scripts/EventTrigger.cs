using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    public int id = 0;
    public Inventory inventory;
    public GameObject[] conditionDino;
    public GameObject[] conditionDoor;
    public GameObject[] dino;
    public GameObject cargo;
    public GameObject[] ammo9mm; //4
    public GameObject[] meds; //2
    public GameObject[] sedat; //8
    void Start()
    {
        inventory = Inventory.instance;
    }
    private void Update()
    {
        if (SaveManager.instance.firstTimeEvent[0] && id == 0)
        {
            IInventoryItem item = inventory.FindKeyItem("Med R. Keycard");
            if (item != null)
            {
                for (int i = 0; i < dino.Length; i++)
                {
                    dino[i].SetActive(true);
                }
                //Invoke("StartCon2", 0.2f);
                SaveManager.instance.firstTimeEvent[0] = false;
            }
            else
            {
                for (int i = 0; i < dino.Length; i++)
                {
                    dino[i].SetActive(false);
                }
            }
        }
        if (SaveManager.instance.firstTimeEvent[1] && id == 1)
        {
            bool condition = false;
            for (int i = 0; i < conditionDino.Length; i++)
            {
                if (conditionDino[i].GetComponent<Health>().HealthPoint <= 0)
                {
                    condition = true;
                }
                else
                {
                    condition = false;
                    break;
                }
            }
            if (condition)
            {
                for (int i = 0; i < dino.Length; i++)
                {
                    dino[i].SetActive(true);
                }
                SaveManager.instance.firstTimeEvent[1] = false;
            }
            else
            {
                for (int i = 0; i < dino.Length; i++)
                {
                    dino[i].SetActive(false);
                }
            }
        }
        if (SaveManager.instance.firstTimeEvent[2] && id == 2)
        {
            bool conditionA = false;
            for (int i = 0; i < conditionDino.Length; i++)
            {
                if (conditionDino[i].GetComponent<Health>().HealthPoint <= 0)
                {
                    conditionA = true;
                }
                else
                {
                    conditionA = false;
                    break;
                }
            }
            bool conditionB = false;
            for (int i = 0; i < conditionDino.Length; i++)
            {
                if (conditionDino[i].GetComponent<SedatPoint>().SedatPoints <= 0)
                {
                    conditionB = true;
                }
                else
                {
                    conditionB = false;
                    break;
                }
            }

            if (conditionA)
            {
                cargo.SetActive(true);

                for (int i = 0; i < ammo9mm.Length; i++)
                {
                    if (ammo9mm[i] != null)
                    {
                        int index = ammo9mm[i].GetComponent<IInventoryItem>().Id;
                        if (!SaveManager.instance.collected[index])
                        {
                            ammo9mm[i].SetActive(true);
                        }
                    }
                }
                for (int i = 0; i < meds.Length; i++)
                {
                    if (meds[i] != null)
                    {
                        int index = meds[i].GetComponent<IInventoryItem>().Id;
                        SaveManager.instance.collected[index] = true;
                    }
                }
                for (int i = 0; i < sedat.Length; i++)
                {
                    if (sedat[i] != null)
                    {
                        int index = sedat[i].GetComponent<IInventoryItem>().Id;
                        if (!SaveManager.instance.collected[index])
                        {
                            sedat[i].SetActive(false);
                        }
                    }
                }
                SaveManager.instance.firstTimeEvent[2] = false;
            }
            else if (conditionB)
            {
                cargo.SetActive(true);

                for (int i = 0; i < meds.Length; i++)
                {
                    if (meds[i] != null)
                    {
                        int index = meds[i].GetComponent<IInventoryItem>().Id;
                        if (!SaveManager.instance.collected[index])
                        {
                            meds[i].SetActive(true);
                        }
                    }
                }
                for (int i = 0; i < sedat.Length; i++)
                {
                    if (sedat[i] != null)
                    {
                        int index = sedat[i].GetComponent<IInventoryItem>().Id;
                        if (!SaveManager.instance.collected[index])
                        {
                            sedat[i].SetActive(true);
                        }
                    }
                }
                for (int i = 0; i < ammo9mm.Length; i++)
                {
                    if(ammo9mm[i] != null)
                    {
                        int index = ammo9mm[i].GetComponent<IInventoryItem>().Id;
                        //SaveManager.instance.collected[index] = true;
                    }
                }
                SaveManager.instance.firstTimeEvent[2] = false;
            }
            else
            {
                cargo.SetActive(false);

                for (int i = 0; i < ammo9mm.Length; i++)
                {
                    ammo9mm[i].SetActive(false);
                }
                for (int i = 0; i < meds.Length; i++)
                {
                    meds[i].SetActive(false);
                }
            }
        }
        if (SaveManager.instance.firstTimeEvent[4] && id == 4)
        {
            if (!conditionDoor[0].GetComponent<Door>().needKey)
            {
                StartCon2();
                SaveManager.instance.firstTimeEvent[4] = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (this.id == 5)
            {
                if (SaveManager.instance.firstTimeEvent[5] && id == 5)
                {
                    IInventoryItem item = inventory.FindKeyItem("Med R. Keycard");
                    if (item != null)
                    {
                        StartCon3();
                        SaveManager.instance.firstTimeEvent[5] = false;
                    }
                }
            }
            else if (this.id == 6)
            {
                if (SaveManager.instance.firstTimeEvent[6] && id == 6)
                {
                    StartCon4();
                    SaveManager.instance.firstTimeEvent[6] = false;
                }
            }
            else if (this.id == 7)
            {
                if(SaveManager.instance.firstTimeEvent[7] && id == 7)
                {
                    bool condition = false;
                    for (int i = 0; i < conditionDino.Length; i++)
                    {
                        if (conditionDino[i].GetComponent<Health>().HealthPoint <= 0)
                        {
                            condition = true;
                        }
                        else
                        {
                            condition = false;
                            break;
                        }
                    }
                    if (condition)
                    {
                        for (int i = 0; i < dino.Length; i++)
                        {
                            dino[i].SetActive(true);
                        }
                        SaveManager.instance.firstTimeEvent[7] = false;
                    }
                    else
                    {
                        for (int i = 0; i < dino.Length; i++)
                        {
                            dino[i].SetActive(false);
                        }
                    }
                }
            }
        }
    }
    void StartCon2()
    {
        GameObject.Find("Second Con").GetComponent<NPCDialogue>().TriggerDialogue();
    }
    void StartCon3()
    {
        GameObject.Find("Third Con").GetComponent<NPCDialogue>().TriggerDialogue();
    }
    void StartCon4()
    {
        GameObject.Find("Fourth Con").GetComponent<NPCDialogue>().TriggerDialogue();
    }
}
