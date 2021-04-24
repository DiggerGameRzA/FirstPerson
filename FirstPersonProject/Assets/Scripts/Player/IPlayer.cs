using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayer
{
    bool CanWalk { get; set; }
    Stats GetStats();
    Rigidbody GetRigidbody();
    Transform GetTransform();
    IWeapon GetWeapon();
    void EquipWeapon(WeaponEnum weapon);
    //Transform GetHandTransform();
}
