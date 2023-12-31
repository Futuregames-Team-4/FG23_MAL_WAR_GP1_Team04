using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    //[SerializeField]
    //GameStateManager gameStateManager;

    [SerializeField]
    GameObject pauseMenu;

    [SerializeField]
    GameObject overviewCamera;

    [SerializeField]
    GameObject mapOverview;

    [SerializeField]
    GameObject mainCamera;

    [SerializeField]
    GameObject pauseText;

    [SerializeField]
    GameObject controlsText;

    [SerializeField]
    PlayerMovement playerMovement;

    [SerializeField]
    GameObject attackSprite;

    [SerializeField]
    GameObject skipTurnButton;

    [SerializeField]
    GameObject battleScene;

    [SerializeField]
    FuelConsumption fuelConsumption;

    [SerializeField]
    GameObject inventory;

    [SerializeField]
    PlayerStats playerStats;

    [SerializeField]
    MusicSelector musicSelector;

    //public Sprite[] fuelSprites;

    bool isPaused = false;

    private void Awake()
    {
        attackSprite.SetActive(false);
        pauseMenu.SetActive(false);
        controlsText.SetActive(false);
        skipTurnButton.SetActive(false);
        battleScene.SetActive(false);
    }

    private void Start()
    {
        RegularSizedInventory();
    }

    private void Update()
    {

        //int fuelLevel = playerStats.fuel;

        //switch (playerStats.fuel)
        //{
        //    case 0: fuelSprites.ElementAt(0);
        //            break;
        //        case1: fuelSprites.ElementAt(1);
        //}


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
        
        if (Input.GetKey(KeyCode.Tab))
        {
            ToggleOverviewMap();
        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            RemoveOverviewMap();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        pauseMenu.SetActive(isPaused);
    }

    public void ControlsButton()
    {
        pauseText.SetActive(false);
        controlsText.SetActive(true);
    }

    public void BackButton()
    {
        pauseText.SetActive(true);
        controlsText.SetActive(false);
    }

    public void MainMenuButton()
    {
        TogglePause();
        SceneManager.LoadScene("Main Menu");
    }

    void ToggleOverviewMap()
    {
        inventory.SetActive(false);
        overviewCamera.SetActive(true);
        mapOverview.SetActive(true);
        mainCamera.SetActive(false);
        playerMovement.enabled = false;
    }

    void RemoveOverviewMap()
    {
        inventory.SetActive(true);
        overviewCamera.SetActive(false);
        mapOverview.SetActive(false);
        mainCamera.SetActive(true);
        playerMovement.enabled = true;
    }

    public IEnumerator AttackedSpriteEnable()
    {
        attackSprite.SetActive(true);
        yield return new WaitForSeconds(2);
        attackSprite.SetActive(false);
    }

    public void ActivateSkipButton()
    {
        skipTurnButton.SetActive(true);
    }

    public void DeactivateSkipButton()
    {
        skipTurnButton.SetActive(false);
    }

    public void SkipTurnButton()
    {
        playerStats.ConsumeActionPoint();
    }

    public void EnemyMoves()
    {
        //play enemy sound
    }

    public void StartBattle()
    {
        playerMovement.enabled = false;
        musicSelector.PlayBattleMusic();
        battleScene.SetActive(true);
        //inventory.transform.localPosition = new Vector3(0f, 400f, 0f);
        //inventory.transform.localScale = new Vector3(1.7f, 1.7f, 1.7f);
    }

    public void FleeButton()
    {
        EndBattle();
        fuelConsumption.HitByEnemy();
        StartCoroutine(AttackedSpriteEnable());
    }

    public void EndBattle()
    {
        if (!playerMovement.enabled)
        {
            musicSelector.PlayInGameMusic();
            playerMovement.enabled = true;
        }
        battleScene.SetActive(false);
        RegularSizedInventory();
    }

    void RegularSizedInventory()
    {
        inventory.transform.localPosition = new Vector3(0f, 0f, 0f);
        inventory.transform.localScale = new Vector3(1f, 1f, 1f);
    }
}
