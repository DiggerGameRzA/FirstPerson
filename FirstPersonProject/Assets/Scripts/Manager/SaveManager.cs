using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    [Header("Doors")]
    public List<bool> doorNeedKey = new List<bool>();
    public List<bool> gateNeedDNA = new List<bool>();

    [Header("Scenes")]
    public List<FirstTimeScene> firstTimeScenes = new List<FirstTimeScene>();

    [Header("Dinos")]
    public List<DinoInfo> dinos = new List<DinoInfo>();
    public List<bool> gathered = new List<bool>();

    [Header("Items")]
    public List<bool> collected = new List<bool>();

    [Header("Event Trigger")]
    public List<bool> firstTimeEvent = new List<bool>();

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
    }
    /*
    private void Start()
    {
        doorNeedKey.Add(true);      //0
        doorNeedKey.Add(false);     //1
        doorNeedKey.Add(false);     //2
        doorNeedKey.Add(true);      //3
        doorNeedKey.Add(false);     //4
        doorNeedKey.Add(true);      //5

        gateNeedDNA.Add(true);      //0

        firstTimeScenes.Add(new FirstTimeScene("Level01", true));

        dinos.Add(new DinoInfo(0, 50f, 1, new Vector3(9, 1, 73), false));
        dinos.Add(new DinoInfo(1, 200f, 1, new Vector3(-27, 1, 81), false));

        dinos.Add(new DinoInfo(2, 200f, 1, new Vector3(29, 5, 87), false));
        dinos.Add(new DinoInfo(3, 200f, 1, new Vector3(27, 5, 63), false));
        dinos.Add(new DinoInfo(4, 200f, 1, new Vector3(-6, 1, 87), false));
        dinos.Add(new DinoInfo(5, 200f, 1, new Vector3(-5, 1, 73), false));

        for (int i = 0; i < 6; i++)
        {
            gathered.Add(false);
        }

        for (int i = 0; i < 15; i++)
        {
            collected.Add(false);
        }

        firstTimeEvent.Add(true);
        firstTimeEvent.Add(true);
        firstTimeEvent.Add(true);
        firstTimeEvent.Add(true);
    }
    */
    public void UnlockDoor(int id)
    {
        doorNeedKey[id] = false;
    }
    public void LockDoor(int id)
    {
        doorNeedKey[id] = true;
    }
}
