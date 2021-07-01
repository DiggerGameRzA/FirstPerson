using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherSyringe : MonoBehaviour
{
    public LayerMask entity;
    IPlayer player;
    public GameObject dna;
    public bool gathered = false;
    void Start()
    {
        /*
        if(GetComponent<UtahRaptor>())
            gathered = SaveManager.instance.gathered[GetComponent<UtahRaptor>().id];
        else if(GetComponent<Compy>())
            gathered = SaveManager.instance.gathered[GetComponent<Compy>().id];
        */
        gathered = SaveManager.instance.gathered[GetComponent<EnemyStats>().id];
    }
    public void ShowUI(GameObject text)
    {
        player = FindObjectOfType<Player>();
        if (CameraManager.camera != null)
        {
            RaycastHit hit = CameraManager.GetCameraRaycast(player.GetStats().InteractRange, entity);
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
