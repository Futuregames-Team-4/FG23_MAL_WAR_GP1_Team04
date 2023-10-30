using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewUseItem : MonoBehaviour
{
    [SerializeField] GameObject objectToInactivate;
    [SerializeField] GameObject objectToInactivate1;
    [SerializeField] GameObject objectToInactivate2;
    [SerializeField] GameObject objectToInactivate3;
    [SerializeField] GameObject reveal;
    [SerializeField] InGameMenu inGameMenu;
    public EnemyAttack enemy;
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
        inGameMenu.EndBattle();
        objectToInactivate.SetActive(false);
    }
    public void UseItem2()
    {
        StartCoroutine(DelayAction());
        inGameMenu.EndBattle();
        objectToInactivate1.SetActive(false);
    }
    public void UseItem3()
    {
        inGameMenu.EndBattle();
        objectToInactivate2.SetActive(false);
    }
    public void UseItem4()
    {
        enemy.Cloack();
        inGameMenu.EndBattle();
        objectToInactivate3.SetActive(false);
        
    }

    private IEnumerator DelayAction()
    {
        reveal.transform.position = new Vector3(-1.85000002f, 2.11999989f, -3.8900001f);


        Debug.Log("Flash THE WORLD");
        yield return new WaitForSeconds(3f);
        reveal.transform.position = new Vector3(-100.5f, -0.150000036f, -6.25f);
        Debug.Log("do something");


        // You can add any action you want to perform after the delay here
    }
}