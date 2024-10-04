using NavMeshPlus.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour
{
    [SerializeField] EnemyStats stats;

    Transform target;
    private NavMeshAgent agent;
    SpriteRenderer sprite;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        sprite = GetComponent<SpriteRenderer>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    void Update()
    {
        SelectTarget();

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

    private void SelectTarget()
    {
        switch (stats.mainTarget)
        {
            case MainTarget.Bed:
                break;

            case MainTarget.Player:
                break;

            case MainTarget.Buildings:
                break;
        }


    }
}
