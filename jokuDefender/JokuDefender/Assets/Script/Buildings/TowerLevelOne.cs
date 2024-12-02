using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[CreateAssetMenu(fileName = "Level 1 Tower", menuName = "Buildings/Tower/Level one")]
public class TowerLevelOne : TowerLevel
{
    //public GameObject nextLevelTower;
    public override void Init()
    {
        anim = crossbow.GetComponent<Animator>();
        arrow = Resources.Load<GameObject>("Arrow Projectile");
        
    }
    public override void LevelUp(BuyPoint buyPoint)
    {
        if(PlayerController.instance.PlayerGold >= 750)
        {
            PlayerController.instance.AddGold(-750);

            Destroy(buyPoint.currentBuilding.gameObject);

            var currentBuilding = Instantiate(buyPoint.currentBuilding.currentLevel.nextLevelBuilding, buyPoint.currentBuilding.transform.position, Quaternion.identity);

            buyPoint.currentBuilding = currentBuilding.GetComponent<Building>();

            
        }
        
        
        
        //Instantiate(nextLevelTower, tower.transform.position, Quaternion.identity);

        /*tower.currentLevel = Resources.Load<TowerLevelTwo>("Assets/Script/Buildings/Levels");
        tower.currentLevel = Instantiate(tower.currentLevel);
        SpriteRenderer image = tower.GetComponent<SpriteRenderer>();
        image.sprite = nextLevelSprite;
        crossbow.transform.position += new Vector3(0, 0.2f, 0);*/
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
