using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionColliderScript : MonoBehaviour
{
    public GameObject currentObject;
    public string currentId;

    void OnTriggerStay2D(Collider2D other)
    {
        currentObject = other.gameObject;
        if (currentObject.GetComponent("InteractiveIdScript") != null)
        {
            currentId = currentObject.GetComponent<InteractiveIdScript>().id;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        currentObject = null;
        currentId = null;
    }
}
