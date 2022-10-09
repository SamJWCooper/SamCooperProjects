using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionScript : MonoBehaviour
{
    PlayerInteractionColliderScript interactionCol;

    void Start()
    {
        interactionCol = this.gameObject.transform.GetChild(2).GetComponent<PlayerInteractionColliderScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            if (interactionCol.currentId == "door")
            {
                interactionCol.currentObject.GetComponentInParent<DoorHingeScript>().DoorOpenClose();
                Debug.Log("Get the currently touched gameobject script and open door");
            }
        }
    }
}
