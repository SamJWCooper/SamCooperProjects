using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarScript : MonoBehaviour
{
    float moveSpeed;

    Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-100f, 0.0f);
    }
    void FixedUpdate()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }
}
