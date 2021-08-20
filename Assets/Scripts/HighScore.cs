using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour
{
    private int score;
    public TextMeshProUGUI highScore;

    private void Awake()
    {
        highScore.text = "Highscore : " + PlayerPrefs.GetInt("HighScore").ToString();
    }

    private void Update()
    {
        score = GameHandler.GetScore();

        if(score > PlayerPrefs.GetInt("HighScore"))
        {       
            SetHighScore();
        }    
    }

    private void SetHighScore()
    {
        PlayerPrefs.SetInt("HighScore", score);
        highScore.text = "Highscore : " + PlayerPrefs.GetInt("HighScore").ToString();
    }
}
