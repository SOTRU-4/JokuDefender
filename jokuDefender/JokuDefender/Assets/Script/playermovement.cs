using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class playermovement : MonoBehaviour
{
    public float movespeed;
    float speedx, speedy;
    Rigidbody2D rb;

    private Animator animator;
    private SpriteRenderer sprite;

    private float cooldown = 1;
    private float lastusedtime;

    public GameObject weapononhand;
    private float angle;
    private SpriteRenderer weaponsprite;
    public GameObject weaponprefab;
    private Vector3 weaponposition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        weaponsprite = weapononhand.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPoint.z = -0.1f;
        Vector2 lookdir = worldPoint - weapononhand.transform.position;

        angle = Mathf.Atan2(lookdir.y, lookdir.x) * Mathf.Rad2Deg;
        if (angle > 90 || angle < -90)
        {
            sprite.flipX = true;
            weaponsprite.flipY = true;
            weaponposition = new Vector3(0.14445f, -0.3f, -0.1f);
            weaponsprite.transform.localPosition = weaponposition;
        }
        else
        {  
            sprite.flipX = false;
            weaponsprite.flipY = false;
            weaponposition = new Vector3(-0.14445f, -0.3f, -0.1f);
            weaponsprite.transform.localPosition = weaponposition;
        }
        weapononhand.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (Time.time > lastusedtime + cooldown)
        {
            weapononhand.SetActive(true);
            if (Input.GetMouseButtonDown(0) && Time.time > lastusedtime + cooldown)
            {
                lastusedtime = Time.time;
                weapononhand.SetActive(false);
                attack(lookdir);
            }
        }

        speedx = Input.GetAxisRaw("Horizontal") * movespeed;
        speedy = Input.GetAxisRaw("Vertical") * movespeed;

        if (speedx != 0 || speedy != 0)
        {
            animator.SetBool("ismoving", true);
        }
        else
        {
            animator.SetBool("ismoving", false);
        }
        rb.velocity = new Vector2(speedx, speedy);
    }

    void attack(Vector2 target)
    {
        Debug.Log(target);
        GameObject bullet = Instantiate(weaponprefab, weaponposition + transform.position, Quaternion.Euler(new Vector3(0, 0, angle - 90)));
        Rigidbody2D bulletrb = bullet.GetComponent<Rigidbody2D>();
        bulletrb.velocity = target;
    }
}
