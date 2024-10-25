using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour, ITakeDamage
{
    public int healthPoints = 20;
    public static Transform bedPosition {  get; private set; }
    void Start()
    {
        bedPosition = gameObject.transform;
    }

    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        healthPoints -= damage;
        if (healthPoints <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out BaseEnemy enemy))
        {
            TakeDamage(enemy.stats.damage);
        }
    }
}
