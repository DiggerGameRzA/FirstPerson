using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    public List<bool> doorNeedKey;
    public float HP = 100;
    public WeaponEnum currentWeapon;
    void Awake()
    {
        if (instance != null && instance != this)
        {
            print("Destroy myself");
            Destroy(this.gameObject);
        }
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }

        doorNeedKey.Add(true);      //0
        doorNeedKey.Add(false);     //1
        doorNeedKey.Add(false);     //2
        doorNeedKey.Add(true);      //3
        doorNeedKey.Add(false);     //4
    }
    public void UnlockDoor(int id)
    {
        doorNeedKey[id] = false;
    }
    public void LockDoor(int id)
    {
        doorNeedKey[id] = true;
    }
}
