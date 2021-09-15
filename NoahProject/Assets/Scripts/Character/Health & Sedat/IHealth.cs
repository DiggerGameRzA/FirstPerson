using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
    float HealthPoint { get; set; }
    float MaxHealthPoint { get; set; }
    void TakeDamage(float damage);
    void TakeHeal(float heal);
    void UpdateHealth(float hp);
    void OnDead();
}
