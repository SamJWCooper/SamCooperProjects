using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    public FailAreaScript failAreaScript;
    public EmptyCubeScript emptyCubeScript;
    public Rigidbody2D rb;
    public Animator anim;

    int forceAmount = 75;
    int colorSelector;

    void Start()
    {
        failAreaScript = GameObject.FindGameObjectWithTag("FailArea").GetComponent<FailAreaScript>();
        emptyCubeScript = GameObject.FindGameObjectWithTag("EmptyCube").GetComponent<EmptyCubeScript>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        SelectColor();
    }

    void Update()
    {
        ColorMatch();

        // check to see if box is in the correct area to bounce 
        if ((Input.GetKeyDown(KeyCode.Space)))
        {
            if (failAreaScript.isOnFailZone == false && emptyCubeScript.isInZone == true)
            {
                rb.velocity = Vector2.zero;
                rb.AddForce(transform.up * forceAmount, ForceMode2D.Impulse);

                // delays time until the box is out of the color selector box
                StartCoroutine(WaitUntilOut());
                IEnumerator WaitUntilOut()
                {
                    yield return new WaitForSeconds(.2f);
                    SelectColor();
                }
            }

            if (failAreaScript.isOnFailZone == true)
            {
                Destroy(gameObject);
            }
        }

        // rotate left and right controls 
        if (Input.GetMouseButtonDown(0))
        {
            emptyCubeScript.rotateLeft = true;
        }

        if (Input.GetMouseButtonDown(1))
        {
            emptyCubeScript.rotateRight = true;
        }    
    }

    // destroys if touches dead colliders
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Dead")
        {
            Destroy(gameObject);
        } 
    }

    bool isRed;
    bool isGreen;
    bool isBlue;
    bool isYellow;

    int endRandomizer = 14;

    // random color changer
    void SelectColor()
    {
        StartCoroutine(ColorRandomizer());
        IEnumerator ColorRandomizer()
        {
            colorSelector = Random.Range(0, 5);

            if (endRandomizer > 0)
            {
                if (colorSelector == 1)
                {
                    isRed = true;

                    isGreen = false; isBlue = false; isYellow = false;

                    anim.SetBool("isRed", true);

                    anim.SetBool("isBlue", false); anim.SetBool("isGreen", false); anim.SetBool("isYellow", false);
                    Debug.Log("ISred");
                }

                if (colorSelector == 2)
                {
                    isGreen = true;

                    isRed = false; isBlue = false; isYellow = false;

                    anim.SetBool("isGreen", true);

                    anim.SetBool("isBlue", false); anim.SetBool("isRed", false); anim.SetBool("isYellow", false);
                    Debug.Log("ISgreen");
                }

                if (colorSelector == 3)
                {
                    isBlue = true;

                    isRed = false; isGreen = false; isYellow = false;

                    anim.SetBool("isBlue", true);

                    anim.SetBool("isRed", false); anim.SetBool("isGreen", false); anim.SetBool("isYellow", false);
                    Debug.Log("ISblue");
                }

                if (colorSelector == 4)
                {
                    isYellow = true;

                    isRed = false; isGreen = false; isBlue = false;

                    anim.SetBool("isYellow", true);

                    anim.SetBool("isBlue", false); anim.SetBool("isGreen", false); anim.SetBool("isRed", false);
                    Debug.Log("ISyellow");
                }

                yield return new WaitForSeconds(.1f);

                endRandomizer = endRandomizer - 1;

                StartCoroutine(ColorRandomizer());
            }

            if (endRandomizer == 0)
            {
                endRandomizer = 14;
            }
        }
    }

    public bool hitRed;
    public bool hitGreen;
    public bool hitBlue;
    public bool hitYellow;

    void ColorMatch()
    {
        if (hitRed == true && isRed == false)
        {
            Destroy(gameObject);
        }

        if (hitGreen == true && isGreen == false)
        {
            Destroy(gameObject);
        }

        if (hitBlue == true && isBlue == false)
        {
            Destroy(gameObject);
        }

        if (hitYellow == true && isYellow == false)
        {
            Destroy(gameObject);
        }
    }
}
