using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelistSpawner : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public DuelistScript player;
    public DuelistEnemyScript enemy;

    GameManager gameManager;

    public bool isPlayer;
    public bool isEnemy;

    void Start()
    {
        player = GameObject.Find("Duelist").GetComponent<DuelistScript>();
        enemy = GameObject.Find("Duelist Enemy").GetComponent<DuelistEnemyScript>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void Spawn()
    {
        if (player.isDead == true || enemy.isDead == true)
        {
            StartCoroutine(StateCoroutine());
            IEnumerator StateCoroutine()
            {
                yield return new WaitForSeconds(.5f);

                if (isPlayer)
                {
                    Debug.Log("SPAWN PLAYER");
                    Instantiate(playerPrefab, transform.position, transform.rotation);
                    gameManager.player = GameObject.FindWithTag("Player").GetComponent<DuelistScript>();
                }

                if (isEnemy)
                {
                    Debug.Log("SPAWN ENEMY");
                    Instantiate(enemyPrefab, transform.position, transform.rotation = Quaternion.Euler(0, -180, 0));
                    gameManager.enemy = GameObject.FindWithTag("Enemy").GetComponent<DuelistEnemyScript>();
                }
            }
        }
    }
}
