using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    IPlayer player;
    public bool needKey = false;
    public string keyName;

    [Header("Change Scene")]
    public string from;
    public string goTo;
    void Start()
    {
        player = FindObjectOfType<Player>().GetComponent<IPlayer>();
    }

    void Update()
    {
        if (CameraManager.camera != null)
        {
            RaycastHit hit = CameraManager.GetCameraRaycast(player.GetStats().InteractRange);
            if (this.transform != hit.transform)
            {
                ShowUI(false);
            }
            else
            {
                ShowUI(true);
            }
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
    }
}
