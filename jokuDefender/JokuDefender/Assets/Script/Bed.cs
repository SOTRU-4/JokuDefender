using UnityEngine;

public class Bed : MonoBehaviour, ITakeDamage
{
    public int healthPoints;
    public HealthBar healthBar;
    private int maxHealth = 100;
    public static Transform bedPosition { get; private set; }
    void Start()
    {
        bedPosition = gameObject.transform;
        maxHealth = healthPoints;
        healthBar.SetHealth(maxHealth, healthPoints);
    }
    public void TakeDamage(int damage)
    {
        healthPoints -= damage;
        healthBar.SetHealth(maxHealth, healthPoints);
        if (healthPoints <= 0)
        {
            Destroy(gameObject);
        }
    }
}
