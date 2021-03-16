using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    Inventory inventory;
    void Start()
    {
        inventory = new Inventory();
    }
    void Update()
    {
        RaycastHit hit = CameraManager.GetCameraRaycast(100f);
        IInventoryItem item = hit.transform.GetComponent<IInventoryItem>();
        if (item != null)
        {
            if (hit.transform.CompareTag("Item"))
            {
                if (Input.GetButtonDown("Collect"))
                    inventory.AddItem(item);
            }
        }
    }
    public static float GetVerInput()
    {
        return Input.GetAxis("Vertical");
    }
    public static float GetHorInput()
    {
        return Input.GetAxis("Horizontal");
    }
}
