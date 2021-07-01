using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNAReader : MonoBehaviour
{
    public LayerMask interactable;
    IPlayer player;
    public int id = 0;
    public bool needDNA = true;
    public string dnaName;
    public GameObject gate;
    void Start()
    {
        player = FindObjectOfType<Player>();
    }
    void Update()
    {
        player = FindObjectOfType<Player>();
        if (CameraManager.camera != null)
        {
            if (needDNA)
            {
                RaycastHit hit = CameraManager.GetCameraRaycast(player.GetStats().InteractRange, interactable);
                if (this.transform != hit.transform)
                {
                    ShowUI(false);
                }
                else
                {
                    ShowUI(true);
                }
            }
            else
            {
                ShowUI(false);

                ChangeScreen();
            }
        }
    }
    public void ShowUI(bool show)
    {
        transform.GetChild(0).gameObject.SetActive(show);
    }
    public void ChangeScreen()
    {
        Material mat = transform.GetChild(1).GetComponent<MeshRenderer>().materials[2];
        mat.color = Color.green;
    }
}
