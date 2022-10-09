using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockAnimationScript : MonoBehaviour
{
    GameObject blockColliderForward;
    GameObject blockColliderLeft;
    GameObject blockColliderRight;

    PlayerPunchHitScript hitScript;

    void Start()
    {
        blockColliderForward = this.gameObject.transform.parent.GetChild(3).GetChild(0).gameObject;
        blockColliderLeft = this.gameObject.transform.parent.GetChild(3).GetChild(1).gameObject;
        blockColliderRight = this.gameObject.transform.parent.GetChild(3).GetChild(2).gameObject;

        BlockForwardDisable();
        BlockLeftDisable();
        BlockRightDisable();
    }

    public void BlockForwardEnable()
    {
        blockColliderForward.SetActive(true);
    }

    public void BlockForwardDisable()
    {
        blockColliderForward.SetActive(false);
    }

    public void BlockLeftEnable()
    {
        blockColliderLeft.SetActive(true); 
    }

    public void BlockLeftDisable()
    {
        blockColliderLeft.SetActive(false); 
    }

    public void BlockRightEnable()
    {
        blockColliderRight.SetActive(true);
    }

    public void BlockRightDisable()
    {
        blockColliderRight.SetActive(false); 
    }
}
