using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour, ITakeDamage
{
    public HealthBar healthBar;
    public int health;
    private int maxHealth;
    private void Awake()
    {
        maxHealth = health;
        healthBar.SetHealth(maxHealth, health);
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.SetHealth(maxHealth, health);
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
