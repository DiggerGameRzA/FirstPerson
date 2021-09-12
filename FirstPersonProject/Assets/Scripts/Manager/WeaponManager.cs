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

    public LayerMask lDefault;

    public int currentSlot;
    float tempFireTime = 0f;
    public float tempReloadTime = 0;
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
            if (weapon.GetAnimator() != null)
            {
                Invoke("ResetAnimation", Mathf.Abs(weapon.FireDelay - 0.2f));
                weapon.GetAnimator().Play("Fire");
            }
            weapon.Fire();

            RaycastHit hit = CameraManager.GetCameraRaycast(100f, lDefault);

            if (hit.transform)
            {
                if(hit.collider.tag == "HurtBox")
                {
                    
                }
                else if (hit.collider.GetComponentInParent<EnemyStats>())
                {
                    EnemyStats es = hit.collider.GetComponentInParent<EnemyStats>();
                    if (es.GetComponent<Health>().HealthPoint > 0)
                    {
                        uiManager.ShowHitMark();
                        if (weapon.WeaponType == WeaponEnum.Bazuka)
                        {
                            SoundManager.instance.PlayBazukaExplode();
                        }
                        uiManager.Invoke("HideHitMark", 0.5f);
                    }
                }
            }
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
        if (weapon.GetAnimator() != null)
        {
            weapon.GetAnimator().SetBool("fire", false);
        }
    }
    public void Restart()
    {
        player = FindObjectOfType<Player>().GetComponent<IPlayer>();
        uiManager = UIManager.instance;
        InputManager.instance.EquipWeaponInSlot(currentSlot);
    }
}
