using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLineScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    float timer;
    bool hitFinishLine;

    // Update is called once per frame
    void Update()
    {
        if (hitFinishLine == false)
        {
            timer += Time.deltaTime;
            Debug.Log(timer);
        }

        if (hitFinishLine == true)
        {
            Debug.Log(timer);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        hitFinishLine = true;
    }
}
