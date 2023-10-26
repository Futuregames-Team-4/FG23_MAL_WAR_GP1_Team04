using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItem : MonoBehaviour
{
    [SerializeField] GameObject objectToInactivate;
    [SerializeField] GameObject objectToInactivate1;
    [SerializeField] GameObject objectToInactivate2;
    [SerializeField] FuelConsumption fuel;
    [SerializeField] PlayerStats playerStats;

    private void Start() {
        playerStats = GetComponent<PlayerStats>();
    }
    public void UseItem1()
    {
        playerStats = GetComponent<PlayerStats>();
        playerStats.Refuel(playerStats.amountToRefill);
        objectToInactivate.SetActive(false);
    }
    public void UseItem2()
    {
        objectToInactivate1.SetActive(false);
    }
    public void UseItem3()
    {
        objectToInactivate2.SetActive(false);
    }
}
