using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : IMovement
{
    IPlayer player;
    Stats stats;
    Rigidbody rb;
    Transform transform;
    public Movement(IPlayer player)
    {
        this.player = player;
        stats = player.GetStats();
        rb = player.GetRigidbody();
        transform = player.GetTransform();
    }
    
    public void Walk(Vector3 direction)
    {
        rb.velocity = direction * stats.WalkSpeed;
    }
    public void Run(Vector3 direction)
    {
        rb.velocity = direction * stats.WalkSpeed;
    }
    public void Rotate(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), stats.RotationSpeed);
        }
    }
}
