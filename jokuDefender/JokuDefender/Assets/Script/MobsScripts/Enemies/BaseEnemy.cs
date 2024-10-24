using NavMeshPlus.Extensions;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour
{
    public EnemyStats stats;
    
    Transform target;
    [HideInInspector]public NavMeshAgent agent;
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

    }
}
