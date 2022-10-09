using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAccuracyTransitionScript : MonoBehaviour
{
    // ** to do, add to rapidFireRate, make it so the accuracy cone expands, however ensure before next shot accurcy resets to 100%

    PlayerMovementScript moveScript;
    PlayerAccuracyScript accuracyScript;
    PlayerWeaponTypeScript weaponScript;

    public float currentAccuracy1;
    public float currentAccuracy2;

    public float targetAccuracy1;
    public float targetAccuracy2;

    void Start()
    {
        moveScript = GetComponent<PlayerMovementScript>();
        accuracyScript = GetComponent<PlayerAccuracyScript>();
        weaponScript = GetComponent<PlayerWeaponTypeScript>();
        isRapidFiring = true;
    }

    void Update()
    {
        AccuracyTransition();
        AssignTargetAccuracy();
        AimSpeed();
        RecoilReset();
        RecoilTimer();
        RapidFireRate();
    }

    // *** set the accuracyNum values as the target accuracy to move to and get the current accuracy when a change occurs
    void AssignTargetAccuracy()
    {
        targetAccuracy1 = accuracyScript.accuracyNum1;
        targetAccuracy2 = accuracyScript.accuracyNum2;
    }

    public float maxAccuracy = 5f;

    void AccuracyTransition()
    {
        // increase currentAccuracy1
        if (currentAccuracy1 < targetAccuracy1 && currentAccuracy1 < maxAccuracy)
        {
            currentAccuracy1 = currentAccuracy1 + 1f * aimSpeedValue;
        }

        // decrease currentAccuracy1
        if (currentAccuracy1 > targetAccuracy1)
        {
            currentAccuracy1 = currentAccuracy1 - 1f * aimSpeedValue;
        }
        
        // increase currentAccuracy2
        if (currentAccuracy2 < targetAccuracy2 && currentAccuracy2 < maxAccuracy)
        {
            currentAccuracy2 = currentAccuracy2 + 1f * aimSpeedValue;
        }

        // decrease currentAccuracy2
        if (currentAccuracy2 > targetAccuracy2)
        {
            currentAccuracy2 = currentAccuracy2 - 1f * aimSpeedValue;
        }
        
        accuracyScript.accuracyFinalValue = currentAccuracy2 + accuracyScript.recoilAmount;
    }

    public float currentRecoil;
    public float targetRecoil = 0f;
    public bool resetRecoilRunning;

    public float recoilResetTime;
    public bool isRapidFiring;
    public int rapidFireValue;
    void RapidFireRate()
    {
        if (rapidFireValue > weaponScript.recoilRapidFireRate || moveScript.isAiming == false)
        {
            isRapidFiring = false;
        }

        if (timeRemaining <= 0 && rapidFireValue <= weaponScript.recoilRapidFireRate && isRapidFiring == true)
        {
            accuracyScript.isRunning = false;
            accuracyScript.startTimer = false;
            resetRecoilRunning = false;
            isRapidFiring = true;
        }
    }

    void RecoilReset()
    {
        if (timeRemaining <= 0)
        {
            // decreases accuracy cone over time 
            if (accuracyScript.recoilAmount > targetRecoil)
            {
                accuracyScript.recoilAmount = accuracyScript.recoilAmount - 0.5f * aimSpeedValue;
            }

            // once recoil cone has fully reset to its default size, will reset varibles
            if (accuracyScript.recoilAmount <= targetRecoil)
            {
                accuracyScript.isRunning = false;
                accuracyScript.startTimer = false;
                resetRecoilRunning = false;
                isRapidFiring = true;
                rapidFireValue = 0;
            }
        }
    }

    public bool startTimer;
    public float timeRemaining;
    void RecoilTimer()
    {
        if (startTimer == true)
        {
            if (timeRemaining > 0f)
            {
                timeRemaining -= Time.deltaTime;
            }
        }

        if (timeRemaining <= 0)
        {
            startTimer = false;
        }
    }


    float aimSpeedValue;
    //float aimSpeedAimingValue; 

    void AimSpeed()
    {
        aimSpeedValue = Time.deltaTime * weaponScript.aimSpeed;

        /*
        // for faster accuracy cone adjust while aiming
        if (moveScript.isAiming == true)
        {
            aimSpeedValue = Time.deltaTime * weaponScript.aimSpeed * 1.5f;
        }

        // slower while not aiming
        else
        {
            aimSpeedValue = Time.deltaTime * weaponScript.aimSpeed;
        }
        */
    }
}
