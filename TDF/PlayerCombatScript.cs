using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatScript : MonoBehaviour
{
    Animator anim;
    PlayerActionQueueScript queue;
    BlockAnimationScript blockAnimation;

    void Start()
    {
        anim = this.gameObject.transform.GetChild(0).GetComponent<Animator>();
        queue = this.gameObject.transform.GetComponent<PlayerActionQueueScript>();
        blockAnimation = this.gameObject.transform.GetChild(0).GetComponent<BlockAnimationScript>();
        currentAnim = "idle";
    }

    void Update()
    {
        MouseInputLeft();
        MouseInputRight();
        SpaceInput();
        ExitInput();
        IsInCombat();
        //Debug.Log("CurrentAnim = " + currentAnim);
    }

    void IsInCombat()
    {
        if (currentAnim == "idle" || currentAnim == "blockForward" || currentAnim == "blockLeft" || currentAnim == "blockRight")
        {
            isInCombat = false; 
        }
        else
        {
            isInCombat = true;
        }
        //Debug.Log("In Combat = " + isInCombat);
        //Debug.Log(currentAnim);
    }

    public bool isInCombat;
    public string currentAnim;
    public bool isHoldingSpace;
    public bool isHoldingLeft;
    public bool isHoldingRight;
    public bool hasExited;
    bool isFeinting;

    public bool isHoldingHookLeft;
    public bool isHoldingHookRight;

    // for checking if space is being held
    void SpaceInput()
    {
        if (Input.GetKey("space") && hasExited == false)
        {
            isHoldingSpace = true;
            HoldingSpaceEnter();
            JabCancel(); // for if is charging and space is pressed 
            CrossCancel();
            StopCoroutine(LeftJabOrHoldCheck());
            StopCoroutine(RightJabOrHoldCheck());
            queue.actionQueue.Clear(); Debug.Log("Clear Current");
            queue.isInAction = false;
        }

        if (Input.GetKeyUp("space"))
        {
            isHoldingSpace = false;
            hasExited = false;
            HoldingSpaceExit();
            blockAnimation.BlockForwardDisable();
            blockAnimation.BlockLeftDisable();
            blockAnimation.BlockRightDisable();
        }

        // for if the player transitions from holding left or right block to charging a hook
        if (Input.GetKeyUp("space") && isHoldingLeft)
        {
            HookLeft();
        }

        if (Input.GetKeyUp("space") && isHoldingRight)
        {
            HookRight();
        }
    }

    // main punching controls 
    void MouseInputLeft()
    {
        // main left punch entry
        if (Input.GetMouseButtonDown(0) && isHoldingSpace == false)
        {
            StartCoroutine(LeftJabOrHoldCheck());
        }
        // left holding entry
        if (Input.GetMouseButtonDown(0) && isHoldingSpace)
        {
            HoldingLeftEnter();
        }
        // left holding exit
        if (Input.GetMouseButtonUp(0))
        {
            HoldingLeftExit();
            isHoldingHookLeft = false;
        }
    }

    void ExitInput()
    {
        // exit entry only if in idle
        if (Input.GetKeyDown(KeyCode.LeftShift) && currentAnim != "idle")
        {
            Exit();
            HookLeftCancel();
            HookRightCancel();
            hasExited = true;
            queue.ActionEnded();
        }
        // if in idle holding shift will enable feint
        if (Input.GetKey(KeyCode.LeftShift) && currentAnim == "idle" && isFeinting == false)
        {
            isFeinting = true;
        }
        // exit feint / reset exit
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isFeinting = false;
            hasExited = false;
            ExitReset();
            JabCancel();
            CrossCancel();
        }
        // exit reset for after it has been used
        if (hasExited && Input.GetMouseButtonUp(0) || hasExited && Input.GetMouseButtonUp(1) || Input.GetKeyUp("space")) // this might be shit
        {
            Debug.Log("EXIT RESET TEST");
            hasExited = false;
            HookLeftCancel();
            HookRightCancel();

            //JabCancel();
            //CrossCancel();
        }
    }

    // checks weather or not the mouse button is being held
    // will set the parameter for holding space or jab
    IEnumerator LeftJabOrHoldCheck()
    {
        ChargeLeft();

        yield return new WaitForSeconds(0.125f);
        // if feinting is enabled, will not start jab or hook
        if (isFeinting)
        {
            Exit();
            HoldingLeftExit();
            HoldingSpaceExit();
            HookLeftCancel(); Debug.Log("Feint");
        }
        // if after charge timer has ended and left mouse being pressed will hold left
        else if (Input.GetMouseButton(0))
        {
            HookLeft();
            HoldingLeftEnter();
        }
        // jabs if left mouse is not being pressed
        else
        {
            Jab();
        }
    }

    void MouseInputRight()
    {
        // main left punch entry
        if (Input.GetMouseButtonDown(1) && isHoldingSpace == false)
        {
            StartCoroutine(RightJabOrHoldCheck());
        }
        // left holding entry
        if (Input.GetMouseButtonDown(1) && isHoldingSpace)
        {
            HoldingRightEnter();
        }
        // left holding exit
        if (Input.GetMouseButtonUp(1))
        {
            HoldingRightExit();
            isHoldingHookRight = false;
        }
    }

    IEnumerator RightJabOrHoldCheck()
    {
        ChargeRight();

        yield return new WaitForSeconds(0.125f);
        // if feinting is enabled, will not start jab or hook
        if (isFeinting)
        {
            Exit();
            HoldingRightExit();
            HoldingSpaceExit();
            HookRightCancel(); Debug.Log("Feint");
        }
        // if after charge timer has ended and left mouse being pressed will hold left
        else if (Input.GetMouseButton(1))
        {
            HookRight();
            HoldingRightEnter();
        }
        // jabs if left mouse is not being pressed
        else
        {
            Cross(); 
        }
    }

    //=======================================================================\\

    // ------------- LEFT -------------\\
    void Jab()
    {
        queue.actionQueue.Add("jab");
    }

    void JabCancel()
    {
        anim.ResetTrigger("jab");
    }

    void ChargeLeft() // will need a new animation for hook hold / charge
    {
        queue.actionQueue.Add("chargeLeft");
    }
    void ChargeLeftCancel()
    {
        anim.ResetTrigger("chargeLeft");
    }

    void HookLeft()
    {
        queue.actionQueue.Add("hookLeft");
    }

    void HookLeftCancel()
    {
        anim.ResetTrigger("hookLeft");
    }

    void HoldingLeftEnter()
    {
        anim.SetBool("isHoldingLeft", true); 
        isHoldingLeft = true;
    }

    void HoldingLeftExit()
    {
        anim.SetBool("isHoldingLeft", false);
        isHoldingLeft = false;
    }


    // ------------- SPACE -------------\\
    void HoldingSpaceEnter()
    {
        anim.SetBool("isHoldingSpace", true);
    }

    void HoldingSpaceExit()
    {
        anim.SetBool("isHoldingSpace", false);
    }

    void Exit()
    {
        anim.SetBool("hasExited", true);
    }

    public void ExitReset()
    {
        anim.SetBool("hasExited", false);
    }

    // ------------- RIGHT -------------\\
    void Cross()
    {
        queue.actionQueue.Add("cross");
    }

    void CrossCancel()
    {
        anim.ResetTrigger("cross");
    }

    void ChargeRight() // will need a new animation for hook hold / charge
    {
        queue.actionQueue.Add("chargeRight");
    }

    void ChargeRightCancel()
    {
        anim.ResetTrigger("chargeRight");
    }

    void HookRight()
    {
        queue.actionQueue.Add("hookRight");
    }

    void HookRightCancel()
    {
        anim.ResetTrigger("hookRight");
    }

    void HoldingRightEnter()
    {
        anim.SetBool("isHoldingRight", true);
        isHoldingRight = true;
    }

    void HoldingRightExit()
    {
        anim.SetBool("isHoldingRight", false);
        isHoldingRight = false;
    }

}
