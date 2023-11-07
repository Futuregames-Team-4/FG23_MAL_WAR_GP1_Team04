using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FuelSprites : MonoBehaviour
{

    public PlayerStats playerStats;

    private GameObject[] spriteObjects;

    private int previousFuel;

    private void Awake()
    {
        previousFuel = playerStats.fuel;
    }
    void Update()
    {
        if(previousFuel == playerStats.fuel) { return; }

        HideAllSprites();

        transform.GetChild(playerStats.fuel).gameObject.SetActive(true);

        previousFuel = playerStats.fuel;
    }

    private void HideAllSprites()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);   
        }
    }
}
