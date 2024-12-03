using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : Building, ITakeDamage
{
    [SerializeField] HealthBar healthBar;
    Animator animator;
    [SerializeField] AudioClip destroyedSound;
    [SerializeField] AudioClip hitSound;
    AudioSource audioSource;
    void Awake()
    {
        animator = GetComponent<Animator>();
        health = maxHealth;
        healthBar.SetHealth(maxHealth, health);
        audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(int damage)
    {
        animator.SetTrigger("Hit");
        audioSource.clip = hitSound;
        audioSource.Play();
        health -= damage;
        healthBar.SetHealth(maxHealth, health);
        if (health <= 0)
        {
            animator.SetTrigger("Destroyed");
            audioSource.clip = destroyedSound;
            audioSource.Play();
            Destroy(gameObject, 5);
        }
    }
}
