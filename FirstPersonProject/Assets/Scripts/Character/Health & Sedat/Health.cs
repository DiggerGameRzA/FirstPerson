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
        
        if (isPlayer)
        {
            uiManager.UpdateHealth(HealthPoint);
        }
        else
        {
            UpdateHealth(HealthPoint);

            GetComponent<EnemyStats>().isHit = true;
            /*
            if (GetComponent<UtahRaptor>())
            {
                GetComponent<UtahRaptor>().isHit = true;
            }
            else if(GetComponent<Compy>())
            {
                GetComponent<Compy>().isHit = true;
            }
            */
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
    public void UpdateHealth(float hp)
    {
        HPBar.sizeDelta = new Vector2((hp / MaxHealthPoint) * 100, 100);
    }
    public void OnDead()
    {
        if (!isPlayer)
        {
            if (FindObjectOfType<WaveSystem>() != null)
            {
                FindObjectOfType<WaveSystem>().DecreaseEnemy();
            }
        }
        else
        {

            FindObjectOfType<Player>().enabled = false;
            UIManager.instance.ShowInventory(false);
            UIManager.instance.ShowMenu(false);
            UIManager.instance.ShowGameOver(true);
            InputManager.instance.enabled = false;
        }
    }
}
