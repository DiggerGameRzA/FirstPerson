using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementDir : IMovementDir
{
    public Vector3 GetMovementDir()
    {
        Vector3 forward = CameraManager.GetCameraForwardDirectionNormalized();
        Vector3 right = CameraManager.GetCameraRightDirectionNormalized();
        return forward * InputManager.GetVerInput() + right * InputManager.GetHorInput();
    }
}
