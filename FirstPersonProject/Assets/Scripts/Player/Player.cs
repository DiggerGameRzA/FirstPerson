using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Stats))]
public class Player : MonoBehaviour, IPlayer
{
    Stats stats;
    Rigidbody rb;
    IMovement movement;
    IMovementDir movementDir;
    [SerializeField] Transform handPrefabs;

    void Start()
    {
        stats = GetComponent<Stats>();
        rb = GetComponent<Rigidbody>();
        //handPrefabs = transform.GetChild(0);
        movement = new Movement(this);
        movementDir = new MovementDir();
    }

    void Update()
    {
        //movement.RotateHand(CameraManager.GetCameraRotation());
        movement.RotateBody(CameraManager.GetCameraRotationY());
    }
    void FixedUpdate()
    {
        rb.velocity = new Vector3(0, rb.velocity.y, 0);

        if (Input.GetButton("Sprint"))
        {
            movement.Walk(movementDir.GetMovementDir(), stats.RunSpeed);
        }
        else
        {
            movement.Walk(movementDir.GetMovementDir(), stats.WalkSpeed);
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
    public Transform GetHandTransform()
    {
        return handPrefabs;
    }
}
