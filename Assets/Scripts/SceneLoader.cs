using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    //public static SceneLoader instance;
    [SerializeField] private SnakeHandler snake;
    [SerializeField] private FoodSpawner food;

    private void Awake()
    {
       // if (instance != null)
        {
        //    Destroy(gameObject);
        }
       // else
        {
            //instance = this;
        //    DontDestroyOnLoad(gameObject);
        }
    }

    public enum Scene
    {
        GameScene,
        Loading
    }

    public void Load()
    {
        StartCoroutine(WaitForUpdate(Scene.GameScene));           
    }

    private IEnumerator WaitForUpdate(Scene scene)
    {
        SceneManager.LoadScene(Scene.Loading.ToString());
        yield return new WaitForSeconds(1);
        snake.ResetPlayer();
        SceneManager.LoadScene(scene.ToString());
        //food.SpawnFood();
        yield break;
    }
}
