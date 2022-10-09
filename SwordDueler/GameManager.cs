using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public DuelistScript player;
    public DuelistEnemyScript enemy;

    public DuelistSpawner spawner1;
    public DuelistSpawner spawner2;

    public Text winLoseText;

    ScoreScript scoreScript;

    public float playerScore;
    public float enemyScore;
    bool isPlayerWin;

    void Start()
    {
        player = GameObject.Find("Duelist").GetComponent<DuelistScript>(); 
        enemy = GameObject.Find("Duelist Enemy").GetComponent<DuelistEnemyScript>();

        winLoseText = GameObject.FindWithTag("WinLose").GetComponent<UnityEngine.UI.Text>();

        scoreScript = GameObject.FindWithTag("ScoreScript").GetComponent<ScoreScript>();

        spawner1 = GameObject.Find("Spawner 1").GetComponent<DuelistSpawner>();
        spawner2 = GameObject.Find("Spawner 2").GetComponent<DuelistSpawner>();
    }

    // win conditions
    void Update()
    {
        if (playerScore == 5)
        {
            isPlayerWin = true;
            EndGame();
        }

        if (enemyScore == 5)
        {
            isPlayerWin = false;
            EndGame();
        }
    }

    // update scores when enemy or player die
    public void AddScore()
    {
        if (player.isDead == true)
        {
            enemyScore = enemyScore + 1;
            scoreScript.UpdateScoreText();
            ResetLevel(); 
        }

        if (enemy.isDead == true)
        {
            playerScore = playerScore + 1;
            scoreScript.UpdateScoreText();
            ResetLevel();   
        }
    }

    // win or lose text display
    void EndGame()
    {
        if (isPlayerWin == true)
        {
            Debug.Log("Player Won");
            winLoseText.text = "YOU WIN";
        }

        if (isPlayerWin == false)
        {
            Debug.Log("Player Lose");
            winLoseText.text = "YOU LOSE";
        }
    }

    // reset spawn position when player or enemy dies
    public void ResetLevel()
    {
        Debug.Log("RESET");
        Destroy(player.gameObject); Destroy(enemy.gameObject);

        if (playerScore < 5 && enemyScore < 5)
        {
            spawner1.Spawn();
            spawner2.Spawn();
        }
    }
}
