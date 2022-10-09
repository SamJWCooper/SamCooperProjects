using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponReloadScript : MonoBehaviour
{
    PlayerWeaponTypeScript weaponType;
    PlayerShootScript shootScript;

    void Start()
    {
        weaponType = GetComponent<PlayerWeaponTypeScript>();
        shootScript = GetComponent<PlayerShootScript>();
    }

    public bool isReloading;
    public int currentLoadedAmmo;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (weaponType.loadedAmmo == weaponType.ammoCapacity)
            {
                Debug.Log("Fully Loaded = " + weaponType.loadedAmmo);
            }

            if (weaponType.loadedAmmo < weaponType.ammoCapacity)
            {
                Debug.Log("Reloading = " + weaponType.loadedAmmo);
                isReloading = true;
                StartCoroutine(Reload());  
            }
        }

        // when loadedAmmo hits 0 and player releases mouse button then clicks again without reloading, gun will automattically reload
        if (weaponType.loadedAmmo == 0 && (Input.GetMouseButtonDown(0)))
        {
            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(weaponType.reloadSpeed);
        weaponType.loadedAmmo = weaponType.ammoCapacity;
        isReloading = false;
        Debug.Log("Reloaded  = " + weaponType.loadedAmmo);
    }
}
