using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour, IHealth
{
    [SerializeField] bool isPlayer = false;
    [SerializeField] float _healthPoint;
    [SerializeField] float _maxHealthPoint;

    [SerializeField] RectTransform HPBar = null;
    [SerializeField] UIManager uiManager;

    public float HealthPoint
    {
        get { return _healthPoint; }
        set { _healthPoint = value; }
    }
    public float MaxHealthPoint
    {
        get { return _maxHealthPoint; }
        set { _maxHealthPoint = value; }
    }
    private void Start()
    {
        if (isPlayer)
        {
            uiManager = UIManager.instance;
        }
        else
        {
            
        }
    }
    public void TakeDamage(float damage)
    {
        HealthPoint -= damage;
        if(HealthPoint <= 0)
        {
            OnDead();
        }

        if (isPlayer)
        {
            uiManager.UpdateHealth(HealthPoint);
        }
        else
        {
            HPBar.sizeDelta = new Vector2((HealthPoint/MaxHealthPoint) * 100, 100);
        }
    }
    public void TakeHeal(float heal)
    {
        //80 + 50 = 130 > 100
        //100 - 80 = 20
        //heal 20
        if(HealthPoint + heal > MaxHealthPoint)
        {
            heal = MaxHealthPoint - HealthPoint;
        }
        HealthPoint += heal;
        print("healing : " + heal);
        if (isPlayer)
        {
            uiManager.UpdateHealth(HealthPoint);
        }
    }
    public void OnDead()
    {
        if (!isPlayer)
        {
            GetComponent<Collider>().enabled = false;
        }
        else
        {
            GameObject FPSCam = GameObject.Find("FPS Camera");
            FPSCam.SetActive(false);
            FindObjectOfType<Player>().enabled = false;
            uiManager.ShowInventory(false);
            InputManager.instance.enabled = false;

        }
    }
}
