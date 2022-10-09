using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAmmoScript : MonoBehaviour
{
    PlayerInventoryScript invScript;
    PlayerWeaponTypeScript weaponTypeScript;

    public int m9LoadedAmmo;
    public int mp5LoadedAmmo;
    public int mac10LoadedAmmo;
    public int akmLoadedAmmo;

    void Start()
    {
        invScript = GetComponent<PlayerInventoryScript>();
        weaponTypeScript = GetComponent<PlayerWeaponTypeScript>();
    }

    void Update()
    {
        if (invScript.equippedWeaponType == "m9")
        {
            m9LoadedAmmo = weaponTypeScript.loadedAmmo;
        }

        if (invScript.equippedWeaponType == "mp5")
        {
            mp5LoadedAmmo = weaponTypeScript.loadedAmmo;
        }

        if (invScript.equippedWeaponType == "m9")
        {
            mac10LoadedAmmo = weaponTypeScript.loadedAmmo;
        }

        if (invScript.equippedWeaponType == "akm")
        {
            akmLoadedAmmo = weaponTypeScript.loadedAmmo;
        }
    }
}
