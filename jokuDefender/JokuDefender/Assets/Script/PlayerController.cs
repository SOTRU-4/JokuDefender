using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    float speedX, speedY;
    Rigidbody2D rb;

    private Animator Animator;
    private SpriteRenderer sprite;

    public float Cooldown;
    private float lastusedtime;
    private float angle;

    public GameObject weapononhand;
    private SpriteRenderer CurrentWeaponSprite;
    private GameObject CurrentWeaponPrefab;
    private Vector3 weaponposition;
    public GameObject Slashprefab;

    public static PlayerController instance;
    private Weapon CurrentWeapon;

    private enum Weapon
    {
        Shovel,
        Scythe,
        Pitchfork,
        Flintlock,
        Shotgun,
        Machinegun
    }

    public void Awake()
    {
        instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        CurrentWeaponSprite = weapononhand.GetComponent<SpriteRenderer>();
        SetWeapon(Weapon.Pitchfork);
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
            CurrentWeaponSprite.flipY = true;
            weaponposition = new Vector3(0.14445f, -0.3f, -0.1f);
            CurrentWeaponSprite.transform.localPosition = weaponposition;
        }
        else
        {  
            sprite.flipX = false;
            CurrentWeaponSprite.flipY = false;
            weaponposition = new Vector3(-0.14445f, -0.3f, -0.1f);
            CurrentWeaponSprite.transform.localPosition = weaponposition;
        }
        weapononhand.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (Time.time > lastusedtime + Cooldown)
        {
            weapononhand.SetActive(true);
            if (Input.GetMouseButtonDown(0) && Time.time > lastusedtime + Cooldown)
            {
                lastusedtime = Time.time;
                Attack(lookdir);
            }
        }

        speedX = Input.GetAxisRaw("Horizontal") * moveSpeed;
        speedY = Input.GetAxisRaw("Vertical") * moveSpeed;

        if (speedX != 0 || speedY != 0)
        {
            Animator.SetBool("ismoving", true);
        }
        else
        {
            Animator.SetBool("ismoving", false);
        }
        rb.velocity = new Vector2(speedX, speedY);
    }
    
    private void SetWeapon(Weapon weapon)
    {
        CurrentWeapon = weapon;
        Debug.Log(weapon);
        CurrentWeaponSprite.sprite = System.Array.Find(Resources.LoadAll<Sprite>("Props"), sprite => sprite.name == weapon.ToString() + "Onhand");
        CurrentWeaponPrefab = Resources.Load<GameObject>(weapon.ToString() + "Prefab");
    }
    
    void Attack(Vector3 target)
    {
        if (CurrentWeapon == Weapon.Shovel)
        {
            GameObject bullet = Instantiate(Slashprefab, weaponposition + transform.position, Quaternion.Euler(new Vector3(0, 0, angle)));
            if (angle > 90 || angle < -90)
            {
                bullet.GetComponent<SpriteRenderer>().flipY = true;
            }
            Rigidbody2D bulletrb = bullet.GetComponent<Rigidbody2D>();
            bulletrb.transform.position = transform.position + target.normalized * 1f;
            bulletrb.velocity = target.normalized * 5;
        }
        else if (CurrentWeapon == Weapon.Scythe)
        {

        }
        else if (CurrentWeapon == Weapon.Pitchfork)
        {
            weapononhand.SetActive(false);

            GameObject bullet = Instantiate(CurrentWeaponPrefab, weaponposition + transform.position, Quaternion.Euler(new Vector3(0, 0, angle - 90)));
            Rigidbody2D bulletrb = bullet.GetComponent<Rigidbody2D>();
            bulletrb.velocity = target.normalized * 5;
        }
    }
}
