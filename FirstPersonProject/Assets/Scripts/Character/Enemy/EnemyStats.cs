using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int id = 0;
    //HUD
    public GameObject textPrefab;

    //Vision
    public float visionRange;
    public float visionRange2;

    //Attack
    public float attackRange;
    public float attackDelay;
    public float damage;

    public float speed;

    //States
    public bool isDead = false;
    public bool isSleep = false;
    public bool isHit = false;
    public bool isInRange = false;
    public bool isInAtk = false;
    public bool isDied = false;

    public bool isEscaped = false;
    public bool isNest = false;

    //Sounds
    public AudioClip idleSound;
    public AudioClip runSound;
    public AudioClip attackSound;
    public AudioClip hitSound;
    public AudioClip sleepSound;
    public AudioClip deadSound;
}
