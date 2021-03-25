using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    GameObject inventoryUI;

    GameObject ammoUI;
    GameObject currentAmmo;
    GameObject currentSpare;
    void Start()
    {
        inventoryUI = transform.GetChild(1).gameObject;
        ammoUI = transform.GetChild(2).gameObject;
        currentAmmo = ammoUI.transform.GetChild(2).gameObject;
        currentSpare = ammoUI.transform.GetChild(0).gameObject;
    }

    void Update()
    {
        
    }
    public void ShowInventory(bool show)
    {
        inventoryUI.SetActive(show);
    }
    public bool GetInventoryVisible()
    {
        return inventoryUI.activeInHierarchy;
    }
    public void UpdateAmmo(int ammo, int spare)
    {
        currentAmmo.GetComponent<Text>().text = ammo.ToString();
        currentSpare.GetComponent<Text>().text = spare.ToString();
    }
}
