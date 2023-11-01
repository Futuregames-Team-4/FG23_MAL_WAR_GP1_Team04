using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackRange = 2.0f;
    public float attackDamage = 1.0f;
    public LayerMask playerLayer;
    public int rayCount = 8; // Number of raycasts to shoot in all directions
    public float spreadAngle = 360.0f; // Angle within which raycasts are spread

    private bool hasAttacked = false; // Flag to track whether an attack has already occurred

    [SerializeField] InGameMenu inGameMenu;

    public void AttackPlayer(Collider player)
    {
        if (!hasAttacked) // Check if an attack has not already occurred
        {
            Debug.Log("You are hit!");
            inGameMenu.StartBattle();

            // Apply damage to the player (you can modify this part)
            //FuelConsumption fuel = player.transform.parent.GetComponent<FuelConsumption>();
            //fuel.HitByEnemy();
            //StartCoroutine(inGameMenu.AttackedSpriteEnable());
            hasAttacked = true; // Set the flag to true after attacking
        }
    }

    

}
