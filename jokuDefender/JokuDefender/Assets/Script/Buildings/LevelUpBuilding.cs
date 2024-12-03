using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelUpBuilding : MonoBehaviour
{
    public BuyPoint buyPoint;
    public void LevelUp()
    {
        buyPoint.currentBuilding.currentLevel.LevelUp(buyPoint);
        gameObject.SetActive(false);
    }
    public void CloseTab()
    {
        gameObject.SetActive(false);
    }
}
