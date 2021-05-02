using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //Inventory
    GameObject inventoryUI;

    //Ammo
    GameObject ammoUI;
    GameObject currentAmmo;
    GameObject currentSpare;

    //Health Point
    GameObject hpBar;
    IHealth playerHealth;

    //Dialogue
    public Text dialogueName;
    public Text dialogueSentence;
    void Start()
    {
        inventoryUI = transform.GetChild(1).gameObject;

        ammoUI = transform.GetChild(2).gameObject;
        currentAmmo = ammoUI.transform.GetChild(2).gameObject;
        currentSpare = ammoUI.transform.GetChild(0).gameObject;

        hpBar = transform.GetChild(3).GetChild(0).GetChild(0).gameObject;
        playerHealth = FindObjectOfType<Player>().GetHealth();
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
    public void DialogueUpdateName(string name)
    {
        dialogueName.text = name.ToString();
    }
    public void DialogueUpdateSentence(string sentence)
    {
        dialogueSentence.text = sentence.ToString();
    }
    public IEnumerator TypeSentence(string sentence)
    {
        dialogueSentence.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueSentence.text += letter;
            yield return null;
        }
    }
    public void UpdateHealth(float hp)
    {
        hpBar.GetComponent<RectTransform>().sizeDelta = new Vector2(hp, 100);
    }
}
