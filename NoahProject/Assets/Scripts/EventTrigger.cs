using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    public int id = 0;
    public Inventory inventory;
    public GameObject[] conditionDino;
    public GameObject[] conditionDinoB;
    public GameObject[] conditionDoor;
    public GameObject[] spawnDino;
    public GameObject[] spawnItems;
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
                for (int i = 0; i < spawnDino.Length; i++)
                {
                    spawnDino[i].SetActive(true);
                }
                //Invoke("StartCon2", 0.2f);
                SaveManager.instance.firstTimeEvent[0] = false;
            }
            else
            {
                for (int i = 0; i < spawnDino.Length; i++)
                {
                    spawnDino[i].SetActive(false);
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
                for (int i = 0; i < spawnDino.Length; i++)
                {
                    spawnDino[i].SetActive(true);
                }
                SaveManager.instance.firstTimeEvent[1] = false;
            }
            else
            {
                for (int i = 0; i < spawnDino.Length; i++)
                {
                    spawnDino[i].SetActive(false);
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
                    if (ammo9mm[i] != null)
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
        if (SaveManager.instance.firstTimeEvent[8] && id == 8)
        {
            IInventoryItem item = inventory.FindKeyItem("Syringe Tricera DNA");
            if (item != null)
            {
                StartCon5();
                SaveManager.instance.firstTimeEvent[8] = false;
            }
        }
        if (SaveManager.instance.firstTimeEvent[9] && id == 9)
        {
            if (conditionDoor[0].GetComponent<Door>().isOpened)
            {
                StartCon6();
                SaveManager.instance.firstTimeEvent[9] = false;
            }
        }
        if (SaveManager.instance.firstTimeEvent[10] && id == 10)
        {
            IInventoryItem item = inventory.FindKeyItem("Syringe Stego DNA");
            if (item != null)
            {
                StartCon7();
                SaveManager.instance.firstTimeEvent[10] = false;
            }
        }
        if (SaveManager.instance.firstTimeEvent[11] && id == 11)
        {
            if (conditionDoor[0].GetComponent<Door>().isOpened)
            {
                StartCon8();
                SaveManager.instance.firstTimeEvent[11] = false;
            }
        }
        if (SaveManager.instance.firstTimeEvent[12] && id == 12)
        {
            IInventoryItem item = inventory.FindKeyItem("Syringe Brachio DNA");
            if (item != null)
            {
                StartCon9();
                FindObjectOfType<SoundManager>().PlayForest();
                FindObjectOfType<SwampSound>().gameObject.SetActive(false);
                SaveManager.instance.firstTimeEvent[12] = false;
            }
        }
        if (SaveManager.instance.firstTimeEvent[13] && id == 13)
        {
            for (int i = 0; i < conditionDino.Length; i++)
            {
                IHealth conDino = conditionDino[i].GetComponent<IHealth>();
                if (conDino.HealthPoint < conDino.MaxHealthPoint)
                {
                    for (int j = 0; j < spawnDino.Length; j++)
                    {
                        spawnDino[j].SetActive(true);
                    }

                    SaveManager.instance.firstTimeEvent[13] = false;
                    break;
                }
                else
                {
                    for (int j = 0; j < spawnDino.Length; j++)
                    {
                        spawnDino[j].SetActive(false);
                    }
                }
            }
        }
        if (SaveManager.instance.firstTimeEvent[14] && id == 14)
        {
            bool conditionA = false;
            for (int i = 0; i < conditionDino.Length; i++)
            {
                IHealth conDino = conditionDino[i].GetComponent<IHealth>();
                if (conDino.HealthPoint <= 0)
                {
                    conditionA = true;
                    SaveManager.instance.firstTimeEvent[14] = false;
                    break;
                }
            }
            bool conditionB = false;
            for (int i = 0; i < conditionDino.Length; i++)
            {
                ISedat conDino = conditionDino[i].GetComponent<ISedat>();
                if (conDino.SedatPoints <= 0)
                {
                    conditionB = true;
                    SaveManager.instance.firstTimeEvent[14] = false;
                    break;
                }
            }
            if (conditionA)
            {
                if (!SaveManager.instance.firstTimeEvent[7] || !SaveManager.instance.firstTimeEvent[14])
                {
                    Kentro[] kentro = FindObjectsOfType<Kentro>();
                    for (int i = 0; i < kentro.Length; i++)
                    {
                        kentro[i].isHit = true;
                    }
                    for (int j = 0; j < spawnDino.Length; j++)
                    {
                        spawnDino[j].SetActive(true);
                        //spawnDino[j].transform.position = new Vector3(340, 1, 190);
                    }
                    for (int i = 0; i < conditionDinoB.Length; i++)
                    {
                        conditionDinoB[i].SetActive(false);
                    }
                    for (int i = 0; i < spawnItems.Length; i++)
                    {
                        spawnItems[i].SetActive(true);
                    }
                    cargo.SetActive(true);
                }
            }
            else if (conditionB)
            {
                if (SaveManager.instance.firstTimeEvent[14])
                {
                    for (int j = 0; j < meds.Length; j++)
                    {
                        meds[j].SetActive(false);
                    }
                }
                else
                {
                    for (int j = 0; j < meds.Length; j++)
                    {
                        meds[j].SetActive(true);
                    }
                }
                cargo.SetActive(true);
            }
            else
            {
                if (SaveManager.instance.firstTimeEvent[7] && SaveManager.instance.firstTimeEvent[14])
                {
                    for (int j = 0; j < spawnDino.Length; j++)
                    {
                        spawnDino[j].SetActive(false);
                    }
                }
                for (int j = 0; j < meds.Length; j++)
                {
                    meds[j].SetActive(false);
                }
                for (int i = 0; i < spawnItems.Length; i++)
                {
                    spawnItems[i].SetActive(false);
                }
                cargo.SetActive(false);
            }

        }
        if (SaveManager.instance.firstTimeEvent[15] && id == 15)
        {
            bool conditionA = false;
            if (conditionDino[0].GetComponent<Health>().HealthPoint <= 0)
            {
                conditionA = true;
            }
            else
            {
                conditionA = false;
            }

            bool conditionB = false;
            if (conditionDino[0].GetComponent<SedatPoint>().SedatPoints <= 0)
            {
                conditionB = true;
            }
            else
            {
                conditionB = false;
            }

            if (conditionA)
            {
                for (int i = 0; i < spawnItems.Length; i++)
                {
                    spawnItems[i].SetActive(true);
                }
                for (int i = 0; i < meds.Length; i++)
                {
                    meds[i].SetActive(false);
                }
                cargo.SetActive(true);
                SaveManager.instance.firstTimeEvent[15] = false;
            }
            else if (conditionB)
            {
                for (int i = 0; i < spawnItems.Length; i++)
                {
                    spawnItems[i].SetActive(false);
                }
                for (int i = 0; i < meds.Length; i++)
                {
                    meds[i].SetActive(true);
                }
                cargo.SetActive(true);
                SaveManager.instance.firstTimeEvent[15] = false;
            }
            else
            {
                for (int i = 0; i < spawnItems.Length; i++)
                {
                    spawnItems[i].SetActive(false);
                }
                for (int i = 0; i < meds.Length; i++)
                {
                    meds[i].SetActive(false);
                }
                cargo.SetActive(false);
            }
        }
        if (SaveManager.instance.firstTimeEvent[16] && id == 16)
        {
            bool[] conditionA = new bool[2];
            for (int i = 0; i < conditionDino.Length; i++)
            {
                if (conditionDino[i].GetComponent<IHealth>().HealthPoint <= 0)
                {
                    conditionA[i] = true;
                }
            }
            if (conditionA[0] && conditionA[1])
            {
                for (int i = 0; i < spawnDino.Length; i++)
                {
                    spawnDino[i].SetActive(true);
                }
                for (int i = 0; i < spawnItems.Length; i++)
                {
                    spawnItems[i].SetActive(true);
                }
                SaveManager.instance.firstTimeEvent[16] = false;
            }
            if (SaveManager.instance.firstTimeEvent[16])
            {
                for (int i = 0; i < spawnDino.Length; i++)
                {
                    spawnDino[i].SetActive(false);
                }
                for (int i = 0; i < spawnItems.Length; i++)
                {
                    spawnItems[i].SetActive(false);
                }
            }
        }
        if (SaveManager.instance.firstTimeEvent[17] && id == 17)
        {
            bool[] conditionA = new bool[2];
            for (int i = 0; i < conditionDino.Length; i++)
            {
                if (conditionDino[i].GetComponent<IHealth>().HealthPoint <= 0)
                {
                    conditionA[i] = true;
                }
            }
            if (conditionA[0] && conditionA[1])
            {
                for (int i = 0; i < spawnDino.Length; i++)
                {
                    spawnDino[i].SetActive(true);
                }
                SaveManager.instance.firstTimeEvent[17] = false;
            }
            if (SaveManager.instance.firstTimeEvent[17])
            {
                for (int i = 0; i < spawnDino.Length; i++)
                {
                    spawnDino[i].SetActive(false);
                }
            }
        }
        if (SaveManager.instance.firstTimeEvent[18] && id == 18)
        {
            bool conditionA = false;
            bool[] conA1 = new bool[3];
            for (int i = 0; i < conditionDino.Length; i++)
            {
                if (conditionDino[i].GetComponent<IHealth>().HealthPoint <= 0)
                {
                    conA1[i] = true;
                }
            }
            for (int i = 0; i < conditionDinoB.Length; i++)
            {
                if (conditionDinoB[i].GetComponent<IHealth>().HealthPoint <= 0)
                {
                    conA1[2] = true;
                    break;
                }
            }
            if (conA1[0] && conA1[1] && conA1[2])
            {
                conditionA = true;
            }

            bool conditionB = false;
            bool[] conB = new bool[3];
            for (int i = 0; i < conditionDino.Length; i++)
            {
                if (conditionDino[i].GetComponent<ISedat>().SedatPoints <= 0)
                {
                    conB[i] = true;
                }
            }
            for (int i = 0; i < conditionDinoB.Length; i++)
            {
                if (conditionDinoB[i].GetComponent<ISedat>().SedatPoints <= 0)
                {
                    conB[2] = true;
                    break;
                }
            }
            if (conB[0] && conB[1] && conB[2])
            {
                conditionB = true;
            }

            if (conditionA)
            {
                for (int i = 0; i < spawnItems.Length; i++)
                {
                    spawnItems[i].SetActive(true);
                }
                for (int i = 0; i < meds.Length; i++)
                {
                    meds[i].SetActive(false);
                }
                SaveManager.instance.firstTimeEvent[18] = false;
            }
            else if (conditionB) 
            {
                for (int i = 0; i < spawnItems.Length; i++)
                {
                    spawnItems[i].SetActive(false);
                }
                for (int i = 0; i < meds.Length; i++)
                {
                    meds[i].SetActive(true);
                }
                SaveManager.instance.firstTimeEvent[18] = false;
            }
            else
            {
                for (int i = 0; i < spawnItems.Length; i++)
                {
                    spawnItems[i].SetActive(false);
                }
                for (int i = 0; i < meds.Length; i++)
                {
                    meds[i].SetActive(false);
                }
            }
        }
        if (SaveManager.instance.firstTimeEvent[19] && id == 19)
        {
            bool conditionA = false;
            if (conditionDino[0].GetComponent<IHealth>().HealthPoint <= 0)
            {
                conditionA = true;
            }
            bool conditionB = false;
            if (conditionDino[0].GetComponent<ISedat>().SedatPoints <= 0)
            {
                conditionB = true;
            }

            if (conditionA)
            {
                for (int i = 0; i < spawnItems.Length; i++)
                {
                    spawnItems[i].SetActive(true);
                }
                SaveManager.instance.firstTimeEvent[19] = false;
            }
            else if (conditionB)
            {
                for (int i = 0; i < meds.Length; i++)
                {
                    meds[i].SetActive(true);
                }
                SaveManager.instance.firstTimeEvent[19] = false;
            }
            else
            {
                for (int i = 0; i < spawnItems.Length; i++)
                {
                    spawnItems[i].SetActive(false);
                }
                for (int i = 0; i < meds.Length; i++)
                {
                    meds[i].SetActive(false);
                }
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
                        SaveManager.instance.firstTimeEvent[7] = false;
                    }
                }

                if (!SaveManager.instance.firstTimeEvent[7])
                {
                    for (int i = 0; i < spawnDino.Length; i++)
                    {
                        spawnDino[i].SetActive(true);
                    }
                    for (int i = 0; i < conditionDinoB.Length; i++)
                    {
                        conditionDinoB[i].SetActive(false);
                    }
                }
            }
        }
    }
    #region Conversations
    void StartCon2()
    {
        GameObject.Find("Con2").GetComponent<NPCDialogue>().TriggerDialogue();
    }
    void StartCon3()
    {
        GameObject.Find("Con3").GetComponent<NPCDialogue>().TriggerDialogue();
    }
    void StartCon4()
    {
        GameObject.Find("Con4").GetComponent<NPCDialogue>().TriggerDialogue();
    }
    void StartCon5()
    {
        GameObject.Find("Con5").GetComponent<NPCDialogue>().TriggerDialogue();
    }
    void StartCon6()
    {
        GameObject.Find("Con6").GetComponent<NPCDialogue>().TriggerDialogue();
    }
    void StartCon7()
    {
        GameObject.Find("Con7").GetComponent<NPCDialogue>().TriggerDialogue();
    }
    void StartCon8()
    {
        GameObject.Find("Con8").GetComponent<NPCDialogue>().TriggerDialogue();
    }
    void StartCon9()
    {
        GameObject.Find("Con9").GetComponent<NPCDialogue>().TriggerDialogue();
    }
    #endregion
}
