using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    public int id = 0;
    public Inventory inventory;
    public GameObject[] dino;
    public GameObject cargo;
    public GameObject[] ammo9mm;
    public GameObject[] meds;
    void Start()
    {
        inventory = Inventory.instance;

        if (SaveManager.instance.firstTimeEvent[0] && id == 0)
        {
            IInventoryItem item = inventory.FindKeyItem("Med R. Keycard");
            if (item != null)
            {
                for (int i = 0; i < dino.Length; i++)
                {
                    dino[i].SetActive(true);
                }
                Invoke("StartCon2", 0.2f);
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
            if(SaveManager.instance.dinos[1].healthPoint <= 0)
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
    }
    private void Update()
    {
        if (SaveManager.instance.firstTimeEvent[2] && id == 2)
        {
            if (SaveManager.instance.dinos[1].healthPoint <= 0)
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
                        SaveManager.instance.firstTimeEvent[2] = false;
            }
            else if (SaveManager.instance.dinos[1].sedatPoint <= 0)
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
                for (int i = 0; i < ammo9mm.Length; i++)
                {
                    if(ammo9mm[i] != null)
                    {
                        int index = ammo9mm[i].GetComponent<IInventoryItem>().Id;
                        SaveManager.instance.collected[index] = true;
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
    }
    void StartCon2()
    {
        GameObject.Find("Second Con").GetComponent<NPCDialogue>().TriggerDialogue();
    }
    public void StartCon3()
    {
        GameObject.Find("Third Con").GetComponent<NPCDialogue>().TriggerDialogue();
        SaveManager.instance.firstTimeEvent[3] = false;
    }
}
