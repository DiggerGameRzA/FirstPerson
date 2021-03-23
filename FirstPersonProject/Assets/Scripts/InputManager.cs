using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Inventory inventory;
    public UIManager ui;
    private void Start()
    {
        
    }
    void Update()
    {
        RaycastItem();

        if(Input.GetButtonDown("Open Inventory"))
        {
            if (!ui.GetInventoryVisible())
            {
                ui.ShowInventory(true);
            }
            else
            {
                ui.ShowInventory(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
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
    private void RaycastItem()
    {
        RaycastHit hit = CameraManager.GetCameraRaycast(50f);
        if (hit.transform)
        {
            IInventoryItem item = hit.transform.GetComponent<IInventoryItem>();
            if (hit.transform.CompareTag("Item"))
            {
                item.ShowUI(true);
                if (Input.GetButtonDown("Collect"))
                {
                    inventory.AddItem(item, "Item");
                }
            }
            else if (hit.transform.CompareTag("Weapon"))
            {
                item.ShowUI(true);
                if (Input.GetButtonDown("Collect"))
                {
                    inventory.AddItem(item, "Weapon");
                }
            }
            
        }
    }
}
