using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets instance;
    public static GameAssets Instance { get { return instance; } }

    public Sprite SnakeHeadSprite;
    public Sprite SnakeHeadSprite2;
    public Sprite FoodSprite;
    public Sprite SnakeBodySprite;
    public Sprite SnakeBodySprite2;
    public Sprite DeadFood;
    public Sprite Shield;
    public Sprite ScoreBoost;
    public Sprite SpeedUp;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
