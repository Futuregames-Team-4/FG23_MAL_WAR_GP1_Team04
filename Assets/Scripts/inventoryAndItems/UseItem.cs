using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItem : MonoBehaviour
{
    [SerializeField] GameObject objectToInactivate;
    [SerializeField] GameObject objectToInactivate1;
    [SerializeField] GameObject objectToInactivate2;
    [SerializeField] CapsuleCollider capsuleTrigger;
    PlayerStats playerStats;

    private void Start()
    {
        playerStats = GetComponent<PlayerStats>();
    }
    public void UseItem1()
    {
        //fuel = transform.GetComponent<FuelConsumption>();
        Debug.Log("PICK UP MF");
        playerStats.Refuel(playerStats.amountToRefill);
        objectToInactivate.SetActive(false);
    }
    public void UseItem2()
    {
        StartCoroutine(DelayAction(5.0f));
        objectToInactivate1.SetActive(false);
    }
    public void UseItem3()
    {

        objectToInactivate2.SetActive(false);
    }

    private IEnumerator DelayAction(float delayInSeconds)
    {
        capsuleTrigger = GetComponent<CapsuleCollider>();
        capsuleTrigger.radius = 100f;

        Debug.Log("Flash THE WORLD");
        yield return new WaitForSeconds(delayInSeconds);

        // You can add any action you want to perform after the delay here
    }
}
