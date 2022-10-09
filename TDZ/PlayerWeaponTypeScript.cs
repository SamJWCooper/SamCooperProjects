using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponTypeScript : MonoBehaviour
{
    PlayerInventoryScript invScript;
    PlayerAmmoScript ammoScript;

    public string playerWeaponType;
    public float accuracy;
    public float recoil;
    public float recoilMultiplier;
    public float aimSpeed;
    public int recoilRapidFireRate;
    public string mechanismType;
    public float rateOfFire;

    public int loadedAmmo;
    public int ammoCapacity;
    public float reloadSpeed;

    //public string caliber;
    public int damage;
    public int criticalChance;

    void Start()
    {
        invScript = GetComponent<PlayerInventoryScript>();
        ammoScript = GetComponent<PlayerAmmoScript>();
    }

    public void WeaponTypeAssigner()
    {
        if (invScript.equippedWeaponType == "m9")
        {
            accuracy = 1.8f;
            recoil = 0.3f;
            recoilMultiplier = 1.4f;
            aimSpeed = 52f;
            recoilRapidFireRate = 1;
            mechanismType = "semiAuto";
            rateOfFire = 0.10f;

            loadedAmmo = ammoScript.m9LoadedAmmo;
            ammoCapacity = 15;
            reloadSpeed = 0.7f;

            damage = 25;
            criticalChance = 8;
        }

        if (invScript.equippedWeaponType == "mp5")
        {
            accuracy = 1.5f;
            recoil = 0.1f;
            recoilMultiplier = 1.2f;
            aimSpeed = 60f;
            recoilRapidFireRate = 3;
            mechanismType = "fullAuto";
            rateOfFire = 0.12f;

            loadedAmmo = ammoScript.mp5LoadedAmmo;
            ammoCapacity = 30;
            reloadSpeed = 0.8f;

            damage = 25;
            criticalChance = 10;
        }

        if (invScript.equippedWeaponType == "mac10")
        {
            accuracy = 2.2f;
            recoil = 0.15f;
            recoilMultiplier = 2f;
            aimSpeed = 65f;
            recoilRapidFireRate = 6;
            mechanismType = "fullAuto";
            rateOfFire = 0.05f;

            loadedAmmo = ammoScript.mac10LoadedAmmo;
            ammoCapacity = 30;
            reloadSpeed = 1f;

            damage = 20;
            criticalChance = 3;
        }

        if (invScript.equippedWeaponType == "akm")
        {
            accuracy = 1f;
            recoil = 0.3f;
            recoilMultiplier = 0.9f;
            aimSpeed = 50f;
            recoilRapidFireRate = 1;
            mechanismType = "fullAuto";
            rateOfFire = 0.14f;

            loadedAmmo = ammoScript.akmLoadedAmmo;
            ammoCapacity = 30;
            reloadSpeed = 1.2f;

            damage = 30;
            criticalChance = 20;
        }

        /*
        if (invScript.equippedWeaponType == "505")
        {
            accuracy = 2.2f;
            recoil = 0.1f;
            recoilMultiplier = 6f;
            aimSpeed = 65f;
            recoilRapidFireRate = 5;
            mechanismType = "pumpAction";
            rateOfFire = 0.05f;
        }
        */
    }
}
