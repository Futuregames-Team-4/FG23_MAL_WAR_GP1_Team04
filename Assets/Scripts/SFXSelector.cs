using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXSelector : MonoBehaviour
{
    public AudioSource enemyTurn;
    public AudioSource loseFuel;
    public AudioSource useItem;

    public void PlayEnemyTurn()
    {
        enemyTurn.Play();
    }

    public void PlayLoseFuel()
    {
        loseFuel.Play();
    }

    public void PlayUseItem()
    {
        useItem.Play();
    }
}
