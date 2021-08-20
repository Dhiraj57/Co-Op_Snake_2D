using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiplayerSceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject gameHandler;
    [SerializeField] private GameObject pauseWindow;
    [SerializeField] private MultiplayerSnakeHandler snake;
    [SerializeField] private MultiplayerSnakeHandler snake2;

    private bool isPaused = false;

    private void Update()
    {
        if (snake.state == State.Alive)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!isPaused)
                {
                    PauseGame();
                }
                else
                {
                    ResumeGame();
                }
            }
        }
    }

    public enum Scene
    {
        MainMenu,
        GameScene,
        Multiplayer,
        Loading
    }

    public void Load()
    {
        SoundManager.Instance.Play(SoundManager.Sounds.ButtonClick);
        StartCoroutine(WaitForUpdate(Scene.Multiplayer));
    }

    public void Menu()
    {
        SoundManager.Instance.Play(SoundManager.Sounds.ButtonClick);
        SceneManager.LoadScene(Scene.MainMenu.ToString());
        Destroy(gameHandler);
    }

    private IEnumerator WaitForUpdate(Scene scene)
    {
        SceneManager.LoadScene(Scene.Loading.ToString());
        yield return new WaitForSeconds(1);

        snake.ResetPlayer();
        snake2.ResetPlayer();
        SceneManager.LoadScene(scene.ToString());
        yield break;
    }

    public void PauseGame()
    {
        SoundManager.Instance.Play(SoundManager.Sounds.ButtonClick);
        isPaused = true;
        pauseWindow.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        SoundManager.Instance.Play(SoundManager.Sounds.ButtonClick);
        isPaused = false;
        Time.timeScale = 1f;
        pauseWindow.SetActive(false);
    }
}
