using System;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
public class TowerCrossbow : MonoBehaviour
{
    public float range;
    public float offset;
    public int speed;
    public Collider2D collision;
    bool targetSelected = false;
    float cooldown;
    public bool shoot;
    Animator anim;

    [SerializeField] GameObject arrow;
    [SerializeField] Tower tower;
    public float enemyPos;
    void Awake()
    {
        cooldown = tower.currentLevel.cooldown;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        cooldown -= Time.deltaTime;
        if (cooldown <= 0 && shoot)
        {
            tower.currentLevel.CrossbowShooting(enemyPos);
            cooldown = tower.currentLevel.cooldown;
        }

        /*cooldown -= Time.deltaTime;
        if (shoot && cooldown <= 0)
        {
            var newArrow = Instantiate(arrow, transform.position, Quaternion.Euler(new Vector3(0, 0, enemyPos)));
            cooldown = 0.5f;
        }*/
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (collision == this.collision)
            {
                tower.currentLevel.CrossbowRotate(collision);
            }
            else if (targetSelected == false && collision != null)
            {
                targetSelected = true;
                this.collision = collision;
            }
        }
        /*if (collision.gameObject.tag == "Enemy")
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
        }*/
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision == this.collision)
        {
            targetSelected = false;
            this.collision = null;
            anim.SetBool("Shooting", false);
        }
    }
}
