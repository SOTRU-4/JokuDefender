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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        speedx = Input.GetAxisRaw("Horizontal") * movespeed;
        speedy = Input.GetAxisRaw("Vertical") * movespeed;
        if (speedx < 0)
        {
            sprite.flipX = true;
        }
        else if (speedx > 0)
        {
            sprite.flipX = false;
        }

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
}
