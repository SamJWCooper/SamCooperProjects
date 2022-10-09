using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidSpawnerScript : MonoBehaviour
{
    public GameObject astroid;

    int randPosSelect;
    bool isSpawning;

    // random timer delay between spawning
    void Update()
    {
        if (isSpawning == false)
        {
            StartCoroutine(AstroidSpawner());
            IEnumerator AstroidSpawner()
            {
                isSpawning = true;
                yield return new WaitForSeconds(Random.Range(0.5f, 2));
                SelectRandomPos();
            }
        }
    }

    public bool is1; public bool is2; public bool is3; public bool is4; public bool is5; public bool is6; public bool is7; public bool is8;

    // select postion to spawn astroid randomly
    void SelectRandomPos()
    {
        randPosSelect = Random.Range(0, 9);

        isSpawning = false;

        if (randPosSelect == 1)
        {
            if (is1 == true)
            {
                Instantiate(astroid, transform.position, transform.rotation);
            }
        }

        if (randPosSelect == 2)
        {
            if (is2 == true)
            {
                Instantiate(astroid, transform.position, transform.rotation);
            }
        }

        if (randPosSelect == 3)
        {
            if (is3 == true)
            {
                Instantiate(astroid, transform.position, transform.rotation);
            }
        }

        if (randPosSelect == 4)
        {
            if (is4 == true)
            {
                Instantiate(astroid, transform.position, transform.rotation);
            }
        }

        if (randPosSelect == 5)
        {
            if (is5 == true)
            {
                Instantiate(astroid, transform.position, transform.rotation);
            }
        }

        if (randPosSelect == 6)
        {
            if (is6 == true)
            {
                Instantiate(astroid, transform.position, transform.rotation);
            }
        }

        if (randPosSelect == 7)
        {
            if (is6 == true)
            {
                Instantiate(astroid, transform.position, transform.rotation);
            }
        }

        if (randPosSelect == 8)
        {
            if (is8 == true)
            {
                Instantiate(astroid, transform.position, transform.rotation);
            }
        }

    }
}

