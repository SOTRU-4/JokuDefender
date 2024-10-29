using System;
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
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        sprite = GetComponent<SpriteRenderer>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        agent.speed = stats.speed;

        behavior = new Behavior(this);

    }
    void Update()
    {
        MoveToTarget();

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

    public void TakeDamage(int damage)
    {
        stats.healthPoints -= damage;
        Debug.Log(stats.healthPoints);
        if(stats.healthPoints <= 0)
        {
            Destroy(gameObject);
            Debug.Log(stats.name + " get hit!");
        }
    }

}
