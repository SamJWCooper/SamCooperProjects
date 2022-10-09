using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailAreaScript : MonoBehaviour
{
    public bool isOnFailZone;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Box")
        {
            isOnFailZone = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Box")
        {
            isOnFailZone = false;
        }
    }
}
