using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelistEnemyScript : MonoBehaviour
{
    Animator anim;
    GameObject swordCol;
    GameObject blockCol;

    GameObject player;
    DuelistScript playerScript;
    public GameManager gameManager;

    public float movementSpeed = 20f;
    public bool isDead;
    public bool isAttackRange;
    public bool hitRangeCol;
    bool isSlashing;
    bool isBlocking;
    public bool isInAttackRange;
    bool isRetreating;
    bool isAdvancing;
    bool isHoldingDistance;
    float distance;
    int selectState;
    int blockFailChance;
    bool isRunning;
    int canSlash;
    bool playingSlash;


    void Start()
    {
        this.name = "Duelist Enemy";

        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        playerScript = GameObject.FindWithTag("Player").GetComponent<DuelistScript>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        swordCol = this.gameObject.transform.GetChild(0).gameObject;
        blockCol = this.gameObject.transform.GetChild(1).gameObject;

        SwordSlashController();
        MovementStateSelector();

        swordCol.SetActive(false);
        blockCol.SetActive(false);
    }

    void Update()
    {
        if (isDead == false)
        {
            GetDistance();
        }
    }

    void FixedUpdate()
    {
        if (isDead == false)
        {
            AIMovementController();
            Movements();
            Sword();
        }
    }

    // detects when hit by player sword
    void OnTriggerEnter2D(Collider2D other)
    {
        if (isBlocking == false && other.gameObject.tag == "Sword" && isInAttackRange == true)
        {
            isDead = true;
            gameManager.AddScore();
        }
    }

    // measures distance from player to enemy
    public void GetDistance()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
    }

    // randomly selects wheather enemy will hold distance or not
    public void MovementStateSelector()
    {
        StartCoroutine(StateCoroutine());
        IEnumerator StateCoroutine()
        {
            isHoldingDistance = false;
            selectState = Random.Range(1, 3);

            if (selectState == 1)
            {
                isHoldingDistance = false;
                yield return new WaitForSeconds(Random.Range(.5f, 3f));
                MovementStateSelector();
            }

            if (selectState == 2)
            {
                isHoldingDistance = true;
                yield return new WaitForSeconds(Random.Range(2, 8));
                MovementStateSelector();
            }
        }
    }

    
    public void AIMovementController()
    {
       // if player gets too close within a certain distance, always retreat
        if (isHoldingDistance == false && distance < Random.Range(1, 3))
        {
            isRetreating = true;
            isAdvancing = false;
        }

        if (isHoldingDistance == false && distance > Random.Range(3, 8))
        {
            isAdvancing = true;
            isRetreating = false;
        }

        // holding distance 
        else if (isHoldingDistance == true)
        {
            if (distance > 1.5f && distance < 1.6f)
            {
                isRetreating = false;
                isAdvancing = false;
            }

            if (distance > 1.6f)
            {
                isRetreating = false;
                isAdvancing = true;
            }

            if (distance < 1.5f)
            {
                isRetreating = true;
                isAdvancing = false;
            }
        }
    }

    // movement control
    public void Movements()
    {
        if (isAdvancing == true)
        {
            transform.Translate(Vector2.left * movementSpeed * Time.deltaTime);
        }

        if (isRetreating == true)
        {
            transform.Translate(Vector2.right * movementSpeed * Time.deltaTime);
        } 
    }

    // random failure block chance 
    public void SwordBlockController()
    {
        if (isInAttackRange == true && isRunning == false)
        {
            blockFailChance = Random.Range(0, 101);
            isRunning = true;

            if (blockFailChance < 90)
            {
                isBlocking = true;
                Debug.Log("SUCCSESS");
                isRunning = false;
            }

            if (blockFailChance >= 90)
            {
                Debug.Log("FAIL");
                StartCoroutine(BlockDelay());
                IEnumerator BlockDelay()
                {
                    yield return new WaitForSeconds(Random.Range(.8f, 1f));
                    isBlocking = true;
                    isRunning = false;
                }
            }
        } 
    }

    // randomly controls when the enemy will slash
    public void SwordSlashController()
    {
        StartCoroutine(CanSlash());
        IEnumerator CanSlash()
        {
            yield return new WaitForSeconds(Random.Range(.1f, 3f));
            canSlash = Random.Range(0, 11);

            SwordSlashController();
        }

        if (isInAttackRange == true && isBlocking == false && isSlashing == false && playerScript.isBlocking == false && canSlash < 6)
        {
            isSlashing = true;
            SwordSlashController();
        }
    }

    // sword actions 
    public void Sword()
    {
        if (playerScript.isSlashing == false)
        {
            isBlocking = false;
            isRunning = false;
        }

        // slash
        if (isSlashing == true && playingSlash == false)
        {
            playingSlash = true;
            anim.SetTrigger("isSlashing");

            StartCoroutine(SwordCollider());
            IEnumerator SwordCollider()
            {
                yield return new WaitForSeconds(0.6f);

                if (isBlocking == false)
                {
                    Debug.Log("SLASH");
                    swordCol.SetActive(true);
                }

                yield return new WaitForSeconds(0.05f);
                swordCol.SetActive(false);
                isSlashing = false;

                playingSlash = false;
            }
        }

        // blocks
        if (isBlocking == true)
        {
            anim.SetBool("isBlocking", true);
            blockCol.SetActive(true);
        }

        if (isBlocking == false)
        {
            anim.SetBool("isBlocking", false);
            blockCol.SetActive(false);
        }
    }

}
