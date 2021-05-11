using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    [Header("Doors")]
    public List<bool> doorNeedKey;

    readonly Dictionary<string, bool> scenes;

    [Header("Player infomations")]
    public float HP = 100;
    public WeaponEnum currentWeapon;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        //DontDestroyOnLoad(gameObject);

        doorNeedKey.Add(true);      //0
        doorNeedKey.Add(false);     //1
        doorNeedKey.Add(false);     //2
        doorNeedKey.Add(true);      //3
        doorNeedKey.Add(false);     //4

        scenes.Add("Level01", true);
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
