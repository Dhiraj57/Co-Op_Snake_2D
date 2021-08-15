using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    private static GameHandler instance;

    public static GameHandler Instance { get { return instance; } }

    private static int score;
    public static bool scoreBoost;

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

    public static int GetScore()
    {
        return score;
    }

    public static void AddScore()
    {
        if(scoreBoost)
        {
            score += 20;
        }
        else
        {
            score += 10;
        }
        
    }

    public static void SubtractScore()
    {
        if(score > 0)
        {
            score -= 10;
        }    
    }

    private static void InitializeStatic()
    {
        score = 0;
    }
}
