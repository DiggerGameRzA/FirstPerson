﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayer
{
    Stats GetStats();
    Rigidbody GetRigidbody();
    Transform GetTransform();
    Transform GetHandTransform();
}
