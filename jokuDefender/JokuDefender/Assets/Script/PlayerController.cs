using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static PlayerController;

public class PlayerController : MonoBehaviour, ITakeDamage
{
    [HideInInspector]
    public float moveSpeed = 6;
    public int HealthPoints = 20;
    public int MaxHealth = 20;
    public int Armor = 0;
    public UnityEngine.UI.Slider HealthBar;
    public TextMeshProUGUI HealthText;
    float speedX, speedY;

    Rigidbody2D rb;
    private Animator Animator;
    private SpriteRenderer sprite;

    public int PlayerGold;
    public TextMeshProUGUI GoldText;
    public TextMeshProUGUI GoldIncrease;

    private float CurrentCooldown;

    private Dictionary<string, float> cooldowns = new Dictionary<string, float>(); //why
    private float lastusedtime;
    private float angle;

    Vector3 WeaponOnHandOrigin;
    public GameObject WeaponOnHand;
    public SpriteRenderer CurrentWeaponSprite;
    private GameObject CurrentWeaponPrefab;
    private Vector3 weaponposition;

    public int[] WeaponDamages;

    public GameObject Slashprefab;
    public GameObject flash;
    public GameObject barrel;

    public static PlayerController instance;
    private Weapon CurrentWeapon;

    private PlayerSpawner Spawner;

    // 0 = attack speed, 1 = health, 2 = speed, 3 = armor
    public int[] UpgradeLevels;

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
        CurrentWeaponSprite = WeaponOnHand.GetComponent<SpriteRenderer>();
        WeaponOnHandOrigin = WeaponOnHand.transform.position;
        Spawner = GameObject.Find("PlayerSpawn").GetComponent<PlayerSpawner>();
        
        //why
        cooldowns["Shovel"] = 1;
        cooldowns["Scythe"] = 0.65f;
        cooldowns["Pitchfork"] = 0.8f;
        cooldowns["Flintlock"] = 0.9f;
        cooldowns["Shotgun"] = 1.5f;
        cooldowns["Machinegun"] = 0.1f;

        //updating player gui at startup
        HealthBar.value = HealthPoints;
        HealthText.text = HealthPoints.ToString();
        GoldText.text = "Gold " + PlayerGold;

