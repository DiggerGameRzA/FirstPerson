﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Stats))]
public class Player : MonoBehaviour, IPlayer
{
    Stats stats;

    Rigidbody rb;
    IMovement movement;
    IMovementDir movementDir;
    CharacterController controller;
    Vector3 velocity;
    Animator anim;
    PoisonState poison;

    IWeapon weapon;
    IHealth health;

    GameObject weaponManager;
    public UIManager uiManager;
    public bool InputManagerinstancecanMove { get; set; }

    Collider col;
    //Transform handPrefabs;

    void Start()
    {
        stats = GetComponent<Stats>();
        rb = GetComponent<Rigidbody>();
        //handPrefabs = transform.GetChild(0);
        movement = new Movement(this);
        movementDir = new MovementDir();
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        poison = GetComponent<PoisonState>();

        weaponManager = GameObject.Find("Weapon Manager");
        weapon = null;
        uiManager = UIManager.instance;
        health = GetComponent<Health>();

        col = GetComponent<Collider>();
    }

    void Update()
    {
        //movement.RotateHand(CameraManager.GetCameraRotation());
        movement.RotateBody(CameraManager.GetCameraRotationY());
    }
    void FixedUpdate()
    {
        velocity.y += -stats.Gravity * Time.deltaTime;
        //rb.velocity = new Vector3(0, rb.velocity.y, 0);

        controller.Move(velocity);
        if (InputManager.instance.canMove)
        {
            if (Input.GetButton("Sprint"))
            {
                movement.Walk(movementDir.GetMovementDir(), stats.RunSpeed);
            }
            else
            {
                movement.Walk(movementDir.GetMovementDir(), stats.WalkSpeed);
            }

            if(movementDir.GetMovementDir() != Vector3.zero)
            {
                Animator hAnim = Camera.main.transform.GetChild(2).GetComponent<Animator>();
                AnimationCon.SetPlayerRun(anim, true);
                //AnimationCon.SetPlayerRun(hAnim, true);
            }
            else
            {
                Animator hAnim = Camera.main.transform.GetChild(2).GetComponent<Animator>();
                AnimationCon.SetPlayerRun(anim, false);
                //AnimationCon.SetPlayerRun(hAnim, false);
            }
        }
        else
        {
            AnimationCon.SetPlayerRun(anim, false);
        }
        if (InputManager.instance.canMove) 
        {
            if (Input.GetButton("Jump") && controller.isGrounded)
            {
                velocity.y = Mathf.Sqrt(stats.JumpForce * stats.Gravity);
            }
        }
    }
    public Stats GetStats()
    {
        return stats;
    }
    public Rigidbody GetRigidbody()
    {
        return rb;
    }
    public Transform GetTransform()
    {
        return transform;
    }
    public CharacterController GetCharacterController()
    {
        return controller;
    }
    public IWeapon GetWeapon()
    {
        return weapon;
    }
    public IHealth GetHealth()
    {
        return health;
    }
    public Animator GetAnimator()
    {
        return anim;
    }
    public PoisonState GetPoisonState()
    {
        return poison;
    }
    /*
    public Transform GetHandTransform()
    {
        return handPrefabs;
    }
    */
    public void EquipWeapon(WeaponEnum weapon)
    {
        weaponManager.GetComponent<WeaponManager>().tempReloadTime = 0;

        if(weapon == WeaponEnum.Pistol)
        {
            this.weapon = weaponManager.GetComponent<Pistol>();
        }
        else if (weapon == WeaponEnum.Sedat)
        {
            this.weapon = weaponManager.GetComponent<Sedat>();
        }
        else if (weapon == WeaponEnum.AssaultRifle)
        {
            this.weapon = weaponManager.GetComponent<AssaultRifle>();
        }
        else if (weapon == WeaponEnum.Bazuka)
        {
            this.weapon = weaponManager.GetComponent<Bazuka>();
        }
        else if (weapon == WeaponEnum.Shotgun)
        {
            this.weapon = weaponManager.GetComponent<Shotgun>();
        }

        if (this.weapon != null)
        {
            this.weapon.Equip();
            uiManager.UpdateAmmo(this.weapon.CurrentAmmo, this.weapon.CurrentSpare);
        }
    }
    bool IsGrounded()
    {
        RaycastHit hit;
        Physics.Raycast(col.bounds.center, Vector3.down, out hit, col.bounds.extents.y + 0.01f);
        return hit.collider != null;
    }
}
