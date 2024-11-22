using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : MonoBehaviour, ITakeDamage
{
    [SerializeField] HealthBar healthBar;
    Animator animator;
    int maxHealth = 50;
    int currentHealth;
    void Awake()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthBar.SetHealth(maxHealth, currentHealth);
    }

    public void TakeDamage(int damage)
    {
        animator.SetTrigger("Hit");
        currentHealth -= damage;
        healthBar.SetHealth(maxHealth, currentHealth);
        if (currentHealth <= 0)
        {
            animator.SetTrigger("Destroyed");
            Destroy(gameObject, 5);
        }
    }
}
