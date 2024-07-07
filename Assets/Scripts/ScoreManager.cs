using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public int scoreToReach; // score the player reach to win (for game to end)
    private int player1Score = 0;
    private int player2Score = 0;

    public Text player1ScoreText;
    public Text player2ScoreText;

    public void Player1Goal()
    {
        player1Score++;
        player1ScoreText.text = player1Score.ToString(); // was int -> turn into string to pass through Text function
        CheckScore();
    }

    public void Player2Goal()
    {
        player2Score++;
        player2ScoreText.text = player2Score.ToString();
        CheckScore();

    }

    private void CheckScore()
    {
        // if player 1 or player 2 reach the score to win -> loads Game over scene
        if(player1Score == scoreToReach || player2Score == scoreToReach)
        {
            SceneManager.LoadScene(2); // Game over scene appears
        }
    }
}
