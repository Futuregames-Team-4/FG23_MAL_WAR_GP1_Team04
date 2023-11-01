using UnityEngine;
using UnityEngine.SceneManagement;
using DebugTools;

public class GameStateManager : MonoBehaviour
{
    public enum GameState
    {
        Start,     // Show Main Menu
        PlayerTurn,   // Player's turn
        EnemyTurn, // Enemy's turn
        Paused,    // Show the Pause Menu
        End         // Game Over or Victory
    }
    public static GameStateManager Instance; // Singleton reference

    public GameState CurrentState { get; private set; } = GameState.Start;

    public PlayerStats player;
    public NewEnemyPathfinding enemy;
    public NewUseItem item;
    public InGameMenu inGameMenu;
    private bool isEnemyActivated = false;  // variabile per tenere traccia dello stato di attivazione del nemico
    public bool isEnemyVisible = true;  // variabile per controllare se il nemico è visibile

    public void SetEnemyVisibility(bool visibility)
    {
        isEnemyVisible = visibility;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void EnemyActivated()
    {
        isEnemyActivated = true;
    }

    public void EnemyDeactivated()
    {
        isEnemyActivated = false;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Update references when a new scene is loaded
        player = FindObjectOfType<PlayerStats>();
        enemy = FindObjectOfType<NewEnemyPathfinding>();
        item = FindObjectOfType<NewUseItem>();
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Keeps this object alive across scene changes
        }
        else if (Instance != this)
        {
            Destroy(gameObject);            // Destroy any other instances that get created in new scenes
            return;
        }
        StartPlayerTurn();
    }

    private void Update()
    {
        switch (CurrentState)
        {
            case GameState.PlayerTurn:
                if (player.useActionPoints && player.currentActionPoints <= 0)
                {
                    EndPlayerTurn();
                }
                break;

            case GameState.EnemyTurn:
                // Enemy logic is handled within the enemy's script
                break;

            // Additional cases can be added later
            case GameState.Start:
            case GameState.End:
            case GameState.Paused:
                break;
        }
    }

    public void StartPlayerTurn()
    {
        Debug.Log("PlayerTurn");
        CurrentState = GameState.PlayerTurn;
        //player.enabled = true;
        if (player.useActionPoints)
        {
            inGameMenu.ActivateSkipButton();
            player.currentActionPoints = player.maxActionPoints;
        }

        // Cloack Turns
        if (item.currentCloackTurns > 0)
        {
            item.currentCloackTurns --;
            if (item.currentCloackTurns == 0)
            {
                enemy.enabled = true;
                Debug.Log("Cloack is Down, Enemy can spot you!");
            }
        }

        // Shield Turns
        if (item.currentShieldTurns > 0)
        {
            item.currentShieldTurns--;
            if (item.currentShieldTurns == 0)
            {
                item.enemyAttack.enabled = true;
                Debug.Log("Shield Down, Enemy can attack you!");
            }
        }

    }

    public void EndPlayerTurn()
    {
        inGameMenu.DeactivateSkipButton();
        player.ConsumeFuel(1);
        if (!isEnemyActivated)
        {
            StartPlayerTurn();
        }
        else 
        {
            StartEnemyTurn();
        }
    }

    public void StartEnemyTurn()
    {
        Debug.Log("Enemy Turn");
        if (!isEnemyVisible)
        {
            EndEnemyTurn();
            return;
        }

        if (enemy && enemy.gameObject.activeInHierarchy)
        {
            CurrentState = GameState.EnemyTurn;
            enemy.shouldFollowPlayer = true; // This will make the enemy calculate the path and start following the player
        }
        else
        {
            EndEnemyTurn();  // If enemy is not there, then end the enemy's turn.
        }
    }

    public void EndEnemyTurn()
    {
        if (enemy)
        {
            StartPlayerTurn(); // Start the player's turn again
        }
    }
}