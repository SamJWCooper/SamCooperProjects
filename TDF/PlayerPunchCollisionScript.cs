using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPunchCollisionScript : MonoBehaviour
{
    public bool isHitting;

    PlayerPunchHitScript hitScript;
    PlayerPunchPathScript pathHitScript;

    void Start()
    {
        hitScript = this.gameObject.transform.parent.transform.parent.GetChild(0).GetComponent<PlayerPunchHitScript>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            //Debug.Log("Hit Enemy");
            isHitting = true;
            hitScript.closestPoint = other.gameObject.transform.parent.GetChild(4).GetComponent<PlayerPunchPoints>().closestPoint;
            hitScript.HitCheck();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            //Debug.Log("No Longer Hit");
            isHitting = false;
        }
    }
}
