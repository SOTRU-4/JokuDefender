using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Building, ITakeDamage
{
    public HealthBar healthBar;
    AudioSource audioSource;
    [SerializeField] TowerCrossbow crossbow;
    [SerializeField] AudioClip hitSound;
    [SerializeField] AudioClip destroyedSound;
    private void Awake()
    {
        currentLevel = Instantiate(currentLevel);
        audioSource = GetComponent<AudioSource>();
        maxHealth = health;
        healthBar.SetHealth(maxHealth, health);
        currentLevel.tower = this;
        currentLevel.crossbow = crossbow;
        currentLevel.Init();
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        audioSource.clip = hitSound;
        audioSource.Play();
        healthBar.SetHealth(maxHealth, health);
        if(health <= 0)
        {
            audioSource.clip = destroyedSound; audioSource.Play();
            Destroy(gameObject, 1);
        }
    }
}
