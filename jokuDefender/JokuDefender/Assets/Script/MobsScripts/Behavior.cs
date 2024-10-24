using System;
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
        if (target == null)
        {
            switch (mainTarget)
            {
                case MainTarget.Bed:
                    target = Bed.bedPosition;

                    break;

                case MainTarget.Player:
                    break;

                case MainTarget.Buildings:
                    break;
            }
        }
    }
}
