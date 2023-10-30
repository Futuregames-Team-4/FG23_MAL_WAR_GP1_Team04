using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionController : MonoBehaviour
{
    PlayerStats playerStats;
    Light lanternLight;
    CapsuleCollider capsuleTrigger;

    private void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
        lanternLight = GetComponentInChildren<Light>();
        capsuleTrigger = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        switch (playerStats.fuel)
        {
            case 0:
                lanternLight.range = 1f;
                capsuleTrigger.radius = 1f;
                break;
            case 1:
            case 2:
            case 3:
                lanternLight.range = 4f;
                capsuleTrigger.radius = 3.5f;
                break;
            case 4:
            case 5:
            case 6:
                lanternLight.range = 7f;
                capsuleTrigger.radius = 4.5f;
                break;
            case 7:
            case 8:
            case 9:
                lanternLight.range = 10f;
                capsuleTrigger.radius = 6f;
                break;
            case 10:
                lanternLight.range = 14f;
                capsuleTrigger.radius = 7f;
                break;
        }
    }
}