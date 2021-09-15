using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : IMovement
{
    IPlayer player;
    Stats stats;
    Rigidbody rb;
    Transform transform;
    CharacterController controller;
    //Transform handTransform;
    public Movement(IPlayer player)
    {
        this.player = player;
        stats = player.GetStats();
        rb = player.GetRigidbody();
        transform = player.GetTransform();
        controller = this.player.GetCharacterController();
        //handTransform = player.GetHandTransform();
    }
    public void Walk(Vector3 direction, float speed)
    {
        //rb.AddForce(direction * speed);
        controller = player.GetCharacterController();
        controller.Move(direction * speed);
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
        //rb.AddForce(0, jumpForce, 0);
        //rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        controller.Move(new Vector3(0, Mathf.Sqrt(jumpForce * player.GetStats().Gravity), 0));
    }
}
