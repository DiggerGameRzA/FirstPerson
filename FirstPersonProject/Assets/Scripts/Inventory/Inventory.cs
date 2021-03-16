using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private const int SLOTS = 5;
    //public List<IInventoryItem> mItems = new List<IInventoryItem>();
    public List<InventorySlot> mSlots = new List<InventorySlot>();
    public event EventHandler<InventoryEventArgs> ItemAdded;
    public event EventHandler<InventoryEventArgs> ItemUsed;
    public event EventHandler<InventoryEventArgs> ItemRemoved;

    public Inventory()
    {
        for (int i = 0; i < SLOTS; i++)
        {
            mSlots.Add(new InventorySlot(i));
        }
    }

    private InventorySlot FindStackkableSlot(IInventoryItem item)
    {
        foreach (InventorySlot slot in mSlots)
        {
            if (slot.IsStackable(item))
                return slot;
        }
        return null;
    }
    private InventorySlot FindNextEmptySlot()
    {
        foreach (InventorySlot slot in mSlots)
        {
            if (slot.IsEmpty)
                return slot;
        }
        return null;
    }

    public IInventoryItem ItemTop()
    {
        IInventoryItem item = null;
        foreach (InventorySlot slot in mSlots)
        {
            if (slot.Count > 0)
                item = slot.mItemStack.Peek();
        }
        return item;
    }
    public void AddItem(IInventoryItem item)
    {
        InventorySlot freeSlot = FindStackkableSlot(item);
        if (freeSlot == null)
        {
            freeSlot = FindNextEmptySlot();
        }
        if (freeSlot != null)
        {
            freeSlot.AddItem(item);
            item.OnPickUp();
            if (ItemAdded != null)
            {
                ItemAdded(this, new InventoryEventArgs(item));
            }
        }
        Debug.Log("Added Item : " + item);
        /*
          if(mItems.Count < SLOTS)
          {
              Collider collider = (item as MonoBehaviour).GetComponent<Collider>();
              if(collider.enabled)
              {
                  collider.enabled = false;
                  mItems.Add(item);
                  item.OnPickup();
                  if(ItemAdded != null)
                  {
                      ItemAdded(this, new InventoryEventArgs(item));
                  }
              }
          }
         */
    }
    internal void UseItem(IInventoryItem item)
    {
        if(ItemUsed != null)
        {
            ItemUsed(this, new InventoryEventArgs(item));
        }
        item.OnUse();
    }
    public void RemoveItem(IInventoryItem item)
    {
        foreach (InventorySlot slot in mSlots)
        {
            if (slot.Remove(item))
            {
                if (ItemRemoved != null)
                {
                    ItemRemoved(this, new InventoryEventArgs(item));
                }
                break;
            }
        }
    }
}
