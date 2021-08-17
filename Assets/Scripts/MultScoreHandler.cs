using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultScoreHandler : MonoBehaviour
{
    [SerializeField] private Text scoreText1;
    [SerializeField] private Text scoreText2;
    [SerializeField] private MultiplayerSnakeHandler snake;
    [SerializeField] private MultiplayerSnakeHandler snake2;

    private void Awake()
    {
        //scoreText = this.GetComponent<Text>();

        //if(snake.playerId == 0)
        {
            //scoreText1 = this.GetComponent<Text>();
        }
        //else
        {
            //scoreText2 = this.GetComponent<Text>();
        }
    }

    private void Update()
    {
        scoreText1.text = MultiplayerGameHandler.GetScore(snake.playerId).ToString();
        scoreText2.text = MultiplayerGameHandler.GetScore(snake2.playerId).ToString(); 
    }
}
