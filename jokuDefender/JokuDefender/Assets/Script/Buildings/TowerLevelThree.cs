using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level 3 Tower", menuName = "Buildings/Tower/Level three")]
public class TowerLevelThree : TowerLevel
{
    public override void Init()
    {
        anim = crossbow.GetComponent<Animator>();
        arrow = Resources.Load<GameObject>("Arrow 3x projectile");
    }

    public override void LevelUp(BuyPoint buyPoint)
    {
        
    }

    public override void Update()
    {
        CrossbowShooting(crossbow.enemyPos);
    }

    public override void CrossbowRotate(Collider2D collision)
    {
        Vector2 direction = collision.transform.position - crossbow.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + offset;
        var target = Quaternion.Euler(new Vector3(0, 0, angle));
        crossbow.transform.rotation = Quaternion.RotateTowards(crossbow.transform.rotation, target, speed);
        anim.SetBool("Shooting", true);

        crossbow.enemyPos = angle;
    }

    public override void CrossbowShooting(float enemyPos)
    {
        var newArrow = Instantiate(arrow, crossbow.transform.position, Quaternion.Euler(new Vector3(0, 0, enemyPos)));
    }
}
