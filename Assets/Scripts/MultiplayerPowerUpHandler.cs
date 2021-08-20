using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerPowerUpHandler : MonoBehaviour
{
    public enum PowerUp
    {
        Shield,
        ScoreBoost,
        SpeedUp
    }

    [SerializeField] private MultiplayerSnakeHandler snake;
    [SerializeField] private MultiplayerSnakeHandler snake2;

    public PowerUp PowerType;
    private Vector2Int powerPosition;

    public Player playerPowerId;

    private int width;
    private int height;

    private void Awake()
    {
        width = 15;
        height = 15;
        PowerType = PowerUp.Shield;
    }

    private void Start()
    {
        StartCoroutine(FoodTimer());
    }

    public void SpawnPowerUp()
    {
        do
        {
            powerPosition = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
        }
        while (snake.GetSnakeGridPositionList().IndexOf(powerPosition) != -1 && snake2.GetSnakeGridPositionList().IndexOf(powerPosition) != -1);

        int select = Random.Range(0, 3);
        switch (select)
        {
            case 0:
                GetComponent<SpriteRenderer>().sprite = GameAssets.Instance.ScoreBoost;
                PowerType = PowerUp.ScoreBoost;
                break;
            case 1:
                GetComponent<SpriteRenderer>().sprite = GameAssets.Instance.Shield;
                PowerType = PowerUp.Shield;
                break;
            case 2:
                GetComponent<SpriteRenderer>().sprite = GameAssets.Instance.SpeedUp;
                PowerType = PowerUp.SpeedUp;
                break;
        }

        transform.position = new Vector3(powerPosition.x, powerPosition.y);
    }

    public bool SnakePowerUp(Vector2Int snakeGridPosition, Player Id)
    {
        playerPowerId = Id;

        if (snakeGridPosition == powerPosition)
        {
            SoundManager.Instance.Play(SoundManager.Sounds.Pickup);

            switch (PowerType)
            {
                default:
                    break;

                case PowerUp.Shield:
                    switch (playerPowerId)
                    {
                        case Player.player1:
                            snake.shield = true;
                            StartCoroutine(Shield(playerPowerId));
                            break;
                        case Player.player2:
                            snake2.shield = true;
                            StartCoroutine(Shield(playerPowerId));
                            break;
                    }      
                    break;

                case PowerUp.ScoreBoost:
                    MultiplayerGameHandler.scoreBoost = true;
                    StartCoroutine(BoostScore());
                    break;

                case PowerUp.SpeedUp:
                    switch (playerPowerId)
                    {
                        case Player.player1:
                            snake.moveTimerMax = 0.07f;
                            StartCoroutine(SpeedUp(playerPowerId));
                            break;
                        case Player.player2:
                            snake2.moveTimerMax = 0.07f;
                            StartCoroutine(SpeedUp(playerPowerId));
                            break;
                    }                   
                    break;
            }

            SpawnPowerUp();
            return false;
        }
        else return false;
    }

    public IEnumerator FoodTimer()
    {
        while (true)
        {
            SpawnPowerUp();
            yield return new WaitForSeconds(Random.Range(4, 8));
        }
    }

    public IEnumerator BoostScore()
    {
        yield return new WaitForSeconds(3);
        MultiplayerGameHandler.scoreBoost = false;
        yield break;
    }

    public IEnumerator SpeedUp(Player Id)
    {
        yield return new WaitForSeconds(3);

        if(Id == Player.player1)
        {
            snake.moveTimerMax = 0.2f;
        }
        else if(Id == Player.player2)
        {
            snake2.moveTimerMax = 0.2f;
        }
        else
        {
            snake.moveTimerMax = 0.2f;
            snake2.moveTimerMax = 0.2f;
        }

        yield break;
    }

    public IEnumerator Shield(Player Id)
    {
        yield return new WaitForSeconds(3);

        switch (Id)
        {
            default:
                snake.shield = false;
                snake2.shield = false;
                break;
            case Player.player1:
                snake.shield = false;
                break;
            case Player.player2:
                snake2.shield = false;
                break;
        }

        yield break;
    }
}
