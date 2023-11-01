using UnityEngine;
using UnityEngine.SceneManagement;
using DebugTools;

using UnityEngine;
using UnityEngine.SceneManagement;
using DebugTools;

public class GameStateManager : MonoBehaviour
{
    public enum GameState
    {
        Start,
        PlayerTurn,
        EnemyTurn,
        Paused,
        End
    }

    public static GameStateManager Instance;

    public GameState CurrentState { get; private set; } = GameState.Start;

    public PlayerStats player;
    public NewEnemyPathfinding enemy;
    public NewUseItem item;
    public InGameMenu inGameMenu;
    public ActivationController activationController;

    private bool isEnemyActivated = false;
    public bool isEnemyVisible = true;

    #region Unity Methods

    private void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;

    private void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

    private void Awake() => InitializeSingleton();

    private void Update() => HandleGameStateUpdate();

    #endregion

    public void SetEnemyVisibility(bool visibility) => isEnemyVisible = visibility;

    public void EnemyActivated() => isEnemyActivated = true;

    public void EnemyDeactivated() => isEnemyActivated = false;

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) => UpdateReferences();

    private void InitializeSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        StartPlayerTurn();
    }

    private void HandleGameStateUpdate()
    {
        if (CurrentState == GameState.PlayerTurn && player.useActionPoints && player.currentActionPoints <= 0)
            EndPlayerTurn();
    }

    private void UpdateReferences()
    {
        player = FindObjectOfType<PlayerStats>();
        enemy = FindObjectOfType<NewEnemyPathfinding>();
        item = FindObjectOfType<NewUseItem>();
        activationController = FindObjectOfType<ActivationController>();
    }

    public void StartPlayerTurn()
    {
        Debug.Log("PlayerTurn");
        CurrentState = GameState.PlayerTurn;
        HandleActionPoints();
        HandleCloakTurns();
        HandleShieldTurns();
    }

    private void HandleActionPoints()
    {
        if (player.useActionPoints)
        {
            inGameMenu.ActivateSkipButton();
            player.currentActionPoints = player.maxActionPoints;
        }
    }

    private void HandleCloakTurns()
    {
        if (item.currentCloackTurns > 0)
        {
            item.currentCloackTurns--;
            if (item.currentCloackTurns == 0)
            {
                enemy.enabled = true;
                Debug.Log("Cloack is Down, Enemy can spot you!");
            }
        }
    }

    private void HandleShieldTurns()
    {
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
        if (!isEnemyActivated && !activationController.scriptComponent.enabled)
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

        if (IsEnemyActive())
        {
            CurrentState = GameState.EnemyTurn;
            enemy.shouldFollowPlayer = true;
        }
        else
        {
            EndEnemyTurn();
        }
    }

    private bool IsEnemyActive() => enemy && enemy.gameObject.activeInHierarchy;

    public void EndEnemyTurn()
    {
        if (enemy) StartPlayerTurn();
    }
}
