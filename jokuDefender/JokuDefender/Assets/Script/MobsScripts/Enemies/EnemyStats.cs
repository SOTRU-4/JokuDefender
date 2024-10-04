using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Mobs/Enemy")]
public class EnemyStats : ScriptableObject
{
    public string enemyName;
    public MainTarget mainTarget;
    public int healthPoints;
    public int damage;
    public int costInPoints;
}

public enum MainTarget
{
    Bed, Player, Buildings
}
