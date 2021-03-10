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

    void Start()
    {
        stats = GetComponent<Stats>();
        rb = GetComponent<Rigidbody>();
        movement = new Movement(this);
        movementDir = new MovementDir();
    }

    void FixedUpdate()
    {
        movement.Rotate(movementDir.GetMovementDir());
        movement.Walk(movementDir.GetMovementDir());
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
}
