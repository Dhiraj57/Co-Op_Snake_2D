using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUPHandler : MonoBehaviour
{
    public enum PowerUp
    {
        Shield,
        ScoreBoost,
        SpeedUp
    }

    public PowerUp PowerType;
    private Vector2Int powerPosition;
    [SerializeField] private SnakeHandler snake;
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
        while (snake.GetSnakeGridPositionList().IndexOf(powerPosition)  != -1);

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

    public bool SnakePowerUp(Vector2Int snakeGridPosition)
    {

        if (snakeGridPosition == powerPosition)
        {
            switch (PowerType)
            {
                default:
                    break;
                case PowerUp.Shield:
                    snake.shield = true;
                    StartCoroutine(Shield());
                    break;
                case PowerUp.ScoreBoost:
                    GameHandler.scoreBoost = true;
                    StartCoroutine(BoostScore());
                    break;
                case PowerUp.SpeedUp:
                    snake.moveTimerMax = 0.07f;
                    StartCoroutine(SpeedUp());
                    break;
            }

            SpawnPowerUp();
            return false;
        }
        else return false;
    }

    public IEnumerator FoodTimer()
    {
        while(true)
        {
            SpawnPowerUp();
            yield return new WaitForSeconds(Random.Range(4,8));       
        }
    }

    public IEnumerator BoostScore()
    {
        yield return new WaitForSeconds(3);
        GameHandler.scoreBoost = false;
        yield break;
    }

    public IEnumerator SpeedUp()
    {
        yield return new WaitForSeconds(3);
        snake.moveTimerMax = 0.2f;
        yield break;
    }

    public IEnumerator Shield()
    {
        yield return new WaitForSeconds(3);
        snake.shield = false;
        yield break;
    }
}
