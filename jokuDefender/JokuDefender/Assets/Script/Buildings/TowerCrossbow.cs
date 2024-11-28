using System;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
public class TowerCrossbow : MonoBehaviour
{
    public float range;
    public float offset;
    public int speed;
    public Collider2D collision;
    bool targetSelected = false;
    float cooldown = 0.5f;
    public bool shoot;
    Animator anim;
    [SerializeField] GameObject arrow;
    float enemyPos;
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        cooldown -= Time.deltaTime;
        if (shoot && cooldown <= 0)
        {
            var newArrow = Instantiate(arrow, transform.position, Quaternion.Euler(new Vector3(0, 0, enemyPos)));
            cooldown = 0.5f;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Enemy")
        {
            if (collision == this.collision)
            {
                Vector2 direction = collision.transform.position - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + offset;
                var target = Quaternion.Euler(new Vector3(0, 0, angle));
                transform.rotation = Quaternion.RotateTowards(transform.rotation, target, speed);
                anim.SetBool("Shooting", targetSelected);

                enemyPos = angle;
                
            }
            else if (targetSelected == false && collision != null)
            {
                targetSelected = true;
                this.collision = collision;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision == this.collision)
        {
            targetSelected = false;
            this.collision = null;
            anim.SetBool("Shooting", targetSelected);
        }
    }
}
