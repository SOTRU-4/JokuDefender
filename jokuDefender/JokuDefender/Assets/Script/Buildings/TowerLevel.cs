using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class TowerLevel : BuildingLevel
{
    public float offset;
    public float speed;
    public float cooldown;
    public bool shoot;

    public GameObject arrow;
    public Animator anim;

    public Tower tower;
    public TowerCrossbow crossbow;
    
    abstract public override void Init();

    abstract public override void LevelUp(BuyPoint buyPoint);

    abstract public override void Update();

    abstract public void CrossbowRotate(Collider2D collision);

    abstract public void CrossbowShooting(float enemyPos);
}
