using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverWindow : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;

    public void GameOver()
    {
        SoundManager.Instance.Play(SoundManager.Sounds.PlayerDeath);
        gameOverUI.SetActive(true); 
    }

    public void DisableUI()
    {
        gameOverUI.SetActive(false);
    }
}
