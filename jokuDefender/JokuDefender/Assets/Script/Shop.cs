using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public PlayerController player;
    private int playerGold;

    [HideInInspector]
    public List<int> Upgrade0 = new List<int> { 500, 750, 1125, 1690, 2535, 3800 }; //Attack speed
    [HideInInspector]
    public List<int> Upgrade1 = new List<int> { 250, 375, 565, 850, 1275, 1915 }; // Health
    [HideInInspector]
    public List<int> Upgrade2 = new List<int> { 200, 300, 350, 675, 1015, 1525 }; // Movement speed
    [HideInInspector]
    public List<int> Upgrade3 = new List<int> { 300, 450, 675, 1015, 1520, 2280 }; // Armor

    //not sure if i could put 1 HideInInspector but didnt find anyting on google
    [HideInInspector]
    public bool Weapon0Owned = true; // shovel
    [HideInInspector]
    public bool Weapon1Owned = false; // scythe
    [HideInInspector]
    public bool Weapon2Owned = false; // pitckfork
    [HideInInspector]
    public bool Weapon3Owned = false; // flintlock
    [HideInInspector]
    public bool Weapon4Owned = false; // shotgun
    [HideInInspector]
    public bool Weapon5Owned = false; // machinegun

    public int[] Weaponcosts;
    public TextMeshProUGUI[] CostTexts;
    public TextMeshProUGUI[] LevelTexts;
    public Button[] BuyButtons;

    private void Start()
    {
        //tässä on listenereitä jokaiseen buttoniin joka on parempi tapa tehä ku 10 eri funktionia jokaselle aseelle ja upgradelle

        BuyButtons[0].onClick.AddListener(delegate { ButtonSwitch(0); });
        BuyButtons[1].onClick.AddListener(delegate { ButtonSwitch(1); });
        BuyButtons[2].onClick.AddListener(delegate { ButtonSwitch(2); });
        BuyButtons[3].onClick.AddListener(delegate { ButtonSwitch(3); });
        BuyButtons[4].onClick.AddListener(delegate { ButtonSwitch(4); });
        BuyButtons[5].onClick.AddListener(delegate { ButtonSwitch(5); });
        BuyButtons[6].onClick.AddListener(delegate { ButtonSwitch(6); });
        BuyButtons[7].onClick.AddListener(delegate { ButtonSwitch(7); });
        BuyButtons[8].onClick.AddListener(delegate { ButtonSwitch(8); });
        BuyButtons[9].onClick.AddListener(delegate { ButtonSwitch(9); });
    }
    
    void ButtonSwitch(int index)
    {
        Debug.Log("Buttonoon");

        //weapons are 0,1,2,3,4,5 and upgrades 6, 7, 8, 9
        if (index <= 5)
        {
            BuyWeapon(index);
        }
        else
        {
            Upgrade(index);
        }
    }

    private void BuyWeapon(int index)
    {
        playerGold = player.PlayerGold;

        bool Owned = (bool)GetType().GetField("Weapon" + index + "Owned").GetValue(this);

        if (!Owned)
        {
            if (playerGold >= Weaponcosts[index])
            {
                player.AddGold(-Weaponcosts[index]);
                player.SetWeapon((PlayerController.Weapon)index);
                CostTexts[index].text = "Owned";
            }
        }
        else
        {
            player.SetWeapon((PlayerController.Weapon)index);
        }
    }

    private void Upgrade(int index)
    {
        playerGold = player.PlayerGold;
        index -= 6;

        List<int> cost = (List<int>)GetType().GetField("Upgrade" + index).GetValue(this);
        int[] Levels = player.UpgradeLevels;

        if (playerGold >= cost[Levels[index]] && Levels[index] < 5)  
        {
            player.AddGold(-cost[Levels[index]]);
            player.UpgradeLevels[index] += 1;

            if (index == 0)
            {
                player.UpdateCooldown();
                player.MaxHealth = 20 + Levels[1] * 5;
                player.moveSpeed = 6 + Levels[2];
                player.Armor = Levels[3];
            }

            if (Levels[index] == 5)
            {
                CostTexts[index + 6].text = "Max";
                LevelTexts[index].text = "6/6";
            }
            else
            {
                CostTexts[index + 6].text = cost[Levels[index]].ToString() + "g";
                LevelTexts[index].text = Levels[index].ToString() + "/6";
            }
        }
    }
}
