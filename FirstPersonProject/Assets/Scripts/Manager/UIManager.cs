using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    IPlayer player;
    //Inventory
    [SerializeField] GameObject inventoryUI;

    [Header("Ammo")]
    //Ammo
    [SerializeField] GameObject ammoUI;
    [SerializeField] GameObject currentAmmo;
    [SerializeField] GameObject currentSpare;
    [SerializeField] GameObject weaponIconParent;

    [Header("Health Point")]
    //Health Point
    [SerializeField] RectTransform hpBar;

    [Header("Dialogue")]
    //Dialogue
    public GameObject dialoguePrefab;
    public Image dialogueProfile;
    public Text dialogueSentence;
    public GameObject dialogueContinue;

    [Header("Subtitle")]
    public Text subtitle;

    [Header("Menu")]
    public GameObject menuPrefab;
    public GameObject gameOverPrefab;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        player = FindObjectOfType<Player>();
        inventoryUI = transform.GetChild(1).gameObject;

        ShowAmmoCanvas(false);
        ammoUI = transform.GetChild(2).gameObject;
        currentAmmo = ammoUI.transform.GetChild(2).gameObject;
        currentSpare = ammoUI.transform.GetChild(0).gameObject;

        hpBar = transform.GetChild(3).GetChild(0).GetChild(0).GetComponent<RectTransform>();
    }
    #region Inventory
    /*********************************************************************************************/
    public void ShowInventory(bool show)
    {
        inventoryUI.SetActive(show);
        CameraManager.instance.ShowCursor(show);

        CameraManager.instance.CanLookAround(!show);

        InputManager.instance.canMove = !show;
        InputManager.instance.canShoot = !show;
    }
    public bool GetInventoryVisible()
    {
        return inventoryUI.activeInHierarchy;
    }
    #endregion
    #region Ammo
    /*********************************************************************************************/
    public void UpdateAmmo(int ammo, int spare)
    {
        currentAmmo.GetComponent<Text>().text = ammo.ToString();
        currentSpare.GetComponent<Text>().text = spare.ToString();
    }
    public void ShowAmmoCanvas(bool show)
    {
        ammoUI.SetActive(show);
    }
    public void ShowWepIcon(int weapon)
    {
        foreach (Transform i in weaponIconParent.transform)
        {
            i.gameObject.SetActive(false);
        }
        weaponIconParent.transform.GetChild(weapon).gameObject.SetActive(true);
    }
    #endregion
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
    #region Dialogue
    /*********************************************************************************************/
    public void DialogueUpdateProfile(Sprite profile)
    {
        dialoguePrefab.SetActive(true);
        dialogueProfile.enabled = true;
        dialogueProfile.sprite = profile;
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

        ShowContinue(true);
        InputManager.instance.canSkip = true;
    }
    public void EndSentence()
    {
        dialoguePrefab.SetActive(false);
        dialogueProfile.enabled = false;
        dialogueProfile.sprite = null;
        InputManager.instance.OnConversation(false);
    }
    public void ShowContinue(bool show)
    {
        dialogueContinue.SetActive(show);
    }
    #endregion
    public void ShowMenu(bool show)
    {
        menuPrefab.SetActive(show);

        CameraManager.instance.CanLookAround(!show);
        CameraManager.instance.ShowCursor(show);

        InputManager.instance.canCollect = !show;
        InputManager.instance.canEquipWep = !show;
        InputManager.instance.canInteract = !show;
        InputManager.instance.canMove = !show;
        InputManager.instance.canOpenInv = !show;
        InputManager.instance.canShoot = !show;
    }
    public void ShowGameOver(bool show)
    {
        gameOverPrefab.SetActive(show);

        CameraManager.instance.CanLookAround(!show);
        CameraManager.instance.ShowCursor(show);

        InputManager.instance.canCollect = !show;
        InputManager.instance.canEquipWep = !show;
        InputManager.instance.canInteract = !show;
        InputManager.instance.canMove = !show;
        InputManager.instance.canOpenInv = !show;
        InputManager.instance.canShoot = !show;
    }
    public bool GetMenuVisible()
    {
        return menuPrefab.activeInHierarchy;
    }
    public void ResetUI()
    {
        player = FindObjectOfType<Player>();
        player.GetHealth().HealthPoint = SaveManager.instance.HP;
        UpdateHealth(player.GetHealth().HealthPoint);
    }
}
