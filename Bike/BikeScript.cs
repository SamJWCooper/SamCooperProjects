using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeScript : MonoBehaviour
{
    Rigidbody rb;
    public GameObject bike;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Steering();
    }

    float acceleration;
    float maxSpeed = 60; 

    // increase & decrease acceleration control
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (acceleration < maxSpeed)
            {
                StartCoroutine(AccelTimer());
                IEnumerator AccelTimer()
                {
                    acceleration += 0.5f;
                    yield return new WaitForSeconds(0.2f);
                }
            }
        }

        if (acceleration > 0)
        {
            transform.Translate(Vector3.forward * acceleration * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (acceleration > 0)
            {
                acceleration += -0.6f;
            }
        }
    }

    public BoxCollider screenCenter;
    float steerAmount;
    bool mouseOnLeft;
    public GameObject bikeModel;

    // destroy if tree hit
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Tree")
        {
            Destroy(gameObject);
        }
    }

    // detect distance from center of the screen to mouse to determine how far to steer left and right
    void Steering()
    {
        Vector3 mousePos = Input.mousePosition;

        // distance from mouse to scren center
        float distance = Vector3.Distance(Input.mousePosition, screenCenter.GetComponent<Collider>().ClosestPointOnBounds(Input.mousePosition));
        var playerScreenPoint = Camera.main.WorldToScreenPoint(bike.transform.position);

        // check which side of the screen mouse is on
        if (mousePos.x < playerScreenPoint.x)
        {
            mouseOnLeft = true;
        }

        if (mousePos.x > playerScreenPoint.x)
        {
            mouseOnLeft = false;
        }

        steerAmount = distance / 8;

        // steer to left and right
        if (acceleration > 0 && mouseOnLeft == true)
        {
            transform.Rotate(-Vector3.up, Time.deltaTime * steerAmount, Space.World);

            bikeModel.transform.Rotate(Vector3.forward, Time.deltaTime * steerAmount, Space.World);
        }

        if (acceleration > 0 && mouseOnLeft == false)
        {
            transform.Rotate(Vector3.up, Time.deltaTime * steerAmount, Space.World);

            bikeModel.transform.Rotate(-Vector3.forward, Time.deltaTime * steerAmount, Space.World);
        }
    }
}
