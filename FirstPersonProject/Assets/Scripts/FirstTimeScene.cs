using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class FirstTimeScene
{
    public string scene;
    public bool firstTime;
    public FirstTimeScene(string _scene, bool _firstTime)
    {
        scene = _scene;
        firstTime = _firstTime;
    }
}
