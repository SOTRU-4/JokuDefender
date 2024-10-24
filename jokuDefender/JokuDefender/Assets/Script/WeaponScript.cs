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
        damage = 3;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OSU");
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("TOIMII");
            //en tiiä miten statít oikeen toimii koitin saada toimimaan joku tunnin mut en tajunnu
            Destroy(gameObject);
        }
    }
}
