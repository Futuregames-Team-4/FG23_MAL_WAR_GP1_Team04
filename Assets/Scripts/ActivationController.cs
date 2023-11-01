using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationController : MonoBehaviour
{
    public PlayerStats player;
    
    [SerializeField]
    GameObject objectToActivate;

    public NewEnemyPathfinding scriptComponent;
    public GameStateManager gameStateManager;

    public void Start()
    {
        this.gameObject.SetActive(true);
    }

    public void DisableEnemy()
    {
        GetComponent<ActivationController>().enabled = false;
    }

    public void EnableEnemy() {
        GetComponent<ActivationController>().enabled = true;
    }

    void Awake()
    {
        scriptComponent = gameObject.GetComponent<NewEnemyPathfinding>();
        objectToActivate.SetActive(false);
        // If the GameObject with this script is different from objectToActivate, 
        // then you can also set the scriptComponent.enabled to false here if you want.
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            objectToActivate.SetActive(true);
            scriptComponent.enabled = true; // Enables the NewEnemyPathfinding script
            player.useActionPoints = true;  // Enable the Turn-Based System
            gameStateManager.EnemyActivated(); // Notify GameStateManager
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            objectToActivate.SetActive(false);
            scriptComponent.enabled = false; // Disables the NewEnemyPathfinding script
            gameStateManager.EnemyDeactivated(); // Notify GameStateManager
        }
    }
}
