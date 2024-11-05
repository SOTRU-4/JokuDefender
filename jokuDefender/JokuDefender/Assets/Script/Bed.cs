using UnityEngine;

public class Bed : MonoBehaviour, ITakeDamage
{
    public int healthPoints = 20;
    public static Transform bedPosition { get; private set; }
    void Start()
    {
        bedPosition = gameObject.transform;
    }
    public void TakeDamage(int damage)
    {
        healthPoints -= damage;
        if (healthPoints <= 0)
        {
            Destroy(gameObject);
        }
    }
}
