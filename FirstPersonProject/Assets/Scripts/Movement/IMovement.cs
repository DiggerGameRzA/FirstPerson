using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovement
{
    void Walk(Vector3 direction, float speed);
    void RotateBody(Quaternion direction);
    //void RotateHand(Quaternion direction);
}
