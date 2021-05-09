using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    IPlayer player;
    //Inventory
    GameObject inventoryUI;

    //Ammo
    GameObject ammoUI;
    GameObject currentAmmo;
    GameObject currentSpare;

    //Health Point
    [SerializeField] RectTransform hpBar;
    IHealth playerHealth;

    //Dialogue
    public Text dialogueName;
    public Text dialogueSentence;

    public Text subtitle;
    void Start()
    {
        player = FindObjectOfType<Player>();
        inventoryUI = transform.GetChild(1).gameObject;

        ammoUI = transform.GetChild(2).gameObject;
        currentAmmo = ammoUI.transform.GetChild(2).gameObject;
        currentSpare = ammoUI.transform.GetChild(0).gameObject;

        hpBar = transform.GetChild(3).GetChild(0).GetChild(0).GetComponent<RectTransform>();
        playerHealth = FindObjectOfType<Player>().GetHealth();
    }
    public void ShowInventory(bool show)
    {
        inventoryUI.SetActive(show);
        CameraManager.instance.ShowCursor(show);

        player.CanWalk = !show;
        InputManager.instance.canShoot = !show;
        CameraManager.instance.CanLookAround(!show);
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
        SaveManager.instance.HP = hp;
        hpBar.sizeDelta = new Vector2(hp, 100);
    }
    public void UpdateSubtitle(string sentence)
    {
        subtitle.text = "";
        LeanTween.cancelAll();
        LeanTween.alphaText(subtitle.gameObject.GetComponent<RectTransform>(), 1f, 0f);
        subtitle.text = sentence;
        Invoke("TextFadeOut", 3f);
    }
    void TextFadeOut()
    {
        LeanTween.alphaText(subtitle.gameObject.GetComponent<RectTransform>(), 0f, 1f);
    }
}
