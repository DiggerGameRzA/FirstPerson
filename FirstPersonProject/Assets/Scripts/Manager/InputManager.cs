using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] IPlayer player;
    [SerializeField] Inventory inventory;
    [SerializeField] UIManager uiManager;
    [SerializeField] WeaponManager weaponManager;
    [SerializeField] CameraManager cameraManager;
    private void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
        uiManager = FindObjectOfType<UIManager>();
        weaponManager = FindObjectOfType<WeaponManager>();
        cameraManager = FindObjectOfType<CameraManager>();
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
                cameraManager.ShowCursor();
                cameraManager.CanNotLookAround();
                player.CanWalk = false;
            }
            else
            {
                uiManager.ShowInventory(false);
                cameraManager.HideCursor();
                cameraManager.CanLookAround();
                player.CanWalk = true;
            }
        }

        if (Input.GetButtonDown("Cancel"))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                cameraManager.ShowCursor();
            }
            else
            {
                cameraManager.HideCursor();
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
        else if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            UseItemInSlot(1);
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

        if (Input.GetKeyDown(KeyCode.P))
        {
            player.GetHealth().TakeDamage(10f);
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
        RaycastHit hit = CameraManager.GetCameraRaycast(player.GetStats().InteractRange);
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
    void UseItemInSlot(int slot)
    {
        IInventoryItem item = inventory.GetPeekItem(slot, "Item");
        if (item != null && item.Weapon == WeaponEnum.None)
        {
            item.OnUse();
        }
        else
        {
            Debug.Log("There is no item in this slot.");
        }
    }
}
