using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapontest : MonoBehaviour
{
    public PlayerController playerscript;

    public void shovel()
    {
        playerscript.SetWeapon(PlayerController.Weapon.Shovel);
    }
    public void Scythe()
    {
        playerscript.SetWeapon(PlayerController.Weapon.Scythe);
    }
    public void Pitchfork()
    {
        playerscript.SetWeapon(PlayerController.Weapon.Pitchfork);
    }
    public void Flintlock()
    {
        playerscript.SetWeapon(PlayerController.Weapon.Flintlock);
    }
    public void Shotgun()
    {
        playerscript.SetWeapon(PlayerController.Weapon.Shotgun);
    }
    public void Machinegun()
    {
        playerscript.SetWeapon(PlayerController.Weapon.Machinegun);
    }
}
