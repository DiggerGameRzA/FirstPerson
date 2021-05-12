using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour, IWeapon
{
    public WeaponEnum WeaponType { get { return WeaponEnum.Pistol; } }
    [Header("Firing")]
    public float _fireDelay = 0.5f;

    [Header("Ammo")]
    public int _currentAmmo = 0;
    public int _maxAmmo = 10;
    public int _currentSpare = 0;
    public int _maxSpare = 30;

    [Header("Damage")]
    public int _damage = 10;

    public GameObject muzzlePrefab;
    public Transform muzzleTranform;

    public float FireDelay 
    {
        get { return _fireDelay; }
    }
    public int CurrentAmmo
    {
        get { return _currentAmmo; }
        set { _currentAmmo = value; }
    }
    public int MaxAmmo
    {
        get { return _maxAmmo; }
        set { _maxAmmo = value; }
    }
    public int CurrentSpare
    {
        get { return _currentSpare; }
        set { _currentSpare = value; }
    }
    public int MaxSpare
    {
        get { return _maxSpare; }
        set { _maxSpare = value; }
    }
    public int Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }
    public bool IsEquiped { get; set; }
    GameObject PistolPrefab;
    public void Equip()
    {
        PistolPrefab = Camera.main.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
        foreach (Transform i in PistolPrefab.transform.parent)
        {
            if(i.name != "Arm_R")
            {
                i.gameObject.SetActive(false);
            }
        }
        PistolPrefab.SetActive(true);
        SaveManager.instance.currentWeapon = WeaponEnum.Pistol;
    }
    public Animator GetAnimator()
    {
        return PistolPrefab.GetComponent<Animator>();
    }
    public void Fire()
    {
        SoundManager.instance.PlayPistolFire();

        RaycastHit hit = CameraManager.GetCameraRaycast(100f);
        if (hit.collider.gameObject.GetComponent<Health>())
        {
            hit.transform.gameObject.GetComponent<Health>().TakeDamage(Damage);
            int index;
            if (hit.transform.gameObject.GetComponent<UtahRaptor>())
            {
                index = hit.transform.gameObject.GetComponent<UtahRaptor>().id;
            }
            else
            {
                index = hit.transform.gameObject.GetComponent<Compy>().id;
            }
            SaveManager.instance.dinos[index].healthPoint = hit.transform.gameObject.GetComponent<Health>().HealthPoint;

        }
        else if (!hit.collider.gameObject.GetComponent<Health>())
        {

        }
        else if (hit.collider == null)
        {

        }
        muzzleTranform = Camera.main.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0);
        GameObject go = Instantiate(muzzlePrefab, muzzleTranform);
        go.transform.parent = muzzleTranform;
        //GameObject muzzlePrefab = Camera.main.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
        //muzzlePrefab.SetActive(true);
    }
    public void NoAmmo()
    {
        SoundManager.instance.PlayPistolNoAmmo();
    }
    public void Reload()
    {
        SoundManager.instance.PlayPistolReload();
    }
}
