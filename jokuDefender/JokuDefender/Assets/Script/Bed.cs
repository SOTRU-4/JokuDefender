using UnityEngine;

public class Bed : MonoBehaviour, ITakeDamage
{
    public int healthPoints;
    public HealthBar healthBar;
    public static Transform bedPosition { get; private set; }
    void Start()
    {
        bedPosition = gameObject.transform;
        healthBar.SetHealth(healthPoints);
        healthBar.maxHealth = healthPoints;
    }
    public void TakeDamage(int damage)
    {
        healthPoints -= damage;
        healthBar.SetHealth(healthPoints);
        if (healthPoints <= 0)
        {
            Destroy(gameObject);
        }
    }
}
