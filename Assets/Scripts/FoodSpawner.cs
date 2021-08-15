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
        while (snake.GetSnakeGridPositionList().IndexOf(foodPosition)  != -1);
       

        foodGameObject = new GameObject("Food", typeof(SpriteRenderer));
        foodGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.Instance.foodSprite;
        foodGameObject.transform.position = new Vector3(foodPosition.x, foodPosition.y);
    }

    public bool EatFood(Vector2Int snakeGridPosition)
    {
        if (snakeGridPosition == foodPosition)
        {
            Object.Destroy(foodGameObject);
            SpawnFood();
            return true;
        }
        else return false;
    }

    public Vector2Int ValidateGridPosition(Vector2Int gridPosition)
    {
        if(gridPosition.x < 0)
        {
            gridPosition.x = width ;
        }
        if (gridPosition.x > width )
        {
            gridPosition.x = 0;
        }
        if (gridPosition.y < 0)
        {
            gridPosition.y = height ;
        }
        if (gridPosition.y > height )
        {
            gridPosition.y = 0;
        }
        return gridPosition;
    }
}
