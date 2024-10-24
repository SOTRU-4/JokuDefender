using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Behavior
{
    BaseEnemy enemy;
    public Transform target {  get; private set; }

    public UnityEvent changeTarget;
    MainTarget mainTarget;
    public Behavior(BaseEnemy enemy)
    {
        this.enemy = enemy;

        mainTarget = enemy.stats.mainTarget;
    }

    public void SetTarget()
    {
        switch (mainTarget)
        {
            case MainTarget.Bed:
                target = Bed.bedPosition;

                Collider2D hit = Physics2D.OverlapCircle(enemy.transform.position, 3, 6);
                if (hit != null)
                {
                    target = hit.transform;
                }
                break;

            case MainTarget.Player:
                target = PlayerController.instance.transform;
                break;

                case MainTarget.Buildings:
                    break;
            }
    }
}
