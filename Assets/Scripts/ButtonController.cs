using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public Button button;
    public string SceneName;

    private void Awake()
    {
        button.onClick.AddListener(SceneLoader);
    }

    private void SceneLoader()
    {
        SoundManager.Instance.Play(SoundManager.Sounds.ButtonClick);
        SceneManager.LoadScene(SceneName);
    }

}
