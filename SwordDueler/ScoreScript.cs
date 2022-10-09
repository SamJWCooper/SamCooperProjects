using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public Text enemyScore;
    public Text playerScore;

    GameManager gameManager;

    void Start()
    {
        enemyScore = GameObject.FindWithTag("EnemyScore").GetComponent<UnityEngine.UI.Text>();
        playerScore = GameObject.FindWithTag("PlayerScore").GetComponent<UnityEngine.UI.Text>();
        

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    public void UpdateScoreText()
    {
        playerScore.text = gameManager.playerScore.ToString();
        enemyScore.text = gameManager.enemyScore.ToString();
    }
}
