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

    public bool[] WeaponsOwned;

    public int[] WeaponCosts;
    public TextMeshProUGUI[] CostTexts;
    public TextMeshProUGUI[] LevelTexts;
    public Button[] BuyButtons;

    public Button CastleButton;
    public TextMeshProUGUI CastleText;
    public GameObject CastlePrefab;

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
    
    private void ButtonSwitch(int index)
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

        //bool Owned = (bool)GetType().GetField("Weapon" + index + "Owned").GetValue(this);
        Debug.Log(index);
        if (!WeaponsOwned[index])
        {
            if (playerGold >= WeaponCosts[index])
            {
                player.AddGold(-WeaponCosts[index]);
                player.SetWeapon((PlayerController.Weapon)index);
                CostTexts[index].text = "Owned";
                WeaponsOwned[index] = true;
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

        //index -=6 because most of the lists for the upgrades like leveltext, upgradelevels, and the upgradecosts dont include weapons at index 0-5 like costtexts or buybuttons
        index -= 6;

        List<int> cost = (List<int>)GetType().GetField("Upgrade" + index).GetValue(this);
        int[] Levels = player.UpgradeLevels;

        if (playerGold >= cost[Levels[index]] && Levels[index] < 6)
        {
            player.AddGold(-cost[Levels[index]]);
            player.UpgradeLevels[index] += 1;

            player.UpdateUpgrades();

            if (Levels[index] == 6)
            {
                CostTexts[index + 6].text = "Max";
                LevelTexts[index].text = "6/6";
                BuyButtons[index + 6].interactable = false;
            }
            else
            { 
                CostTexts[index + 6].text = cost[Levels[index]].ToString() + "g";
                LevelTexts[index].text = Levels[index].ToString() + "/6";
            }
        }
    }

   public void BuyCastle()
   {
        playerGold = player.PlayerGold;

        if (playerGold >= 1000)
        {
            CastleButton.interactable = false;
            player.AddGold(-1000);
            CastleText.text = "Owned";
            Instantiate(CastlePrefab, new Vector3(-1.5f,2.5f,0), Quaternion.identity);
        }
    }
}
