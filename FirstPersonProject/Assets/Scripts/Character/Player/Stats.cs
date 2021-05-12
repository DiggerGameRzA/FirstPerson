using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [Header("Interact")]
    public float InteractRange = 10f;

    [Header("Movements")]
    public float WalkSpeed = 10f;
    public float RunSpeed = 20f;
    public float Gravity = 1f;
    public float JumpForce = 20f;
}
