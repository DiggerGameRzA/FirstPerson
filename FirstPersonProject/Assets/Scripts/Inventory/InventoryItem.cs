using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryItem
{
    string Name { get; }
    Sprite Image { get; }
    WeaponEnum Weapon { get; }
    InventorySlot Slot { get; set; }
    void OnPickUp();
    void OnUse();
    void ShowUI(bool show);
}
public class InventoryEventArgs : EventArgs
{
    public IInventoryItem Item;
    public InventoryEventArgs(IInventoryItem item)
    {
        Item = item;
    }
}