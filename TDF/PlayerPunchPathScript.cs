using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPunchPathScript : MonoBehaviour
{
    public bool hitBlockForward;
    public bool hitBlockLeft;
    public bool hitBlockRight;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "BlockForward")
        {
            hitBlockForward = true; 
        }

        if (other.gameObject.tag == "BlockLeft")
        {
            hitBlockLeft = true;
        }

        if (other.gameObject.tag == "BlockRight")
        {
            hitBlockRight = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "BlockForward")
        {
            hitBlockForward = false; 
        }

        if (other.gameObject.tag == "BlockLeft")
        {
            hitBlockLeft = false;
        }

        if (other.gameObject.tag == "BlockRight")
        {
            hitBlockRight = false;
        }
    }
}
