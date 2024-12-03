using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level 2 Tower", menuName = "Buildings/Tower/Level two")]
public class TowerLevelTwo : TowerLevel
{
    //[SerializeField] GameObject nextLevelTower;
    public override void Init()
    {
        anim = crossbow.GetComponent<Animator>();
        arrow = Resources.Load<GameObject>("Arrow 2x projectile");
    }

    public override void LevelUp(BuyPoint buyPoint)
    {
        if (PlayerController.instance.PlayerGold >= 750)
        {
            PlayerController.instance.AddGold(-750);

            Destroy(buyPoint.currentBuilding.gameObject);

            var currentBuilding = Instantiate(buyPoint.currentBuilding.currentLevel.nextLevelBuilding, buyPoint.currentBuilding.transform.position, Quaternion.identity);

            buyPoint.currentBuilding = currentBuilding.GetComponent<Building>();
        }
        
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
