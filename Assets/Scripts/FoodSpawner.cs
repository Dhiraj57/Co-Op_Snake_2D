using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner 
{
    private Vector2Int foodPosition;
    private GameObject foodGameObject;
    private SnakeHandler snake;
    private int width;
    private int height;

    public FoodSpawner(int width, int height)
    {
        this.width = width;
        this.height = height;
    }

    public void Setup(SnakeHandler snake)
    {
        this.snake = snake;
        SpawnFood();
    }

    private void SpawnFood()
    {
        do
        {
            foodPosition = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
        } 
        while (snake.GetGridPosition() == foodPosition);
       

        foodGameObject = new GameObject("Food", typeof(SpriteRenderer));
        foodGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.Instance.foodSprite;
        foodGameObject.transform.position = new Vector3(foodPosition.x, foodPosition.y);
    }

    public void EatFood(Vector2Int snakeGridPosition)
    {
        if(snakeGridPosition == foodPosition)
        {
            Object.Destroy(foodGameObject);
            SpawnFood();
        }
    }
}
