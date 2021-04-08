using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Inventory inventory;
    public UIManager uiManager;
    public WeaponManager wm;
    IPlayer player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<IPlayer>();
    }
    void Update()
    {
        RaycastItem();

        if(Input.GetButtonDown("Open Inventory"))
        {
            if (!uiManager.GetInventoryVisible())
            {
                uiManager.ShowInventory(true);
            }
            else
            {
                uiManager.ShowInventory(false);
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

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipWeaponInSlot(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipWeaponInSlot(2);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if(player.GetWeapon() != null)
                wm.Fire();
        }
        else if (Input.GetButtonDown("Reload"))
        {
            if (player.GetWeapon() != null)
                wm.Reload();
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
            if (Input.GetButtonDown("Collect"))
            {
                IInventoryItem item = hit.transform.GetComponent<IInventoryItem>();
                if (hit.transform.CompareTag("Item"))
                {
                    inventory.AddItem(item, "Item");
                }
                else if (hit.transform.CompareTag("Weapon"))
                {
                    inventory.AddItem(item, "Weapon");
                }
            }
        }
    }
    void EquipWeaponInSlot(int slot)
    {
        IInventoryItem item = inventory.GetPeekItem(slot, "Weapon");
        if (item != null && item.Weapon != WeaponEnum.None)
        {
            player.EquipWeapon(item.Weapon);
        }
        else
        {
            Debug.Log("There is no weapon in this slot.");
        }
    }
}
