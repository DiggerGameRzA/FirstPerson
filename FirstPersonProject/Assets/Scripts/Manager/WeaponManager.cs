using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pistol))]
[RequireComponent(typeof(Sedat))]
[RequireComponent(typeof(AssaultRifle))]
public class WeaponManager : MonoBehaviour
{
    public static WeaponManager instance;

    [SerializeField] IPlayer player;
    [SerializeField] IWeapon weapon;
    [SerializeField] UIManager uiManager;

    public int currentSlot;
    float tempTime = 0f;
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
        //DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        player = FindObjectOfType<Player>().GetComponent<IPlayer>();
        uiManager = FindObjectOfType<UIManager>();
        if (player.GetWeapon() != null)
        {
            InputManager.instance.EquipWeaponInSlot(currentSlot);
        }
    }
    void Update()
    {
        weapon = player.GetWeapon();
        tempTime -= Time.deltaTime;
    }
    public void Fire()
    {
        if(tempTime <= 0 && weapon.CurrentAmmo > 0)
        {
            tempTime = weapon.FireDelay;
            weapon.CurrentAmmo -= 1;
            uiManager.UpdateAmmo(weapon.CurrentAmmo, weapon.CurrentSpare);
            //weapon.GetAnimator().SetBool("fire", true);
            weapon.GetAnimator().Play("Fire");
            Invoke("ResetAnimation", Mathf.Abs(weapon.FireDelay - 0.2f));
            weapon.Fire();
        }
        else if(tempTime <= 0 && weapon.CurrentAmmo <= 0)
        {
            weapon.NoAmmo();
        }
    }
    public void Reload()
    {
        if (weapon.CurrentSpare == 0)
        {

        }
        else
        {
            for (int i = weapon.CurrentAmmo; i < weapon.MaxAmmo; i++)
            {
                if (weapon.CurrentSpare == 0)
                {
                    break;
                }
                else
                {
                    weapon.Reload();
                    weapon.CurrentAmmo++;
                    weapon.CurrentSpare--;
                    uiManager.UpdateAmmo(weapon.CurrentAmmo, weapon.CurrentSpare);
                }
            }
        }
    }
    void ResetAnimation()
    {
        weapon.GetAnimator().SetBool("fire", false);
    }
    public void Restart()
    {
        player = FindObjectOfType<Player>().GetComponent<IPlayer>();
        uiManager = UIManager.instance;
        InputManager.instance.EquipWeaponInSlot(currentSlot);
    }
}
