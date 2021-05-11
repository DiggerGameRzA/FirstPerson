using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] Inventory inventory;

    [SerializeField] Transform weaponSlots;

    IPlayer player;
    UIManager uiManager;

    void Start()
    {
        inventory = Inventory.instance;
        inventory.ItemAdded += InventoryScript_ItemAdd;
        inventory.ItemRemoved += InventoryScript_ItemRemoved;

        inventory.WeaponAdded += InventoryScript_WeaponAdd;

        uiManager = FindObjectOfType<UIManager>();

        player = FindObjectOfType<Player>().GetComponent<IPlayer>();
    }
    private void Update()
    {
        
    }
    private void InventoryScript_ItemAdd(object sender,InventoryEventArgs e)
    {
        Transform inventoryPanel = transform.GetChild(1).GetChild(2);
        int index = -1;
        foreach(Transform slot in inventoryPanel)
        {
            index++;

            Transform imageTransform = slot.GetChild(0);
            Transform textTransform = slot.GetChild(1);
            Image image = imageTransform.GetComponent<Image>();
            Text txtCount = textTransform.GetComponent<Text>();

            if (index == e.Item.Slot.Id)
            {
                image.enabled = true;
                image.sprite = e.Item.Image;

                int ItemCount = e.Item.Slot.Count;
                if (ItemCount > 1)
                    txtCount.text = ItemCount.ToString();
                else
                    txtCount.text = "";
                break;
            }
        }
    }
    public void InventoryScript_WeaponAdd(object sender, InventoryEventArgs e)
    {
        int index = -1;
        foreach (Transform slot in weaponSlots)
        {
            index++;

            Transform imageTransform = slot.GetChild(0);
            Transform textTransform = slot.GetChild(1);
            Image image = imageTransform.GetComponent<Image>();

            if (index == e.Item.Slot.Id)
            {
                image.enabled = true;
                image.sprite = e.Item.Image;

                int ItemCount = e.Item.Slot.Count;
                break;
            }
        }
    }
    private void InventoryScript_ItemRemoved(object sender,InventoryEventArgs e)
    {
        Transform inventoryPanel = transform.GetChild(1).GetChild(2);
        int index = -1;
        foreach (Transform slot in inventoryPanel)
        {
            index++;

            Transform imageTransform = slot.GetChild(0);
            Transform textTransform = slot.GetChild(1);
            Image image = imageTransform.GetComponent<Image>();
            Text txtCount = textTransform.GetComponent<Text>();

            if (index == e.Item.Slot.Id)
            {
                int itemCount = e.Item.Slot.Count;
                if (itemCount < 2)
                    txtCount.text = "";
                else
                    txtCount.text = itemCount.ToString();

                if(itemCount == 0)
                {
                    image.enabled = false;
                    image.sprite = null;
                }

                break;
            }
        }
    }
    public void UseItemInSlot(int slot)
    {
        IInventoryItem item = inventory.GetPeekItem(slot - 1, "Item");
        if (item != null)
        {
            inventory.UseItem(item);
        }
        else
        {
            Debug.Log("There is no item in slot : " + slot);
        }
    }
}
