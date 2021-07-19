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
    float tempFireTime = 0f;
    [SerializeField] float tempReloadTime = 0;
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
        tempFireTime -= Time.deltaTime;
        tempReloadTime -= Time.deltaTime;

        if(tempReloadTime <= 0)
        {
            uiManager.HideReloading();
        }
        else
        {
            uiManager.ShowReloading();
        }
    }
    public void Fire()
    {
        if(tempFireTime <= 0 && weapon.CurrentAmmo > 0 && tempReloadTime <= 0)
        {
            tempFireTime = weapon.FireDelay;
            weapon.CurrentAmmo -= 1;
            uiManager.UpdateAmmo(weapon.CurrentAmmo, weapon.CurrentSpare);
            //weapon.GetAnimator().SetBool("fire", true);
            weapon.GetAnimator().Play("Fire");
            Invoke("ResetAnimation", Mathf.Abs(weapon.FireDelay - 0.2f));
            weapon.Fire();
        }
        else if(tempFireTime <= 0 && weapon.CurrentAmmo <= 0)
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
            if (weapon.CurrentAmmo != weapon.MaxAmmo)
            {
                tempReloadTime = weapon.ReloadDuration;
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
