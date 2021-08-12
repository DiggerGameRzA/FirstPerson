using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    IPlayer player;
    public int id = 0;
    public bool needKey = false;
    public bool isOpened = false;
    public string keyName;
    public string keyDisplay;
    public GameObject cardReader;

    [Header("Enter Zone")]
    public bool enterZone = false;
    public GameObject[] gate;

    [Header("Change Scene")]
    public string from;
    public string goTo;
    /*
     * Door id
     * 
     * Checkpoint Door = 0
     * SecurityRoom = 1
     * SecurityRoom_Out = 2
     * MedicalRoom = 3
     * MedicalRoom_Out = 4
     * AGate = 5
     * 
     */
    void Start()
    {
        if(cardReader == null)
        {

        }
        player = FindObjectOfType<Player>().GetComponent<IPlayer>();

        //needKey = SaveManager.instance.doorNeedKey[id];
    }

    void Update()
    {
        if (CameraManager.camera != null)
        {
            RaycastHit hit = CameraManager.GetCameraRaycast(player.GetStats().InteractRange);
            if (hit.transform)
            {
                if (this.transform.name == hit.collider.name)
                {
                    ShowUI(true);
                }
                else
                {
                    ShowUI(false);
                }
            }
            else
            {
                ShowUI(false);
            }
        }
        if (!needKey)
        {
            if (cardReader != null)
            {
                Material mat = cardReader.GetComponent<MeshRenderer>().materials[1];
                mat.color = Color.green;
            }
        }
        if (isOpened)
        {
            GetComponent<BoxCollider>().enabled = false;
        }
    }
    public void ShowUI(bool show)
    {
        transform.GetChild(0).gameObject.SetActive(show);
    }
    public void OnEnter()
    {
        print("Entering " + gameObject.name);
        if (from == "Level01" && goTo == "MedicalRoom")
        {
             GameManager.instance.LoadMedRoom();
        }
        else if (from == "MedicalRoom" && goTo == "Level01")
        {
            GameManager.instance.LoadMedToLevel01();
        }
        else if (from == "Level01" && goTo == "SecurityRoom")
        {
            GameManager.instance.LoadSecRoom();
        }
        else if(from == "SecurityRoom" && goTo == "Level01")
        {
            GameManager.instance.LoadSecToLevel01();
        }
        else if (from == "Level01" && goTo == "Level02")
        {
            GameManager.instance.LoadLevel02();
        }
    }
    public void OnOpen()
    {
        for (int i = 0; i < gate.Length; i++)
        {
            gate[i].GetComponent<Animator>().Play("Open");
        }
    }
}
