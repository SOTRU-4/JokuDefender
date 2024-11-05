using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class WeaponScript : MonoBehaviour
{
    private GameObject player;
    public int damage;
    private BaseEnemy enemy;
    private ScriptableObject stats;
    private Rigidbody2D rb;
    private float speed;

    private void Start()
    {
        speed = UnityEngine.Random.Range(16, 20);
        rb = GetComponent<Rigidbody2D>();
        if (gameObject.name == "SlashPrefab(Clone)")
        {
            Destroy(gameObject, 0.1f);
        }
        else
        {
            Destroy(gameObject, 15);
        }
    }
    private void FixedUpdate()
    {
        if (gameObject.name != "SlashPrefab(Clone)")
        {
            rb.MovePosition(rb.position + (Vector2)transform.up * Time.deltaTime * speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("TOIMII");
            collision.TryGetComponent(out BaseEnemy enemy);

            enemy.TakeDamage(damage);
            if (gameObject.name != "SlashPrefab(Clone)")
            {
                Destroy(gameObject);
            }
        }
    }
}