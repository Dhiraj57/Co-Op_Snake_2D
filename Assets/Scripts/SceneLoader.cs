﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private SnakeHandler snake;
    [SerializeField] private FoodSpawner food;

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
        yield break;
    }
}
