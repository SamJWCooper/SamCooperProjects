using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRangeColliderScript : MonoBehaviour
{
    public GameObject duelistEnemy;
    DuelistEnemyScript duelistEnemyScript;

    

    void Start()
    {
        duelistEnemyScript = duelistEnemy.GetComponent<DuelistEnemyScript>();
    }

    void Update()
    {

    }

    bool isInCollider;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            duelistEnemyScript.isInAttackRange = true;
            //Debug.Log(duelistEnemyScript.isInAttackRange);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            duelistEnemyScript.isInAttackRange = false;
            //Debug.Log(duelistEnemyScript.isInAttackRange);
        }
    }



}

