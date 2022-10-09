using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAccuracyScript : MonoBehaviour
{
    // redo rapidFireReset once accuracy and inventory works as intended

    PlayerAimingScript aimScript;
    PlayerShootScript shootScript;
    PlayerMovementScript moveScript;
    PlayerWeaponTypeScript weaponScript;
    PlayerAccuracyTransitionScript transitionScript;


    public float accuracyNum1;
    public float accuracyNum2;
    public float numAccuracy;
    public float recoilAmount;

    void Start()
    {
        aimScript = GetComponent<PlayerAimingScript>();
        shootScript = GetComponent<PlayerShootScript>();
        moveScript = GetComponent<PlayerMovementScript>();
        weaponScript = GetComponent<PlayerWeaponTypeScript>();
        transitionScript = GetComponent<PlayerAccuracyTransitionScript>();
    }

    void Update()
    {
        SetAccuracy();
    }

    // recoil value & multiplier values will be assigned in the weapon attribute script as according to each weapon type
    // set timeRemaining for recoil to a varible too that will also be adjusted in weapon types script

    public void SetAccuracy() 
    {
        // weaponScript.accuracy * amount that accuracy is effected by movement & aim
        // is immobile & is aiming
        if (moveScript.moveType == "immobileAiming")
        {
            accuracyNum1 = -weaponScript.accuracy;
            accuracyNum2 = weaponScript.accuracy;
        }

        // is immobile & not aiming
        if (moveScript.moveType == "immobile")
        {
            accuracyNum1 = -weaponScript.accuracy * 5f;
            accuracyNum2 = weaponScript.accuracy * 5f;
        }

        // is moving & aiming
        if (moveScript.moveType == "movingAiming")
        {
            accuracyNum1 = -weaponScript.accuracy * 6f;
            accuracyNum2 = weaponScript.accuracy * 6f;
        }

        // is moving & not aiming
        if (moveScript.moveType == "moving")
        {
            accuracyNum1 = -weaponScript.accuracy * 15f;
            accuracyNum2 = weaponScript.accuracy * 15f;
        }
        SetRecoil();
        aimScript.UpdateViewangle();
    }

    //public float recoilValue;
    public float recoilMultiplier;

    public float accuracyFinalValue; // ********************** 

    // recoil functions 
    public void SetRecoil() // *** this is where final values will be assigned for use in transition script;
    {
        accuracyNum1 = accuracyNum1 + (-recoilAmount);
        accuracyNum2 = accuracyNum2 + (recoilAmount);  
        numAccuracy = accuracyNum2;
    }

    public bool isRunning;
    public bool startTimer;

    // if aiming recoil is reduced;
    //recoilRateOfIncrease = recoilRateOfIncrease + (recoilValue * recoilMultiplier);

    public float recoilRateOfIncrease;
    public void IncreaseRecoil()
    {
        // if is firing within rapid fire rate will not multiply recoil value
        if (transitionScript.isRapidFiring == true)
        {
            recoilAmount = recoilAmount + weaponScript.recoil;
        }

        // if has exeeded rapid fire rate then recoil will be multiplied and increased 
        if (transitionScript.isRapidFiring == false && transitionScript.currentAccuracy1 < transitionScript.maxAccuracy && transitionScript.currentAccuracy2 < transitionScript.maxAccuracy)
        {
            recoilMultiplier = weaponScript.recoilMultiplier;
            recoilRateOfIncrease = recoilRateOfIncrease + (weaponScript.recoil * recoilMultiplier);
            recoilAmount = recoilAmount + (recoilMultiplier * recoilRateOfIncrease);
        }

        accuracyNum1 = accuracyNum1 + 1f;
        accuracyNum2 = accuracyNum2 - 1f;

        aimScript.UpdateViewangle();
    }
}