using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSelector : MonoBehaviour
{
    public AudioSource inGameMusic;
    public AudioSource battleMusic;

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
