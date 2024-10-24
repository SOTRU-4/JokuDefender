using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    private GameObject player;
    private int damage;
    private BaseEnemy enemy;
    private ScriptableObject stats;

    private void Start()
    {
        damage = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("TOIMII");
            collision.TryGetComponent(out BaseEnemy enemy);

            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
