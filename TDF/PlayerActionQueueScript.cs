using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionQueueScript : MonoBehaviour
{
    Animator anim;
    PlayerCombatScript combatScript;

    public List<string> actionQueue = new List<string>();

    void Start()
    {
        anim = this.gameObject.transform.GetChild(0).GetComponent<Animator>();
        combatScript = this.gameObject.transform.GetComponent<PlayerCombatScript>();
    }

    void Update()
    {
        ActionSelector();
        ActionEnd();
    }

    public bool isInAction;

    bool isFirstPunch = true;
    bool isInCombo;
    void ActionSelector()
    {
        if (isInAction == false && actionQueue.Count > 0)
        {
            isInAction = true;

            // charge
            if (actionQueue[0] == "chargeLeft" || actionQueue[0] == "chargeRight")
            {
                anim.SetTrigger(actionQueue[0]);
                ActionEnded();
            }

            // first jab/cross
            else if (isFirstPunch && actionQueue[0] == "jab" || isFirstPunch && actionQueue[0] == "cross") 
            {
                anim.SetTrigger(actionQueue[0]);
                isFirstPunch = false; 
                isInCombo = true;
            }
            // first hook
            else if (isFirstPunch && actionQueue[0] == "hookLeft" || isFirstPunch && actionQueue[0] == "hookRight")
            {
                anim.SetTrigger(actionQueue[0]);
                isFirstPunch = false; //Debug.Log("First Hook");
            }

            // jab in combo
            else if (isFirstPunch == false && isInCombo && actionQueue[0] == "jab" || isFirstPunch == false && isInCombo && actionQueue[0] == "cross")
            {
                anim.SetTrigger(actionQueue[0]); //Debug.Log("Jab/Cross IN Combo");
            }
            // jab no combo 
            else if (isFirstPunch == false && actionQueue[0] == "jab" && isInCombo == false || isFirstPunch == false && actionQueue[0] == "cross" && isInCombo == false)
            {
                //Debug.Log("Jab NO Combo");
                isInCombo = true;
                StartCoroutine(ChargeExitActionTimer());
            }
            
            // hook no first
            else if (isFirstPunch == false && actionQueue[0] == "hookLeft" || isFirstPunch == false && actionQueue[0] == "hookRight")
            {
                //Debug.Log("Hook");
                isInCombo = false;
                StartCoroutine(ChargeExitActionTimer()); 
            }
        }

        // for setting the holding hook varible for use ai 
        if (actionQueue.Count > 0 && actionQueue[0] == "hookLeft" && combatScript.isHoldingLeft)
        {
            combatScript.isHoldingHookLeft = true; 
        }
        if (actionQueue.Count > 0 && actionQueue[0] == "hookRight" && combatScript.isHoldingRight)
        {
            combatScript.isHoldingHookRight = true;
        }
    }

    IEnumerator ChargeExitActionTimer()
    {
        //Debug.Log("Charge Timer Enter");
        yield return new WaitForSeconds(0.125f);
        anim.SetTrigger(actionQueue[0]);

        if (actionQueue[0] == "chargeLeft" || actionQueue[0] == "chargeRight")
        {
            ActionEnded();
        }
    }

    public void ActionEnded()
    {
        //Debug.Log("Ended Action = " + actionQueue[0]);
        actionQueue.RemoveAt(0);
        isInAction = false;
    }

    void ActionEnd()
    {
        if (actionQueue.Count == 0 && combatScript.currentAnim != "jab" && combatScript.currentAnim != "chargeLeft" && combatScript.currentAnim != "hookLeft")
        {
            isFirstPunch = true; //Debug.Log("FINAL PUNCH THROWN");
            isInCombo = false;
        }
    }
}
