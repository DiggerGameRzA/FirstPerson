using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour,IInventoryItem
{
    [Header("Status")]
    public int _id = 0;
    public bool _collected = false;

    [Header("Item's Infomations")]
    public string _name = "Item";
    public Sprite _image = null;
    public int _amount = 1;
    public WeaponEnum _weapon = WeaponEnum.None;
    public LayerMask interactable;
    IPlayer player;
    [SerializeField] Inventory inventory;

    private void Start()
    {
        player = FindObjectOfType<Player>().GetComponent<IPlayer>();
        inventory = Inventory.instance;

        _collected = SaveManager.instance.collected[_id];
        if (_collected)
            Destroy(this.gameObject);
    }
    public int Id
    {
        get { return _id; }
    }
    public bool Collected
    {
        get { return _collected; }
        set { _collected = value; }
    }
    public string Name
    {
        get { return _name; }
    }
    public Sprite Image
    {
        get { return _image; }
    }
    public int Amount
    {
        get { return _amount; }
    }
    public WeaponEnum Weapon
    {
        get { return _weapon; }
    }
    public InventorySlot Slot { get; set; }
    public void OnPickUp()
    {
        gameObject.SetActive(false);
    }
    public virtual void OnUse()
    {
        player = FindObjectOfType<Player>().GetComponent<IPlayer>();

        print("Using Item : " + Name);
        if (Name == "First Aid M")
        {
            player.GetHealth().TakeHeal(50);
            inventory.RemoveItem(this);
        }
        else if (Name == "First Aid S")
        {
            player.GetHealth().TakeHeal(25);
            inventory.RemoveItem(this);
        }
    }
    public void ShowUI(bool show)
    {
        transform.GetChild(1).gameObject.SetActive(show);
    }
    void Update()
    {
        if (CameraManager.camera != null)
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

        transform.Rotate(0, 100.0f * Time.deltaTime, 0);
    }
}
