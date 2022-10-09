using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyCubeScript : MonoBehaviour
{
    void Update()
    {
        Rotate();
    }

    public bool isInZone;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Box")
        {
            isInZone = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Box")
        {
            isInZone = false;
        }
    }

    public bool rotateLeft;
    public bool rotateRight;
    void Rotate()
    {
        if (rotateLeft == true)
        {
            transform.Rotate(new Vector3(0, 0, 90));
            rotateLeft = false;
        }

        if (rotateRight == true)
        {
            transform.Rotate(new Vector3(0, 0, -90));
            rotateRight = false;
        }
    }
}
