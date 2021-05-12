using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherSyringe : MonoBehaviour
{
    IPlayer player;
    public GameObject dna;
    void Start()
    {
    }
    private void Update()
    {
        
    }
    public void ShowUI(GameObject text)
    {
        player = FindObjectOfType<Player>();
        if (CameraManager.camera != null)
        {
            RaycastHit hit = CameraManager.GetCameraRaycast(player.GetStats().InteractRange);
            if (this.transform != hit.transform)
            {
                text.gameObject.SetActive(false);
            }
            else
            {
                text.gameObject.SetActive(true);
            }
        }
    }
}
