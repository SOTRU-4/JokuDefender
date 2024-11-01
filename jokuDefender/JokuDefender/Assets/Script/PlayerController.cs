using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.U2D;

public class PlayerController : MonoBehaviour, ITakeDamage
{
    public float moveSpeed;
    public int healthPoints = 20;
    float speedX, speedY;
    Rigidbody2D rb;

    private Animator Animator;
    private SpriteRenderer sprite;

    private float CurrentCooldown;

    private Dictionary<string, float> cooldowns = new Dictionary<string, float>();
    private float lastusedtime;
    private float angle;

    public GameObject weapononhand;
    private SpriteRenderer CurrentWeaponSprite;
    private GameObject CurrentWeaponPrefab;
    private Vector3 weaponposition;
    public GameObject Slashprefab;

    public static PlayerController instance;
    private Weapon CurrentWeapon;

    public enum Weapon
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

        cooldowns["Shovel"] = 1;
        cooldowns["Scythe"] = 0.65f;
        cooldowns["Pitchfork"] = 0.8f;
        cooldowns["Flintlock"] = 1.2f;
        cooldowns["Shotgun"] = 1.5f;
        cooldowns["Machinegun"] = 0.15f;

        SetWeapon(Weapon.Shovel);
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

        if (Time.time > lastusedtime + CurrentCooldown)
        {
            weapononhand.SetActive(true);
            if (Input.GetMouseButton(0) && Time.time > lastusedtime + CurrentCooldown)
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

    public void TakeDamage(int damage)
    {
        healthPoints -= damage;
        if (healthPoints <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetWeapon(Weapon weapon)
    {
        CurrentCooldown = cooldowns[weapon.ToString()];
        Debug.Log(cooldowns[weapon.ToString()]);

        CurrentWeapon = weapon;
        CurrentWeaponSprite.sprite = System.Array.Find(Resources.LoadAll<Sprite>("Props"), sprite => sprite.name == weapon.ToString() + "Onhand");

        if (weapon == Weapon.Pitchfork)
        {
            CurrentWeaponPrefab = Resources.Load<GameObject>(weapon.ToString() + "Prefab");
        }
        else
        {
            CurrentWeaponPrefab = Resources.Load<GameObject>("BulletPrefab");
        }
    }
    
    void Attack(Vector3 target)
    {
        if (CurrentWeapon == Weapon.Shovel || CurrentWeapon == Weapon.Scythe)
        {
            GameObject bullet = Instantiate(Slashprefab, weaponposition + transform.position, Quaternion.Euler(new Vector3(0, 0, angle)));
            if (angle > 90 || angle < -90)
            {
                bullet.GetComponent<SpriteRenderer>().flipY = true;
            }
            Rigidbody2D bulletrb = bullet.GetComponent<Rigidbody2D>();
            bulletrb.transform.position = weapononhand.transform.position + target.normalized * 1f;
            bulletrb.velocity = target.normalized * 5;
            bullet.GetComponent<WeaponScript>().damage = 2;
        }
        else if (CurrentWeapon == Weapon.Pitchfork)
        {
            weapononhand.SetActive(false);
            float spread = UnityEngine.Random.Range(-5,5);

            GameObject bullet = Instantiate(CurrentWeaponPrefab, weaponposition + transform.position, Quaternion.Euler(new Vector3(0, 0, angle - 90 + spread)));
            Rigidbody2D bulletrb = bullet.GetComponent<Rigidbody2D>();
            bulletrb.velocity = target.normalized * 5;
            bullet.GetComponent<WeaponScript>().damage = 4;
        }
        else if (CurrentWeapon == Weapon.Flintlock)
        {
            float spread = UnityEngine.Random.Range(-3, 3);
            GameObject bullet = Instantiate(CurrentWeaponPrefab, weaponposition + transform.position, Quaternion.Euler(new Vector3(0, 0, angle - 90 + spread)));
            bullet.transform.position = weapononhand.transform.position + target.normalized * 0.6f;
            bullet.GetComponent<WeaponScript>().damage = 5;
        }
        else if (CurrentWeapon == Weapon.Shotgun)
        {
            for (int i = 0; i < 4; i++)
            {
                float spread = UnityEngine.Random.Range(-20, 20);
                GameObject bullet = Instantiate(CurrentWeaponPrefab, weaponposition + transform.position, Quaternion.Euler(new Vector3(0, 0, angle - 90 + spread)));
                bullet.transform.position = weapononhand.transform.position + target.normalized * 0.6f;
                bullet.GetComponent<WeaponScript>().damage = 3;
            }
        }
        else if (CurrentWeapon == Weapon.Machinegun)
        {
            float spread = UnityEngine.Random.Range(-8, 8);
            GameObject bullet = Instantiate(CurrentWeaponPrefab, weaponposition + transform.position, Quaternion.Euler(new Vector3(0, 0, angle - 90 + spread)));
            bullet.transform.position = weapononhand.transform.position + target.normalized * 0.6f;
            bullet.GetComponent<WeaponScript>().damage = 3;
        }
    }
}
