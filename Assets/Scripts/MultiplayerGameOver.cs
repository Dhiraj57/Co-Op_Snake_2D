using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerGameOver : MonoBehaviour
{
    public WinText win;
    public GameOverWindow gameOver;
    public MultiplayerSnakeHandler snake;
    public MultiplayerSnakeHandler snake2;

    private int score1;
    private int score2;
    private bool gameOverBool = false;

    private void Update()
    {
        score1 = MultiplayerGameHandler.GetScore(Player.player1);
        score2 = MultiplayerGameHandler.GetScore(Player.player2);

        //Debug.Log(gameOverBool);

        if(!gameOverBool)
        {
            CheckWinCondition();
        }
    }

    public void CheckWinCondition()
    {
        if (score1 >= 60)
        {
            gameOverBool = true;
            StartCoroutine(ResetBool());
            win.SetWinText(1);
            snake.state = State.Dead;
            snake2.state = State.Dead;
            gameOver.GameOver();
        }
        else if (score2 >= 60)
        {
            gameOverBool = true;
            StartCoroutine(ResetBool());
            win.SetWinText(2);
            snake.state = State.Dead;
            snake2.state = State.Dead;
            gameOver.GameOver();
        }
    }

    public IEnumerator ResetBool()
    {
        //Debug.Log(score1);
        yield return new WaitUntil(() => score1 == 0 && score2 == 0);
        gameOverBool = false;
        yield break;
    }
}
