using UnityEngine;
using UnityEngine.Events;

public class EnemyBehavior
{
    BaseEnemy enemy;
    public Transform target {  get; private set; }

    public UnityEvent changeTarget;
    MainTarget mainTarget;
    public EnemyBehavior(BaseEnemy enemy)
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
                if (enemy.isPlayerNearbye || Bed.bedPosition == null)
                {
                    target = PlayerController.instance.transform;
                }
                
                break;

            case MainTarget.Player:
                target = PlayerController.instance.transform;

                if (PlayerController.instance.transform == null)
                {
                    target = Bed.bedPosition;
                }
                break;

                case MainTarget.Buildings:
                    break;
            }
    }

    
}
