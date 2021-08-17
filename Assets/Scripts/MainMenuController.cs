using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject object1;
    [SerializeField] private GameObject object2;
    [SerializeField] private GameObject object3;


    public void Quit()
    {
        Application.Quit();
    }

    public void ModeSelect()
    {
        object1.SetActive(false);
        object2.SetActive(true);
        object3.SetActive(false);
    }

    public void BackToMenu()
    {
        object1.SetActive(true);
        object2.SetActive(false);
        object3.SetActive(false);
    }

    public void HowToPlay()
    {
        object1.SetActive(false);
        object2.SetActive(false);
        object3.SetActive(true);
    }


}
