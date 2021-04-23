using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pistol))]
[RequireComponent(typeof(Sedat))]
[RequireComponent(typeof(AssaultRifle))]
public class WeaponManager : MonoBehaviour
{
    [SerializeField] IPlayer player;
    [SerializeField] IWeapon weapon;
    [SerializeField] UIManager uiManager;

    float tempTime = 0f;
    void Start()
    {
        player = FindObjectOfType<Player>().GetComponent<IPlayer>();
        uiManager = FindObjectOfType<UIManager>();
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
            weapon.GetAnimator().SetBool("fire", true);
            Invoke("ResetAnimation", weapon.FireDelay - 0.2f);
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
    void ResetAnimation()
    {
        weapon.GetAnimator().SetBool("fire", false);
    }
}
