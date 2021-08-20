using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject gameHandler;
    [SerializeField] private GameObject pauseWindow;
    [SerializeField] private SnakeHandler snake;
    [SerializeField] private FoodSpawner food;

    private bool isPaused = false;

    public enum Scene
    {
        MainMenu,
        GameScene,
        Multiplayer,
        Loading
    }

    private void Update()
    {
        if(snake.state == State.Alive)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if(!isPaused)
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

    public void Menu()
    {
        SoundManager.Instance.Play(SoundManager.Sounds.ButtonClick);
        SceneManager.LoadScene(Scene.MainMenu.ToString());     
        Destroy(gameHandler);
    }

    public void Load()
    {
        SoundManager.Instance.Play(SoundManager.Sounds.ButtonClick);
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
