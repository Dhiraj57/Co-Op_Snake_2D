using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerFoodHandler : MonoBehaviour
{
    public enum Food
    {
        food,
        deadFood,
    }

    [SerializeField] private MultiplayerSnakeHandler snake;
    [SerializeField] private MultiplayerSnakeHandler snake2;

    public Food foodType;
    public Food getFood = Food.deadFood;
    private Vector2Int foodPosition;

    public Player playerFoodId;

    private int width;
    private int height;

    private void Awake()
    {
        width = 15;
        height = 15;
        foodType = Food.food;
    }

    private void Start()
    {
        StartCoroutine(FoodTimer());
    }

    public void SpawnFood()
    {
        do
        {
            foodPosition = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
        }
        while (snake.GetSnakeGridPositionList().IndexOf(foodPosition) != -1 && snake2.GetSnakeGridPositionList().IndexOf(foodPosition) != -1);

        if (snake.GetSnakeSize() > 1)
        {
            int select = Random.Range(0, 3);
            switch (select)
            {
                default:
                    GetComponent<SpriteRenderer>().sprite = GameAssets.Instance.FoodSprite;
                    foodType = Food.food;
                    break;
                case 0:
                    GetComponent<SpriteRenderer>().sprite = GameAssets.Instance.FoodSprite;
                    foodType = Food.food;
                    break;
                case 1:
                    GetComponent<SpriteRenderer>().sprite = GameAssets.Instance.DeadFood;
                    foodType = Food.deadFood;
                    break;
            }
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = GameAssets.Instance.FoodSprite;
            foodType = Food.food;
        }

        transform.position = new Vector3(foodPosition.x, foodPosition.y);
    }

    public bool EatFood(Vector2Int snakeGridPosition, Player Id)
    {
        playerFoodId = Id;

        if (snakeGridPosition == foodPosition)
        {
            switch (foodType)
            {
                default:
                    MultiplayerGameHandler.AddScore(playerFoodId);
                    switch(playerFoodId)
                    {
                        case Player.player1:
                            snake.deadFood = false;
                            break;
                        case Player.player2: 
                            snake2.deadFood = false;
                            break;
                    }
                    break;

                case Food.food:
                    //snake.deadFood = false;
                    MultiplayerGameHandler.AddScore(playerFoodId);
                    switch (playerFoodId)
                    {
                        case Player.player1:
                            snake.deadFood = false;
                            break;
                        case Player.player2:
                            snake2.deadFood = false;
                            break;
                    }
                    break;

                case Food.deadFood:
                    MultiplayerGameHandler.SubtractScore(playerFoodId);
                    switch (playerFoodId)
                    {
                        case Player.player1:
                            snake.deadFood = true;
                            break;
                        case Player.player2:
                            snake2.deadFood = true;
                            break;
                    }
                    //snake.deadFood = true;
                    break;
            }

            SpawnFood();
            return true;
        }
        else return false;
    }

    public IEnumerator FoodTimer()
    {
        while (true)
        {
            SpawnFood();
            yield return new WaitForSeconds(4f);
        }
    }

}
