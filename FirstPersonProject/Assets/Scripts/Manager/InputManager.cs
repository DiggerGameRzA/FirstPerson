using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    [SerializeField] IPlayer player;
    [SerializeField] Inventory inventory;
    [SerializeField] UIManager uiManager;
    [SerializeField] WeaponManager weaponManager;
    [SerializeField] CameraManager cameraManager;
    [SerializeField] HUD hud;

    public bool canShoot = true;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        //DontDestroyOnLoad(gameObject);

        inventory = Inventory.instance;
        cameraManager = CameraManager.instance;
        weaponManager = WeaponManager.instance;
    }
    void Start()
    {
        hud = FindObjectOfType<HUD>();
        player = FindObjectOfType<Player>().GetComponent<IPlayer>();
        uiManager = FindObjectOfType<UIManager>();
    }
    void Update()
    {
        RaycastItem();
        RaycastDoor();

        if (Input.GetButtonDown("Open Inventory"))
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

        if (Input.GetButtonDown("Cancel"))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                cameraManager.ShowCursor(true);
            }
            else
            {
                cameraManager.ShowCursor(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipWeaponInSlot(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipWeaponInSlot(1);
        }

        if (canShoot)
        {
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
    private void RaycastDoor()
    {
        RaycastHit hit = CameraManager.GetCameraRaycast(player.GetStats().InteractRange);
        if (hit.transform)
        {
            if (Input.GetButtonDown("Interact"))
            {
                Door door = hit.transform.GetComponent<Door>();
                if (hit.transform.CompareTag("Interactable"))
                {
                    if (!door.needKey)
                    {
                        if (door.enterZone)
                            door.OnOpen();
                        else
                            door.OnEnter();
                    }
                    else if (door.needKey)
                    {
                        if(inventory.FindKeyItem(door.keyName) != null)
                        {
                            inventory.RemoveItem(inventory.FindKeyItem(door.keyName));
                            door.needKey = false;
                            SaveManager.instance.UnlockDoor(door.id);
                            
                            if (door.enterZone)
                                door.OnOpen();
                            else
                                door.OnEnter();
                        }
                        else
                        {
                            uiManager.UpdateSubtitle("You need " + door.keyName + " to enter");
                        }
                    }
                }
            }
        }
    }
    public void EquipWeaponInSlot(int slot)
    {
        IInventoryItem item = inventory.GetPeekItem(slot, "Weapon");
        if (item != null && item.Weapon != WeaponEnum.None)
        {
            weaponManager.currentSlot = slot;
            player.EquipWeapon(item.Weapon);
        }
        else
        {
            Debug.Log("There is no weapon in slot : " + slot);
        }
    }
    public void Restart()
    {
        player = FindObjectOfType<Player>().GetComponent<IPlayer>();
        uiManager = FindObjectOfType<UIManager>();
    }
}
