using System.Collections;
using System.Collections.Generic;
using UnityEngine;


abstract public class BuildingLevel : ScriptableObject
{
    public GameObject nextLevelBuilding;
    abstract public void Init();
    abstract public void Update();

    abstract public void LevelUp(BuyPoint buyPoint);
}
