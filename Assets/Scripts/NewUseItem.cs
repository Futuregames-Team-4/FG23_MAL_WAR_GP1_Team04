using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewUseItem : MonoBehaviour
{
    [SerializeField] GameObject fuel;
    [SerializeField] GameObject flare;
    [SerializeField] GameObject shield;
    [SerializeField] GameObject cloack;
    [SerializeField] GameObject reveal;
    [SerializeField] InGameMenu inGameMenu;
    [SerializeField] SFXSelector sfxSelector;
    public EnemyAttack enemyAttack;
    public NewEnemyPathfinding enemy;
    PlayerStats player;
    public int cloackTurns = 4;
    public int currentCloackTurns = 0;
    public float ShieldTime = 10;
    public int shieldTurns = 1;
    public int currentShieldTurns = 0;
    public bool hasKey = false;

    private void Start()
    {
        player = GetComponent<PlayerStats>();
    }
    public void FuelRefil()
    {
        if (player.currentActionPoints > 0)
        {
            sfxSelector.PlayUseItem();
            player.Refuel(player.amountToRefill);
            //inGameMenu.EndBattle();
            fuel.SetActive(false);
            player.ConsumeActionPoint();
        } else
        {
            Debug.Log("Not enough actionPoints to activate Cloack");
        }
    }
    public void Flare()
    {
        if (player.currentActionPoints > 0)
        {
            sfxSelector.PlayUseItem();
            inGameMenu.EndBattle();

            //StartCoroutine(DelayAction());
            //inGameMenu.EndBattle();
            flare.SetActive(false);
            player.ConsumeActionPoint();

        }
        else
        {
            Debug.Log("Not enough actionPoints to activate Cloack");
        }
        
    }
    public void Shield()
    {
        if (player.currentActionPoints > 0)
        {
            sfxSelector.PlayUseItem();

            Debug.Log("Shield has been Activated");
            currentShieldTurns = shieldTurns;
            inGameMenu.EndBattle();
            enemyAttack.enabled = false;
            shield.SetActive(false);
            player.ConsumeActionPoint();
        }
        else
        {
            Debug.Log("Not enough actionPoints to activate Cloack");
        }
        
    }
    public void Cloack()
    {
        if (player.currentActionPoints > 0)
        {
            sfxSelector.PlayUseItem();

            Debug.Log("Cloack Activated");
            currentCloackTurns = cloackTurns;

            inGameMenu.EndBattle();
            enemy.enabled = false;
            cloack.SetActive(false);
            player.ConsumeActionPoint();
        } else
        {
            Debug.Log("Not enough actionPoints to activate Cloack");
        }
    }
    public void Key()
    {
        sfxSelector.PlayUseItem();

        hasKey = true;
    }

    //private IEnumerator DelayAction()
    //{
    //    reveal.transform.position = new Vector3(-1.85000002f, 2.11999989f, -3.8900001f);
    //    Debug.Log("Flash THE WORLD");
    //    yield return new WaitForSeconds(3f);
    //    reveal.transform.position = new Vector3(-100.5f, -0.150000036f, -6.25f);
    //    Debug.Log("do something");
    //}
}