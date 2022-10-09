using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseScript : MonoBehaviour
{
    public float lookSpeed = 5f;

    void FixedUpdate()
    {
        // gets position of mouse and sets it as direction
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, lookSpeed * Time.deltaTime);
    }
}
