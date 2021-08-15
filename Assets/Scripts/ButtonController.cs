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
        SceneManager.LoadScene(SceneName);
    }

}
