﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverWindow : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;

    public void GameOver()
    {
        gameOverUI.SetActive(true); 
    }

    public void Retry()
    {
        gameOverUI.SetActive(false);
    }
}
