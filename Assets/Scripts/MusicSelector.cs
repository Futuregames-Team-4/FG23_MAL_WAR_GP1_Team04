using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSelector : MonoBehaviour
{
    public AudioSource inGameMusic;
    public AudioSource battleMusic;

    private void OnEnable()
    {
        inGameMusic.Play();
    }

    public void PlayInGameMusic()
    {
        battleMusic.Stop();
        inGameMusic.Play();
    }

    public void PlayBattleMusic()
    {
        inGameMusic.Stop();
        battleMusic.Play();
    }
}
