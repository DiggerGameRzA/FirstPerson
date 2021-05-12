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
    CharacterController controller;
    Vector3 velocity;

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
        }

        if (Input.GetButton("Jump") && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(stats.JumpForce * stats.Gravity);
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
        }
        else if (weapon == WeaponEnum.Sedat)
        {
            this.weapon = weaponManager.GetComponent<Sedat>();
        }
        else if (weapon == WeaponEnum.AssaultRifle)
        {
            this.weapon = weaponManager.GetComponent<AssaultRifle>();
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
