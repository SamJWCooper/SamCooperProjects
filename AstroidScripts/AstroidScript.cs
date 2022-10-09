using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidScript : MonoBehaviour
{
    public Transform target;
    public float moveSpeed;

    Rigidbody2D rb;
    ShipScript shipScript;

    void Start()
    {
        target = GameObject.FindWithTag("SpaceShip").transform;
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = Random.Range(5f, 9f);

        transform.LookAt(target.position);

        StartCoroutine(Despawner());
        IEnumerator Despawner()
        {
            yield return new WaitForSeconds(20);
            Destroy(gameObject);
        }
    }
    void FixedUpdate()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "SpaceShip")
        {
            shipScript = target.GetComponent<ShipScript>();
            shipScript.Die();
            Debug.Log("DEAD");
        }
    }
}
