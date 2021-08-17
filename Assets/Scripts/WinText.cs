using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI winText;

    public void SetWinText(int player)
    {
        switch (player)
        {
            case 0:
                winText.text = "! Try Again !";
                break;
            case 1:
                winText.text = "! Player-1 Won !" ;
                break;
            case 2:
                winText.text = "! Player-2 Won !" ;
                break;
        }
    }
}
