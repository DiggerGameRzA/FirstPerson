using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sedat : MonoBehaviour, IWeapon
{
    public WeaponEnum WeaponType { get { return WeaponEnum.Sedat; } }
    [Header("Firing")]
    public float _fireDelay = 0.5f;

    [Header("Ammo")]
    public int _currentAmmo = 0;
    public int _maxAmmo = 10;
    public int _currentSpare = 0;
    public int _maxSpare = 30;
    
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
    public bool IsEquiped { get; set; }
    GameObject SedatPrefab;
    public void Equip()
    {
        SedatPrefab = Camera.main.transform.GetChild(0).GetChild(0).GetChild(1).gameObject;
        foreach (Transform i in SedatPrefab.transform.parent)
        {
            i.gameObject.SetActive(false);
        }
        SedatPrefab.SetActive(true);
    }
    public Animator GetAnimator()
    {
        return SedatPrefab.GetComponent<Animator>();
    }
    public void Fire()
    {

    }
}
