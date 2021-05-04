using System.Collections;
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
    IWeapon weapon;
    IHealth health;
    GameObject weaponManager;
    public UIManager uiManager;
    public bool CanWalk { get; set; }

    Collider col;
    //Transform handPrefabs;

    void Start()
    {
        stats = GetComponent<Stats>();
        rb = GetComponent<Rigidbody>();
        //handPrefabs = transform.GetChild(0);
        movement = new Movement(this);
        movementDir = new MovementDir();

        weaponManager = GameObject.Find("Weapon Manager");
        weapon = null;
        uiManager = FindObjectOfType<UIManager>();
        health = GetComponent<Health>();

        CanWalk = true;
        col = GetComponent<Collider>();
    }

    void Update()
    {
        //movement.RotateHand(CameraManager.GetCameraRotation());
        //movement.RotateBody(CameraManager.GetCameraRotationY());
    }
    void FixedUpdate()
    {
        //rb.velocity = new Vector3(0, rb.velocity.y, 0);

        if (CanWalk)
        {
            if (Input.GetButton("Sprint"))
            {
                movement.Walk(movementDir.GetMovementDir(), stats.RunSpeed);
            }
            else
            {
                movement.Walk(movementDir.GetMovementDir(), stats.WalkSpeed);
            }
        }

        if (Input.GetButton("Jump") && IsGrounded())
        {
            movement.Jump(stats.JumpForce);
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
    public IWeapon GetWeapon()
    {
        return weapon;
    }
    public IHealth GetHealth()
    {
        return health;
    }
    /*
    public Transform GetHandTransform()
    {
        return handPrefabs;
    }
    */
    public void EquipWeapon(WeaponEnum weapon)
    {
        if(weapon == WeaponEnum.Pistol)
        {
            this.weapon = weaponManager.GetComponent<Pistol>();
            this.weapon.Equip();
            uiManager.UpdateAmmo(this.weapon.CurrentAmmo, this.weapon.CurrentSpare);
        }
        else if (weapon == WeaponEnum.Sedat)
        {
            this.weapon = weaponManager.GetComponent<Sedat>();
            this.weapon.Equip();
            uiManager.UpdateAmmo(this.weapon.CurrentAmmo, this.weapon.CurrentSpare);
        }
        else if (weapon == WeaponEnum.AssaultRifle)
        {
            this.weapon = weaponManager.GetComponent<AssaultRifle>();
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
