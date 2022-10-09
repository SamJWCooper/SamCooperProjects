using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarController : MonoBehaviour
{
    Rigidbody2D rb;
    public float steeringSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = transform.up * steeringSpeed;
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            rb.velocity = transform.up * 0;
        }

        if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = -transform.up * steeringSpeed;
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            rb.velocity = -transform.up * 0;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "CrashCol")
        {
            Debug.Log("Destroy Dead");
            Destroy(gameObject);
        }
    }
}
