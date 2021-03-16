using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour,IInventoryItem
{
    public string _name = "Item";
    public Sprite _image = null;
    public float _range = 50f;
   
    public string Name
    {
        get { return _name; }
    }
    public Sprite Image
    {
        get { return _image; }
    }
    public float Range
    {
        get { return _range; }
    }
    public InventorySlot Slot { get; set; }
    public void OnPickUp()
    {
        gameObject.SetActive(false);
    }
    public virtual void OnUse()
    {
        
    }
}
