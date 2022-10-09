using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneSpawnerScript : MonoBehaviour
{
    public bool isLane1; public bool isLane2; public bool isLane3; public bool isLane4;

    bool isSpawning;
    int randLaneSelect;
    public GameObject car;

    // car spawn delay
    void Update()
    {
        if (isSpawning == false)
        {
            StartCoroutine(CarSpawner());
            IEnumerator CarSpawner()
            {
                isSpawning = true;
                yield return new WaitForSeconds(Random.Range(0.45f, .50f));
                SelectLane();
            }
        }
    }

    // randomly select lane to spawn car
    void SelectLane()
    {
        randLaneSelect = Random.Range(0, 5);
        isSpawning = false;

        if (randLaneSelect == 1)
        {
            if (isLane1)
            {
                Instantiate(car, transform.position, transform.rotation);
            }
        }

        if (randLaneSelect == 2)
        {
            if (isLane2)
            {
                Instantiate(car, transform.position, transform.rotation);
            }
        }

        if (randLaneSelect == 3)
        {
            if (isLane3)
            {
                Instantiate(car, transform.position, transform.rotation);
            }
        }

        if (randLaneSelect == 4)
        {
            if (isLane4)
            {
                Instantiate(car, transform.position, transform.rotation);
            }
        }
    }
}
