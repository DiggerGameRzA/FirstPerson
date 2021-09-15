using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T:MonoBehaviour
{
    private static T _instance;
    public static T instance
    {
        get
        {
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = FindObjectOfType<T>();
        }
        else if (_instance != FindObjectOfType<T>())
        {
            Destroy(FindObjectOfType<T>());
        }

        DontDestroyOnLoad(FindObjectOfType<T>());

        
    }
}
