using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour,IInventoryItem
{
    public string _name = "Item";
    public Sprite _image = null;
    public WeaponEnum _weapon = WeaponEnum.None;
   
    public string Name
    {
        get { return _name; }
    }
    public Sprite Image
    {
        get { return _image; }
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
        
    }
    public void ShowUI(bool show)
    {
        transform.GetChild(1).gameObject.SetActive(show);
    }
    void Update()
    {
        RaycastHit hit = CameraManager.GetCameraRaycast(50f);
        if(this.transform != hit.transform)
        {
            ShowUI(false);
        }
        else
        {
            ShowUI(true);
        }
    }
}
