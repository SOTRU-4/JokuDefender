using UnityEngine;

public class PlayerDetecter : MonoBehaviour
{
    [SerializeField] BaseEnemy enemy;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out PlayerController player))
        {
            enemy.isPlayerNearbye = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController player))
        {
            enemy.isPlayerNearbye = false;
        }
    }
}
