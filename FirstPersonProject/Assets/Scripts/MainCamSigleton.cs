using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamSigleton : MonoBehaviour
{
    public static MainCamSigleton instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            print("Destroy myself");
            Destroy(this.gameObject);
        }
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
