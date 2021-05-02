using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] public static new UnityEngine.Camera camera;
    [SerializeField] GameObject FPSCamera;
    void Start()
    {
        camera = UnityEngine.Camera.main;
        FPSCamera = GameObject.Find("FPS Camera");
        HideCursor();
    }
    public static Vector3 GetCameraForwardDirectionNormalized()
    {
        if(camera == null)
        {
            return Vector3.zero;
        }
        Vector3 forward = camera.transform.forward;
        forward.y = 0;
        return forward.normalized;
    }
    public static Vector3 GetCameraRightDirectionNormalized()
    {
        if (camera == null)
        {
            return Vector3.zero;
        }
        Vector3 right = camera.transform.right;
        right.y = 0;
        return right.normalized;
    }
    /*
    public static Quaternion GetCameraRotation()
    {
        return camera.transform.rotation;
    }
    */
    public static Quaternion GetCameraRotationY()
    {
        if (camera == null)
        {
            return Quaternion.identity;
        }
        Quaternion yDir = camera.transform.rotation;
        yDir.x = 0;
        yDir.z = 0;
        return yDir;
    }
    public static RaycastHit GetCameraRaycast(float range)
    {
        RaycastHit hit;
        Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, range);
        return hit;
    }
    public void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void CanLookAround()
    {
        FPSCamera.SetActive(true);
    }
    public void CanNotLookAround()
    {
        FPSCamera.SetActive(false);
    }
}
