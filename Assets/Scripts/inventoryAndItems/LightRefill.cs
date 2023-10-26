using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRefill : MonoBehaviour
{
    [SerializeField]
    GameObject objectToInactivate;
    int amountToRefill = 10;

    private PlayerStats playerStats;

    private void Start() {
        //playerStats = GetComponent<PlayerStats>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Hitbox")
        {
            //FuelConsumption fuel = other.transform.parent.GetComponent<FuelConsumption>();
            PlayerStats playerStats = other.transform.parent.GetComponent<PlayerStats>();
            Debug.Log("PICK UP MF");
            
            playerStats.Refuel(amountToRefill);

            objectToInactivate.SetActive(false);
        }
    }
}
