using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 movement;

    PlayerAccuracyScript accuracyScript;

    public float aimSpeed;
    public float moveSpeed;

    public bool isImmobile;
    public bool isAiming;
    public float currentSpeed;

    void Start()
    {
        accuracyScript = GetComponent<PlayerAccuracyScript>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        AssignCurrentMovementType();

        if (speed == 0)
        {
            isImmobile = true;
        }

        if (speed > 0)
        {
            isImmobile = false;

            if (isAiming == true)
            {
                currentSpeed = aimSpeed;
            }

            if (isAiming == false)
            {
                currentSpeed = moveSpeed;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            isAiming = !isAiming;
        }

        if (Input.GetMouseButtonUp(1))
        {
            isAiming = !isAiming;
        }
    }

    public string moveType;

    void AssignCurrentMovementType()
    {
        // immobile & not aiming
        if (isImmobile == true && isAiming == false)
        {
            moveType = "immobile";
        }
        // immobile & aiming
        if (isImmobile == true && isAiming == true)
        {
            moveType = "immobileAiming";
        }
        // moving & not aiming
        if (isImmobile == false && isAiming == false)
        {
            moveType = "moving";
        }
        // moving & aiming
        if (isImmobile == false && isAiming == true)
        {
            moveType = "movingAiming";
        }
    }


    Vector3 lastPosition = Vector3.zero;
    float speed;

    void FixedUpdate()
    {
        // movement
        var input = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        Vector3 velocity = input.normalized * currentSpeed;
        transform.position += velocity * Time.deltaTime;

        // speed varible assignment
        speed = (transform.position - lastPosition).magnitude;
        lastPosition = transform.position;
    }
}
