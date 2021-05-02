using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    WeaponEnum WeaponType { get; }

    float FireDelay { get; }

    int CurrentAmmo { get; set; }
    int MaxAmmo { get; set; }
    int CurrentSpare { get; set; }
    int MaxSpare { get; set; }

    int Damage { get; set; }

    bool IsEquiped { get; set; }

    Animator GetAnimator();
    void Equip();
    void Fire();
}
