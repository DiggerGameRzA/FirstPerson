using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] IPlayer player;
    [SerializeField] Inventory inventory;
    [SerializeField] UIManager uiManager;
    [SerializeField] WeaponManager weaponManager;
    private void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
        uiManager = FindObjectOfType<UIManager>();
        weaponManager = FindObjectOfType<WeaponManager>();
    }
    void Start()
    {
        player = FindObjectOfType<Player>().GetComponent<IPlayer>();
        uiManager = FindObjectOfType<UIManager>();
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
            if (player.GetWeapon() != null)
            {
                weaponManager.Fire();
            }
        }
        else if (Input.GetButton("Fire1"))
        {
            if (player.GetWeapon() != null)
            {
                if (player.GetWeapon().WeaponType == WeaponEnum.AssaultRifle)
                {
                    weaponManager.Fire();
                }
            }
        }
        else if (Input.GetButtonDown("Reload"))
        {
            if (player.GetWeapon() != null)
                weaponManager.Reload();
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
