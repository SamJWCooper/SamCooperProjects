using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 movement;
    public float moveSpeed = 0.7f;
    public float quickMoveSpeed = 0.9f;
    float currentMoveSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentMoveSpeed = moveSpeed;
    }

    void Update()
    {
        if (Input.GetKeyDown("x"))
        {
            if (currentMoveSpeed == moveSpeed)
            {
                currentMoveSpeed = quickMoveSpeed;
            }

            else if (currentMoveSpeed == quickMoveSpeed)
            {
                currentMoveSpeed = moveSpeed; 
            }
            Debug.Log(currentMoveSpeed);
        }
    }

    void FixedUpdate()
    {
        var input = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        Vector3 velocity = input.normalized * currentMoveSpeed; 
        transform.position += velocity * Time.deltaTime;
    }
}
