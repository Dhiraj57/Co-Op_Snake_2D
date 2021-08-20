using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_ButtonHover : MonoBehaviour
{
    public void PlayHoverSound()
    {
        SoundManager.Instance.Play(SoundManager.Sounds.Switch);
    }

    private void OnMouseEnter()
    {
        SoundManager.Instance.Play(SoundManager.Sounds.Switch);
    }
}
