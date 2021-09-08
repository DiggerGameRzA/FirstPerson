using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager2 : MonoBehaviour
{
    #region Singleton
    //public static PlayerManager instance;

    private void Awake()
    {
        //instance = this;
    }
    #endregion

    public GameObject player;
}