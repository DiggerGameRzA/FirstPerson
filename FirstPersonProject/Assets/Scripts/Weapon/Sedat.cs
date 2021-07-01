using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sedat : MonoBehaviour, IWeapon
{
    public LayerMask entity;
    public WeaponEnum WeaponType { get { return WeaponEnum.Sedat; } }
    [Header("Firing")]
    public float _fireDelay = 0.5f;

    [Header("Ammo")]
    public int _currentAmmo = 0;
    public int _maxAmmo = 10;
    public int _currentSpare = 0;
    public int _maxSpare = 30;

    [Header("Damage")]
    public int _damage = 10;

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
    GameObject SedatPrefab;
    public void Equip()
    {
        SedatPrefab = Camera.main.transform.GetChild(0).GetChild(0).GetChild(1).gameObject;
        foreach (Transform i in SedatPrefab.transform.parent)
        {
            if (i.name != "Arm_R")
            {
                i.gameObject.SetActive(false);
            }
        }
        SedatPrefab.SetActive(true);
        SaveManager.instance.currentWeapon = WeaponEnum.Sedat;
    }
    public Animator GetAnimator()
    {
        return SedatPrefab.GetComponent<Animator>();
    }
    public void Fire()
    {
        RaycastHit hit = CameraManager.GetCameraRaycast(100f, entity);
        if (hit.collider.gameObject.GetComponent<SedatPoint>())
        {
            hit.transform.gameObject.GetComponent<SedatPoint>().TakeSedat(Damage);
            int index;
            EnemyStats dino = hit.transform.GetComponent<EnemyStats>();
            index = dino.id;
            /*
            if (hit.transform.gameObject.GetComponent<UtahRaptor>())
            {
                index = hit.transform.gameObject.GetComponent<UtahRaptor>().id;
            }
            else
            {
                index = hit.transform.gameObject.GetComponent<Compy>().id;
            }
            */
            SaveManager.instance.dinos[index].sedatPoint = hit.transform.gameObject.GetComponent<SedatPoint>().SedatPoints;
        }
        else if (!hit.collider.gameObject.GetComponent<Health>())
        {

        }
        else if (hit.collider == null)
        {

        }
    }
    public void NoAmmo()
    {

    }
    public void Reload()
    {

    }
}
