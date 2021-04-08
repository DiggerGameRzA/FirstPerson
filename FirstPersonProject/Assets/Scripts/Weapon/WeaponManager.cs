using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pistol))]
[RequireComponent(typeof(Sedat))]
public class WeaponManager : MonoBehaviour
{
    IPlayer player;
    IWeapon weapon;
    public UIManager uiManager;

    float tempTime = 0f;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<IPlayer>();
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
            weapon.Fire();
            Debug.Log("Pew");
        }
    }
    public void Reload()
    {
        if (weapon.CurrentSpare == 0)
        {
            Debug.Log("Can not reload");
        }
        else
        {
            Debug.Log("Reloading");
            for (int i = weapon.CurrentAmmo; i < weapon.MaxAmmo; i++)
            {
                if (weapon.CurrentSpare == 0)
                {
                    break;
                }
                else
                {
                    weapon.CurrentAmmo++;
                    weapon.CurrentSpare--;
                    uiManager.UpdateAmmo(weapon.CurrentAmmo, weapon.CurrentSpare);
                }
            }
        }
    }
}
