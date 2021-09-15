using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamPanel : MonoBehaviour
{
    IPlayer player;
    public GameObject cardReader;
    public GameObject sea;
    public GameObject col;
    public bool isActive = false;
    void Start()
    {
        player = FindObjectOfType<Player>().GetComponent<IPlayer>();
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
        Material mat = cardReader.GetComponent<MeshRenderer>().materials[1];
        mat.color = Color.green;
        if (isActive)
        {
            GetComponent<BoxCollider>().enabled = false;
        }
    }

    public void ShowUI(bool show)
    {
        transform.GetChild(0).gameObject.SetActive(show);
    }
    public void OnActive()
    {
        Animator animSea = sea.GetComponent<Animator>();
        animSea.Play("Move");
        col.SetActive(false);
    }
}
