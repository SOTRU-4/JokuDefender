using System.Collections;
using System.Collections.Generic;
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

    public Text ScytheOwnedtext;
    public Text PitchforkOwnedtext;
    public Text FlintlockOwnedtext;
    public Text ShotgunOwnedtext;
    public Text MachinegunOwnedtext;

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
            if (playerGold >= 1000)
            {
                player.AddGold(-1000);
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
            if (playerGold >= 1500)
            {
                player.AddGold(-1500);
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
            if (playerGold >= 2000)
            {
                player.AddGold(-2000);
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
            if (playerGold >= 2500)
            {
                player.AddGold(-2500);
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
