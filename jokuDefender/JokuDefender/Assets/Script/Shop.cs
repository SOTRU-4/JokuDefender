using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public PlayerController player;
    private int playerGold;

    private bool ScytheOwned = false;
    private bool PitchforkOwned = false;
    private bool FlintlockOwned = false;
    private bool ShotgunOwned = false;
    private bool MachinegunOwned = false;

    public TextMeshProUGUI ScytheOwnedtext;
    public TextMeshProUGUI PitchforkOwnedtext;
    public TextMeshProUGUI FlintlockOwnedtext;
    public TextMeshProUGUI ShotgunOwnedtext;
    public TextMeshProUGUI MachinegunOwnedtext;

    public void BuyShovel()
    {
        playerGold = player.PlayerGold;
        player.SetWeapon(PlayerController.Weapon.Shovel);

    }
    public void BuyScythe()
    {
        playerGold = player.PlayerGold;
        if (!ScytheOwned)
        {
            if (playerGold >= 500)
            {
                player.AddGold(-500);
                player.SetWeapon(PlayerController.Weapon.Scythe);
                ScytheOwnedtext.text = "Owned";
            }
        }
        else
        {
            player.SetWeapon(PlayerController.Weapon.Scythe);
        }
    }
    public void BuyPitchfork()
    {
        playerGold = player.PlayerGold;
        if (!PitchforkOwned)
        {
            if (playerGold >= 950)
            {
                player.AddGold(-950);
                player.SetWeapon(PlayerController.Weapon.Pitchfork);
                PitchforkOwnedtext.text = "Owned";
            }
        }
        else
        {
            player.SetWeapon(PlayerController.Weapon.Pitchfork);
        }
    }
    public void BuyFlintlock()
    {
        playerGold = player.PlayerGold;
        if (!FlintlockOwned)
        {
            if (playerGold >= 1800)
            {
                player.AddGold(-1800);
                player.SetWeapon(PlayerController.Weapon.Flintlock);
                FlintlockOwnedtext.text = "Owned";
            }
        }
        else
        {
            player.SetWeapon(PlayerController.Weapon.Flintlock);
        }
    }
    public void BuyShotgun()
    {
        playerGold = player.PlayerGold;
        if (!ShotgunOwned)
        {
            if (playerGold >= 3400)
            {
                player.AddGold(-3400);
                player.SetWeapon(PlayerController.Weapon.Shotgun);
                ShotgunOwnedtext.text = "Owned";
            }
        }
        else
        {
            player.SetWeapon(PlayerController.Weapon.Shotgun);
        }
    }
    public void BuyMachinegun()
    {
        playerGold = player.PlayerGold;
        if (!MachinegunOwned)
        {
            if (playerGold >= 7500)
            {
                player.AddGold(-7500);
                player.SetWeapon(PlayerController.Weapon.Machinegun);
                MachinegunOwnedtext.text = "Owned";
            }
        }
        else
        {
            player.SetWeapon(PlayerController.Weapon.Machinegun);
        }
    }
}
