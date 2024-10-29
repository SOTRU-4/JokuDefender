using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class BaseEnemy : MonoBehaviour, ITakeDamage
{
    public EnemyStats stats;
    Transform target;
    [HideInInspector] public NavMeshAgent agent;
    SpriteRenderer sprite;
    Behavior behavior;
    float delay = 1;

    [Header("Stats")]
    public int healthPoints;
    public int damage;
    public float speed;

    private void Awake()
    {
        Init();
    }
    void Update()
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
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        this.damage = stats.damage;
        this.speed = stats.speed;
        this.healthPoints = stats.healthPoints;

        agent.speed = speed;

        behavior = new Behavior(this);
    }
    public void TakeDamage(int damage)
    {
        stats.healthPoints -= damage;
        if (stats.healthPoints <= 0)
        {
            Destroy(gameObject);
            Debug.Log(stats.name + " get hit!");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out ITakeDamage target) && collision.gameObject.tag != "Enemy" && delay <= 0)
        {
            target.TakeDamage(stats.damage);
            delay = 1;
        }

    }

}