        //setting shovel as the starting weapon
        SetWeapon(Weapon.Shovel);
    }

    void Update()
    {
        //turning the held weapon to the mouse
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPoint.z = -0.1f;
        Vector2 lookdir = worldPoint - WeaponOnHand.transform.position;

        angle = Mathf.Atan2(lookdir.y, lookdir.x) * Mathf.Rad2Deg;
        if (angle > 90 || angle < -90)
        {
            sprite.flipX = true;
            CurrentWeaponSprite.flipY = true;
            weaponposition = new Vector3(0.14445f, -0.3f, -0.1f);
            CurrentWeaponSprite.transform.localPosition = weaponposition;
            flipBarrel(false);
        }
        else
        {  
            sprite.flipX = false;
            CurrentWeaponSprite.flipY = false;
            weaponposition = new Vector3(-0.14445f, -0.3f, -0.1f);
            CurrentWeaponSprite.transform.localPosition = weaponposition;
            flipBarrel(true);
        }
        WeaponOnHand.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        //attack if cooldown is 0
        if (Time.time > lastusedtime + CurrentCooldown)
        {
            WeaponOnHand.SetActive(true);
            if (Input.GetMouseButton(0) && Time.time > lastusedtime + CurrentCooldown)
            {
                lastusedtime = Time.time;
                Attack(lookdir);
            }
        }

        //player movement
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

    private void flipBarrel(bool faceRight)
    {
        Vector3 localPosition = barrel.transform.localPosition;
        localPosition.y = Mathf.Abs(localPosition.y) * (faceRight ? 1 : -1);
        barrel.transform.localPosition = localPosition;
    }

    public void UpdateUpgrades()
    {
        CurrentCooldown = cooldowns[CurrentWeapon.ToString()] * (1 - UpgradeLevels[0] * 0.1f);
        MaxHealth = 20 + UpgradeLevels[1] * 5;
        HealthBar.maxValue = MaxHealth;
        moveSpeed = 6 + UpgradeLevels[2];
        Armor = UpgradeLevels[3];
    }

    public void AddGold(int Gold)
    {
        PlayerGold += Gold;
        GoldText.text = "Gold " + PlayerGold;

        TextMeshProUGUI Moneyprefab = Instantiate(GoldIncrease);

        var count = Mathf.FloorToInt(Mathf.Log10(PlayerGold)) + 1;

        Moneyprefab.transform.SetParent(GoldText.transform);
        Moneyprefab.transform.localPosition = new Vector2(50 + 8 * count,0);
        if (Gold < 0)
        {
            Moneyprefab.text = Gold.ToString();
        }
        else
        {
            Moneyprefab.text = "+" + Gold;
        }
    }

    public void TakeDamage(int damage)
    {
        int armorRoll = UnityEngine.Random.Range(0, 100);
        
        //each armor point gives a 15% chance of negating damage
        if (armorRoll > Armor * 15)
        {
            HealthPoints -= damage;
            HealthBar.value = HealthPoints;
            HealthText.text = HealthPoints.ToString();
        }
        
        //disable player if health is < 0 and respawn in 5 seconds
        if (HealthPoints <= 0)
        {
            Spawner.SpawnCheck();
        }
    }

    private IEnumerator FadeWeapon()
    {
        SpriteRenderer spriteRenderer = WeaponOnHand.GetComponent<SpriteRenderer>();

        Color color = spriteRenderer.materials[0].color;

        while (color.a > 0)
        {
            color.a -= 0.4f;
            spriteRenderer.materials[0].color = color;
            
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitUntil(() => spriteRenderer.materials[0].color.a <= 0f);
        yield return new WaitForSeconds(0.1f * CurrentCooldown);

        while (color.a < 1)
        {
            color.a += 0.4f;
            spriteRenderer.materials[0].color = color;

            yield return new WaitForEndOfFrame();
        }
    }

    public void SetWeapon(Weapon weapon)
    {
        Debug.Log(cooldowns[weapon.ToString()]);

        CurrentWeapon = weapon;
        UpdateUpgrades();

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
    
    //spaget
    void Attack(Vector3 target)
    {
        if (CurrentWeapon == Weapon.Shovel || CurrentWeapon == Weapon.Scythe)
        {
            StartCoroutine(FadeWeapon());

            GameObject bullet = Instantiate(Slashprefab, weaponposition + transform.position, Quaternion.Euler(new Vector3(0, 0, angle)));
            if (angle > 90 || angle < -90)
            {
                bullet.GetComponent<SpriteRenderer>().flipY = true;
            }

            Rigidbody2D bulletrb = bullet.GetComponent<Rigidbody2D>();

            bulletrb.transform.position = WeaponOnHand.transform.position + target.normalized * 3f;
            bulletrb.velocity = target.normalized * 5;

            //scythe and shovel will do the same damage no matter what because of how i coded it cant be bothered to fix
            bullet.GetComponent<WeaponScript>().damage = WeaponDamages[0];
        }

        else if (CurrentWeapon == Weapon.Pitchfork)
        {
            StartCoroutine(FadeWeapon());

            float spread = UnityEngine.Random.Range(-4,4);

            GameObject bullet = Instantiate(CurrentWeaponPrefab, weaponposition + transform.position, Quaternion.Euler(new Vector3(0, 0, angle - 90 + spread)));

            bullet.GetComponent<WeaponScript>().damage = WeaponDamages[1];
        }

        else if (CurrentWeapon == Weapon.Flintlock)
        {
            float spread = UnityEngine.Random.Range(-2, 2);
            GameObject bullet = Instantiate(CurrentWeaponPrefab, barrel.transform.position + target.normalized * 0.5f, Quaternion.Euler(new Vector3(0, 0, angle - 90 + spread)));
            Instantiate(flash, barrel.transform.position + target.normalized * 0.5f, Quaternion.Euler(new Vector3(0, 0, angle + spread)));

            bullet.GetComponent<WeaponScript>().damage = WeaponDamages[2];
        }

        else if (CurrentWeapon == Weapon.Shotgun)
        {
            for (int i = 0; i < 6; i++)
            {
                float spread = UnityEngine.Random.Range(-20, 20);
                GameObject bullet = Instantiate(CurrentWeaponPrefab, barrel.transform.position + target.normalized * 0.5f, Quaternion.Euler(new Vector3(0, 0, angle - 90 + spread)));
                Instantiate(flash, barrel.transform.position + target.normalized * 0.5f, Quaternion.Euler(new Vector3(0, 0, angle + spread)));

                bullet.GetComponent<WeaponScript>().damage = WeaponDamages[3];
            }
        }

        else if (CurrentWeapon == Weapon.Machinegun)
        {
            float spread = UnityEngine.Random.Range(-7, 7);

            GameObject bullet = Instantiate(CurrentWeaponPrefab, barrel.transform.position + target.normalized * 0.5f, Quaternion.Euler(new Vector3(0, 0, angle - 90 + spread)));
            Instantiate(flash, barrel.transform.position + target.normalized * 0.5f, Quaternion.Euler(new Vector3(0, 0, angle + spread)));

            bullet.GetComponent<WeaponScript>().damage = WeaponDamages[4];
        }
    }
}