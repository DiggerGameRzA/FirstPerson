using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovement
{
    void Walk(Vector3 direction);
    void Run(Vector3 direction);
    void Rotate(Vector3 direction);
}
