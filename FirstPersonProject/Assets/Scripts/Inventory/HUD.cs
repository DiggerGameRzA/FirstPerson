using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    IPlayer player;
    UIManager uiManager;

    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();

        inventory = Inventory.instance;
        inventory.ItemAdded += InventoryScript_ItemAdd;
        inventory.ItemRemoved += InventoryScript_ItemRemoved;

        inventory.WeaponAdded += InventoryScript_WeaponAdd;
        
        player = FindObjectOfType<Player>().GetComponent<IPlayer>();
        Invoke("ResetUI", 0.1f);
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
    private void InventoryScript_WeaponAdd(object sender, InventoryEventArgs e)
    {
        Transform inventoryPanel = transform.GetChild(1).GetChild(1);
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
        IInventoryItem item = null;
        if (inventory.iSlots[slot - 1].mItemStack.Count != 0)
        {
            item = inventory.iSlots[slot - 1].mItemStack.Peek();
            inventory.UseItem(item);
        }
        else
        {
            Debug.Log("There is no item in this slot.");
        }
    }
    void ResetUI()
    {
        IInventoryItem item;

        for (int i = 0; i < inventory.wSlots.Count; i++)
        {
            if (inventory.wSlots[i].mItemStack.Count != 0)
            {
                item = inventory.wSlots[i].mItemStack.Peek();
                InventoryScript_WeaponAdd(inventory, new InventoryEventArgs(item));
            }
        }
        for (int i = 0; i < inventory.wSlots.Count; i++)
        {
            if (inventory.iSlots[i].mItemStack.Count != 0)
            {
                item = inventory.iSlots[i].mItemStack.Peek();
                InventoryScript_ItemAdd(inventory, new InventoryEventArgs(item));
            }
        }
        player.GetHealth().HealthPoint = SaveManager.instance.HP;
        uiManager.UpdateHealth(player.GetHealth().HealthPoint);
    }
}
