using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    [SerializeField] public static new UnityEngine.Camera camera;
    [SerializeField] GameObject FPSCamera;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        //DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        camera = UnityEngine.Camera.main;
        FPSCamera = GameObject.Find("FPS Camera");
        ShowCursor(false);
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
    public static RaycastHit GetCameraRaycast(float range, LayerMask layer)
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        RaycastHit hit;
        Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, range, layer);
        return hit;
    }
    public void ShowCursor(bool show)
    {
        if (show)
            Cursor.lockState = CursorLockMode.None;
        else if (!show)
            Cursor.lockState = CursorLockMode.Locked;

        Cursor.visible = show;
    }
    public void CanLookAround(bool can)
    {
        FPSCamera.SetActive(can);
    }
    public void Restart()
    {
        camera = UnityEngine.Camera.main;
        FPSCamera = GameObject.Find("FPS Camera");
        ShowCursor(false);
    }
}
