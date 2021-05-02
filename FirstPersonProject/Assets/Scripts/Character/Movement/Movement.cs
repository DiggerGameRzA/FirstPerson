using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : IMovement
{
    IPlayer player;
    Stats stats;
    Rigidbody rb;
    Transform transform;
    //Transform handTransform;
    public Movement(IPlayer player)
    {
        this.player = player;
        stats = player.GetStats();
        rb = player.GetRigidbody();
        transform = player.GetTransform();
        //handTransform = player.GetHandTransform();
    }

    public void Walk(Vector3 direction, float speed)
    {
        rb.AddForce(direction * speed);
    }
    public void RotateBody(Quaternion direction)
    {
        transform.rotation = direction;
    }
    public void RotateHand(Quaternion direction)
    {
        //handTransform.rotation = direction;
    }
    public void Jump(float jumpForce)
    {
        rb.AddForce(0, jumpForce, 0);
    }
}
