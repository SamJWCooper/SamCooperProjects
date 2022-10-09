using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelistScript : MonoBehaviour
{
    Animator anim;
    GameObject swordCol;
    GameObject blockCol;
    DuelistEnemyScript enemyScript;
    GameManager gameManager;

    public float movementSpeed = 5f;
    public bool isBlocking;
    public bool isDead;
    public bool isSlashing;

    void Start()
    {
        this.name = "Duelist";

        anim = GetComponent<Animator>();
        enemyScript = GameObject.FindWithTag("Enemy").GetComponent<DuelistEnemyScript>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        swordCol = this.gameObject.transform.GetChild(0).gameObject;
        blockCol = this.gameObject.transform.GetChild(1).gameObject;

        swordCol.SetActive(false);
        blockCol.SetActive(false); 
    }

    void Update()
    {
        // left and right movement control 
        var movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * movementSpeed;

        // left click to slash
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("isSlashing");

            StartCoroutine(SwordCollider());
            IEnumerator SwordCollider()
            {
                enemyScript.SwordBlockController();
                isSlashing = true;
                yield return new WaitForSeconds(0.6f);

                if (isBlocking == false)
                {
                    swordCol.SetActive(true);
                }

                yield return new WaitForSeconds(0.05f);
                swordCol.SetActive(false);
                isSlashing = false;
            }
        }

        // hold right click to block
        if (Input.GetMouseButtonDown(1))
        {
            anim.SetBool("isBlocking", true);
            isBlocking = true;
            blockCol.SetActive(true);
        }

        if (Input.GetMouseButtonUp(1))
        {
            anim.SetBool("isBlocking", false);
            isBlocking = false;
            blockCol.SetActive(false);
        }
    }

    // detect if hit by enemy sword
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "EnemySword" && isBlocking == false)
        {
            isDead = true;
            gameManager.AddScore();
            Debug.Log("DEAD 2");
        }
    }
}
