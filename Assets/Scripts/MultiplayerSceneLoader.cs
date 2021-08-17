using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiplayerSceneLoader : MonoBehaviour
{
    [SerializeField] private MultiplayerSnakeHandler snake;
    [SerializeField] private MultiplayerSnakeHandler snake2;

    public enum Scene
    {
        GameScene,
        Multiplayer,
        Loading
    }

    public void Load()
    {
        StartCoroutine(WaitForUpdate(Scene.Multiplayer));
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
}
