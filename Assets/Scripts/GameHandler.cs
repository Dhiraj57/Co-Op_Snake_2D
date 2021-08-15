using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    private static GameHandler instance;

    public static GameHandler Instance { get { return instance; } }

    private static int score;

    [SerializeField] private FoodSpawner foodSpawn;
    [SerializeField] private SnakeHandler snake;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        InitializeStatic();
    }

    private void Start()
    {
        //foodSpawn = new FoodSpawner(20, 20);
        //snake.Setup(foodSpawn);
        //foodSpawn.Setup(snake);
    }

    /*public void ResetGameHandler()
    {
        InitializeStatic();
        //foodSpawn = new FoodSpawner(20, 20);
        //Start();
    }*/

    public static int GetScore()
    {
        return score;
    }

    public static void AddScore()
    {
        score += 10;
    }

    private static void InitializeStatic()
    {
        score = 0;
    }
}
