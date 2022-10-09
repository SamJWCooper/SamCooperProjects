using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public float thrust = 1f;
    public bool isDead;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        FaceMouse();

        if (Input.GetKey(KeyCode.Space))
        {
            Thrust();
        }
    }

    // point ship object in direction of mouse
    void FaceMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2
            (
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y
            );
        transform.up = direction;
    }

    void Thrust()
    {
        rb.AddForce(transform.up * thrust);
    }

    public void Die()
    {
        isDead = true;
        Destroy(gameObject);
    }
}
