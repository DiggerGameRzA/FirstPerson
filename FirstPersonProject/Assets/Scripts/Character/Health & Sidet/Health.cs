using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour, IHealth
{
    UIManager uiManager;
    [SerializeField] float healthPoint;
    [SerializeField] float maxHealthPoint;

    RectTransform HPBar;

    public float HealthPoint
    {
        get { return healthPoint; }
        set { healthPoint = value; }
    }
    public float MaxHealthPoint
    {
        get { return maxHealthPoint; }
        set { maxHealthPoint = value; }
    }
    private void Start()
    {
        if (GetComponent<Player>())
        {
            uiManager = FindObjectOfType<UIManager>();
        }
        else
        {
            HPBar = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<RectTransform>();
        }
    }
    public void TakeDamage(float damage)
    {
        HealthPoint -= damage;
        if(HealthPoint <= 0)
        {
            OnDead();
        }

        if (GetComponent<Player>())
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
        if(HealthPoint + heal > MaxHealthPoint)
        {
            heal = MaxHealthPoint - HealthPoint;
        }
        HealthPoint += heal;
        print("healing : " + heal);
        if (GetComponent<Player>())
        {
            uiManager.UpdateHealth(HealthPoint);
        }
    }
    public void OnDead()
    {
        print(this.name + " die");
    }
}
