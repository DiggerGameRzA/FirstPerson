using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class DinoInfo
{
    public int id;
    public float healthPoint;
    public float sedatPoint;
    public Vector3 position;
    public bool escaped;

    public DinoInfo(int _id, float _hp, float _sp, Vector3 _pos, bool _esc)
    {
        id = _id;
        healthPoint = _hp;
        sedatPoint = _sp;
        position = _pos;
        escaped = _esc;
    }
}
