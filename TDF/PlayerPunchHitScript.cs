using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPunchHitScript : MonoBehaviour
{
    GameObject jabCollider;
    GameObject crossCollider;
    GameObject hookLeftCollider;
    GameObject hookRightCollider;

    GameObject jabPath;
    GameObject crossPath;
    GameObject hookLeftPath;
    GameObject hookRightPath;

    PlayerPunchCollisionScript punchHitScript;
    PlayerPunchPathScript pathHitScript;
    PlayerPunchPoints pointScript;
    PlayerActionQueueScript queueScript;

    public string closestPoint;

    string currentPunchType;

    void Start()
    {
        jabCollider = this.gameObject.transform.parent.GetChild(2).transform.GetChild(0).gameObject;
        crossCollider = this.gameObject.transform.parent.GetChild(2).transform.GetChild(1).gameObject;
        hookLeftCollider = this.gameObject.transform.parent.GetChild(2).transform.GetChild(2).gameObject;
        hookRightCollider = this.gameObject.transform.parent.GetChild(2).transform.GetChild(3).gameObject;

        jabPath = this.gameObject.transform.parent.GetChild(5).transform.GetChild(0).gameObject;
        crossPath = this.gameObject.transform.parent.GetChild(5).transform.GetChild(1).gameObject;
        hookLeftPath = this.gameObject.transform.parent.GetChild(5).transform.GetChild(2).gameObject;
        hookRightPath = this.gameObject.transform.parent.GetChild(5).transform.GetChild(3).gameObject;

        pointScript = this.gameObject.transform.parent.GetChild(4).GetComponent<PlayerPunchPoints>(); 
        queueScript = this.gameObject.transform.parent.GetComponent<PlayerActionQueueScript>(); 

        JabDisable();
        CrossDisable();
        HookLeftDisable();
        HookRightDisable();

        JabDisable();
        CrossDisable();
    }

    public void HitCheck()
    {
        currentPunchType = queueScript.actionQueue[0]; //Debug.Log("Current Punch Type = " + currentPunchType);

        if (closestPoint == "front")
        {
            Debug.Log("||Front||");
            if (currentPunchType == "jab" || currentPunchType == "cross")
            {
                if (pathHitScript.hitBlockForward == false)
                {
                    Debug.Log("Hit - Jab/Cross");
                }
                if (pathHitScript.hitBlockForward)
                {
                    Debug.Log("Blocked - Jab/Cross");
                }
            }

            if (currentPunchType == "hookLeft")
            {
                if (pathHitScript.hitBlockRight == false)
                {
                    Debug.Log("Hit - Left Hook");
                }
                if (pathHitScript.hitBlockRight)
                {
                    Debug.Log("Blocked - Left Hook");
                }
            }

            if (currentPunchType == "hookRight")
            {
                if (pathHitScript.hitBlockLeft == false)
                {
                    Debug.Log("Hit - Right Hook");
                }
                if (pathHitScript.hitBlockLeft)
                {
                    Debug.Log("Blocked - Right Hook");
                }
            }
        }

        if (closestPoint == "left")
        {
            Debug.Log("||Left||");
            if (currentPunchType == "jab" || currentPunchType == "cross")
            {
                if (pathHitScript.hitBlockLeft == false)
                {
                    Debug.Log("Hit - Jab/Cross");
                }
                if (pathHitScript.hitBlockLeft)
                {
                    Debug.Log("Blocked - Jab/Cross");
                }
            }

            if (currentPunchType == "hookLeft")
            {
                if (pathHitScript.hitBlockForward == false)
                {
                    Debug.Log("Hit - Left Hook");
                }
                if (pathHitScript.hitBlockForward)
                {
                    Debug.Log("Blocked - Left Hook");
                }
            }

            if (currentPunchType == "hookRight")
            {
                Debug.Log("Hit - Right Hook");
            }
        }

        if (closestPoint == "right")
        {
            Debug.Log("||RIGHT||");
            if (currentPunchType == "jab" || currentPunchType == "cross")
            {
                if (pathHitScript.hitBlockRight == false)
                {
                    Debug.Log("Hit - Jab/Cross");
                }
                if (pathHitScript.hitBlockRight)
                {
                    Debug.Log("Blocked - Jab/Cross");
                }
            }

            if (currentPunchType == "hookLeft")
            {
                Debug.Log("Hit - Left Hook");
            }

            if (currentPunchType == "hookRight")
            {
                if (pathHitScript.hitBlockForward == false)
                {
                    Debug.Log("Hit - Right Hook");
                }
                if (pathHitScript.hitBlockForward)
                {
                    Debug.Log("Blocked - Right Hook");
                }
            }
        }
    }



    // ------ PUNCH COLLIDERS ------
    void JabEnable()
    {
        jabCollider.SetActive(true);
        punchHitScript = jabCollider.GetComponent<PlayerPunchCollisionScript>();
        pathHitScript = jabPath.GetComponent<PlayerPunchPathScript>(); 
    }

    void JabDisable()
    {
        jabCollider.SetActive(false);
    }

    void CrossEnable()
    {
        crossCollider.SetActive(true);
        punchHitScript = crossCollider.GetComponent<PlayerPunchCollisionScript>();
        pathHitScript = crossPath.GetComponent<PlayerPunchPathScript>();
    }

    void CrossDisable()
    {
        crossCollider.SetActive(false);
    }

    void HookLeftEnable()
    {
        hookLeftCollider.SetActive(true);
        punchHitScript = hookLeftCollider.GetComponent<PlayerPunchCollisionScript>();
        pathHitScript = hookLeftPath.GetComponent<PlayerPunchPathScript>();
    }

    void HookLeftDisable()
    {
        hookLeftCollider.SetActive(false);
    }

    void HookRightEnable()
    {
        hookRightCollider.SetActive(true);
        punchHitScript = hookRightCollider.GetComponent<PlayerPunchCollisionScript>();
        pathHitScript = hookRightPath.GetComponent<PlayerPunchPathScript>();
    }

    void HookRightDisable()
    {
        hookRightCollider.SetActive(false);
        punchHitScript = hookRightCollider.GetComponent<PlayerPunchCollisionScript>();
    }
}
