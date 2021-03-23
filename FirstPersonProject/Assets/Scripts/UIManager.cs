using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void ShowInventory(bool show)
    {
        transform.GetChild(1).gameObject.SetActive(show);
    }
    public bool GetInventoryVisible()
    {
        return transform.GetChild(1).gameObject.activeInHierarchy;
    }
}
