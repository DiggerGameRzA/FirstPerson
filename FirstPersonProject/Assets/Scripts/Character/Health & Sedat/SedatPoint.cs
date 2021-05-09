using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SedatPoint : MonoBehaviour, ISedat
{
    [SerializeField] float _sedatPoint = 1;

    public float SedatPoints
    {
        get { return _sedatPoint; }
        set { _sedatPoint = value; }
    }
    public void TakeSedat(float sedat)
    {
        SedatPoints -= sedat;
        if(SedatPoints <= 0)
        {
            OnSedat();
        }
    }
    void OnSedat()
    {

    }
}
