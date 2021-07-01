using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayer
{
    Stats GetStats();
    Rigidbody GetRigidbody();
    Transform GetTransform();
    CharacterController GetCharacterController();
    IWeapon GetWeapon();
    IHealth GetHealth();
    Animator GetAnimator();
    void EquipWeapon(WeaponEnum weapon);
    //Transform GetHandTransform();
}
