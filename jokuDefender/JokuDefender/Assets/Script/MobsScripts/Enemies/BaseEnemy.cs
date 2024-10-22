using NavMeshPlus.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour
{
    public EnemyStats stats;
    
    Transform target;
    private NavMeshAgent agent;
    SpriteRenderer sprite;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        sprite = GetComponent<SpriteRenderer>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        agent.speed = stats.speed;
    }
    void Update()
    {
        SelectTarget();
        
    }

    private void SelectTarget()
    {
        switch (stats.mainTarget)
        {
            case MainTarget.Bed:
                target = Bed.bedPosition;
                agent.SetDestination(target.position);
                break;

            case MainTarget.Player:
                break;

            case MainTarget.Buildings:
                break;
        }

        agent.SetDestination(target.position);
        if (target.position.x < gameObject.transform.position.x)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }
    }
}
