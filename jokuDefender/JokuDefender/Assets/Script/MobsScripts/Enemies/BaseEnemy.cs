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
    public bool isPlayerNearbye = false;
    Animator animator;

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
        animator.SetTrigger("Hit");
        if (healthPoints <= 0)
        {
            PlayerController.instance.AddGold(stats.gold);
            Destroy(gameObject);
        }
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
