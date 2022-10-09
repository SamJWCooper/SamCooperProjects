using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadScript : MonoBehaviour
{
    float moveSpeed = 50f;

    void Start()
    {
        target = new Vector2(-150f, 0.0f);
        position = gameObject.transform.position;
        this.transform.Rotate(0.0f, 0.0f, 90.0f, Space.World);
    }

    private Vector2 target;
    private Vector2 position;

    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
    }
}
