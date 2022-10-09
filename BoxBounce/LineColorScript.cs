using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineColorScript : MonoBehaviour
{
    public bool isRed;
    public bool isGreen;
    public bool isBlue;
    public bool isYellow;
    public BoxScript boxScript;

    void Start()
    {
        boxScript = GameObject.FindGameObjectWithTag("Box").GetComponent<BoxScript>();
    }

    // checks to see what color the box hit
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Box" && isRed == true)
        {
            boxScript.hitRed = true;
            Debug.Log(boxScript.hitRed);
        }

        if (other.tag == "Box" && isGreen == true)
        {
            boxScript.hitGreen = true;
            Debug.Log(boxScript.hitGreen);
        }

        if (other.tag == "Box" && isBlue == true)
        {
            boxScript.hitBlue = true;
            Debug.Log(boxScript.hitBlue);
        }

        if (other.tag == "Box" && isYellow == true)
        {
            boxScript.hitYellow = true;
            Debug.Log(boxScript.hitYellow);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Box" && isRed == true)
        {
            boxScript.hitRed = false;
            Debug.Log(boxScript.hitRed);
        }

        if (other.tag == "Box" && isGreen == true)
        {
            boxScript.hitGreen = false;
            Debug.Log(boxScript.hitGreen);
        }

        if (other.tag == "Box" && isBlue == true)
        {
            boxScript.hitBlue = false;
            Debug.Log(boxScript.hitBlue);
        }

        if (other.tag == "Box" && isYellow == true)
        {
            boxScript.hitYellow = false;
            Debug.Log(boxScript.hitYellow);
        }
    }
}
