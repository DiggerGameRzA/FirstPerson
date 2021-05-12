using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryItem
{
    int Id { get; }
    bool Collected { get; set; }
    string Name { get; }
    Sprite Image { get; }
    int Amount { get; }
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