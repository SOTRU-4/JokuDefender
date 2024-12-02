using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class BaseEnemy : MonoBehaviour, ITakeDamage
{
    public EnemyStats stats;
    [SerializeField] HealthBar healthBar;
    Transform target;
    [HideInInspector] public NavMeshAgent agent;
    SpriteRenderer sprite;
    [HideInInspector] public EnemyBehavior behavior {  get; private set; }
    float delay = 1;
    [HideInInspector]public bool isPlayerNearbye = false;
    Animator animator;

    AudioSource audioSource;
    [SerializeField] AudioClip deadSound;

    [Header("Stats")]
    public int healthPoints;
    public int damage;
    public float speed;
    private int maxHealth;
    private void Awake()
    {
        Init();
    }
    void FixedUpdate()
    {
        MoveToTarget();
        delay -= Time.deltaTime;
    }
    
    private void MoveToTarget()
    {

        behavior.SetTarget();
        agent.SetDestination(behavior.target.position);

        if (behavior.target.position.x < gameObject.transform.position.x)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }
    }

    private void Init()
    {
        agent = GetComponent<NavMeshAgent>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;

        this.damage = stats.damage;
        this.speed = stats.speed;
        this.healthPoints = stats.healthPoints;

        agent.speed = speed;

        behavior = new EnemyBehavior(this);

        maxHealth = healthPoints;
        healthBar.SetHealth(maxHealth, healthPoints);
    }
    public void TakeDamage(int damage)
    {
        healthPoints -= damage;
        healthBar.SetHealth(maxHealth, healthPoints);
        audioSource.pitch = Random.Range(0.85f, 1.15f);
        audioSource.Play();
        animator.SetTrigger("Hit");
        if (healthPoints <= 0)
        {
            PlayerController.instance.AddGold(stats.gold);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        GameObject audioPlayer = new GameObject("PersistentAudioPlayer");
        AudioSource audioSource = audioPlayer.AddComponent<AudioSource>();
        audioSource.clip = deadSound;
        audioSource.Play();
        Destroy(audioPlayer, 3);

        var particle = Resources.Load<GameObject>("bloodBlow");
        var particleObject = Instantiate(particle, transform.position, Quaternion.identity);
        Destroy(particleObject, 1f);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out ITakeDamage target) && collision.gameObject.tag != "Enemy" && delay <= 0)
        {
            target.TakeDamage(damage);
            delay = 1;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 4.5f);
    }
}
