using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public enum Food
    {
        Food,
        DeadFood,
    }

    private Vector2Int foodPosition;
    //public GameObject foodGameObject;
    [SerializeField] private SnakeHandler snake;
    private int width;
    private int height;

    private void Awake()
    {
        width = 15;
        height = 15;
    }

    private void Start()
    {
        SpawnFood();
    }

    /*public FoodSpawner(int width, int height)
    {
        this.width = width;
        this.height = height;
    }*/

    /*public void Setup(SnakeHandler snake)
    {
        this.snake = snake;
        //foodGameObject = new GameObject("Food", typeof(SpriteRenderer));
        //foodGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.Instance.foodSprite;
        //Debug.Log("Setup");
        SpawnFood();
    }*/

    public void SpawnFood()
    {
        do
        {
            foodPosition = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
            //Debug.Log(foodPosition);
        } 
        while (snake.GetSnakeGridPositionList().IndexOf(foodPosition)  != -1); 

        //foodGameObject = new GameObject("Food", typeof(SpriteRenderer));
        //Debug.Log(foodGameObject);
        //foodGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.Instance.foodSprite;
        transform.position = new Vector3(foodPosition.x, foodPosition.y);
    }

    public bool EatFood(Vector2Int snakeGridPosition)
    {
        if (snakeGridPosition == foodPosition)
        {
            //Object.Destroy(foodGameObject);
            SpawnFood();
            GameHandler.AddScore();
            return true;
        }
        else return false;
    }

    /*public Vector2Int ValidateGridPosition(Vector2Int gridPosition)
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
    }*/
}
