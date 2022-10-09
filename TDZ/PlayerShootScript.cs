using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootScript : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    PlayerAccuracyScript accuracyScript;
    PlayerAccuracyTransitionScript transitionScript;
    PlayerWeaponTypeScript weaponType;
    PlayerWeaponReloadScript reloadScript;

    public float bulletForce = 20f;
    Quaternion randomRotation;

    void Start()
    {
        accuracyScript = GetComponent<PlayerAccuracyScript>();
        transitionScript = GetComponent<PlayerAccuracyTransitionScript>();
        weaponType = GetComponent<PlayerWeaponTypeScript>();
        reloadScript = GetComponent<PlayerWeaponReloadScript>();
    }

    public bool isHoldingShoot;
    bool isFiring;
    public bool triggerReset = true;

    void Update()
    {
        ShootInput();
        ShootTypeSelector();
    }
    
    // mouse inputs
    void ShootInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isHoldingShoot = true;
        }

        if (Input.GetMouseButton(0))
        {
            isHoldingShoot = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isHoldingShoot = false;
            triggerReset = true;                      // used for semi auto shooting, for seeing if the player has released the mouse button
            accuracyScript.recoilRateOfIncrease = 0f; // resets the recoilIncreaseRate to 0
        }
    }

    void ShootTypeSelector()
    {
        if (isHoldingShoot == true)
        {
            // for checking if ammo is loaded and is not reloading
            if (weaponType.loadedAmmo > 0 && reloadScript.isReloading == false)
            {
                // semi auto fire
                if (weaponType.mechanismType == "semiAuto" && isFiring == false && triggerReset == true)
                {
                    triggerReset = false;
                    StartCoroutine(SemiAutomaticFireRun());
                    isFiring = true;
                }

                // full auto fire
                if (weaponType.mechanismType == "fullAuto" && isFiring == false)
                {
                    StartCoroutine(AutomaticFireRun());
                    isFiring = true;
                }
            }

            else
            {
                Debug.Log("Out of ammo");
            }
        }
    }

    IEnumerator SemiAutomaticFireRun()
    {
        SetSpawnAccuracy();
        Shoot();
        yield return new WaitForSeconds(weaponType.rateOfFire);
        isFiring = false;
    }

    // full auto coroutine
    IEnumerator AutomaticFireRun()
    {
        if (isHoldingShoot == true)
        {
            SetSpawnAccuracy();
            Shoot();
            yield return new WaitForSeconds(weaponType.rateOfFire);
            isFiring = false;
        }
    }

    // random rotation for accuracy generator
    public void SetSpawnAccuracy()
    {
        randomRotation = Quaternion.Euler(0.0f, 0.0f, Random.Range(transitionScript.targetAccuracy1, transitionScript.targetAccuracy2)); // accuracyScript.accuracyNum1 and 2 old idk
    }

    // bullet instantiation + force
    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.transform.localRotation = randomRotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        bullet.GetComponent<BulletScript>().damage = weaponType.damage;
        bullet.GetComponent<BulletScript>().criticalChance = weaponType.criticalChance;

        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        accuracyScript.IncreaseRecoil();

        // rapid fire time reset 
        transitionScript.timeRemaining = 0.2f; // time before the recoil will begin to shrink after shooting stops
        transitionScript.startTimer = true;
        transitionScript.rapidFireValue = transitionScript.rapidFireValue + 1;

        weaponType.loadedAmmo = weaponType.loadedAmmo - 1;
    }
}
