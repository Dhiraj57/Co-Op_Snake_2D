using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject object1;
    [SerializeField] private GameObject object2;
    [SerializeField] private GameObject object3;

    private void Awake()
    {
        SoundManager.Instance.PlayMusic(SoundManager.Sounds.Music);
    }


    public void Quit()
    {
        Application.Quit();
    }

    public void ModeSelect()
    {
        OnMouseEnter();
        SoundManager.Instance.Play(SoundManager.Sounds.ButtonClick);
        object1.SetActive(false);
        object2.SetActive(true);
        object3.SetActive(false);
    }

    public void BackToMenu()
    {
        OnMouseEnter();
        SoundManager.Instance.Play(SoundManager.Sounds.ButtonClick);
        object1.SetActive(true);
        object2.SetActive(false);
        object3.SetActive(false);
    }

    public void HowToPlay()
    {
        SoundManager.Instance.Play(SoundManager.Sounds.ButtonClick);
        object1.SetActive(false);
        object2.SetActive(false);
        object3.SetActive(true);
    }

    private void OnMouseEnter()
    {
        SoundManager.Instance.Play(SoundManager.Sounds.Switch);
    }

}
