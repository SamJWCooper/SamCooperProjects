using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawnerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject road;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Road")
        {
            Debug.Log("Is Touching Road");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Road")
        {
            Debug.Log("Spawn Road");
            Instantiate(road, transform.position, transform.rotation);
        }

    }
}
