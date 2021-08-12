using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    private FoodSpawner foodSpawn;
    [SerializeField] private SnakeHandler snake;

    private void Start()
    {
        foodSpawn = new FoodSpawner(20, 20);
        snake.Setup(foodSpawn);
        foodSpawn.Setup(snake);
    }
}
