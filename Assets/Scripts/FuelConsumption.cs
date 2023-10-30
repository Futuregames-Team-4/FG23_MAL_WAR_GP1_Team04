/*
 * For Albin: I commented out you previous code, so you can redo all chages if you want
 */
using UnityEngine;
using UnityEngine.SceneManagement;

public class FuelConsumption : MonoBehaviour
{
    [SerializeField]
    private RectTransform fuelLeft;

    private PlayerStats playerStats;

    private void Start()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        UpdateFuelBar();
        HandleOutOfFuel();
        // HandleNoActionPoints();
    }

    private void UpdateFuelBar()
    {
        int fuel = playerStats.fuel;
        int x = 100;
        int y = Mathf.Clamp(fuel * 30, 0, 300); // Adjusted fuel-to-y mapping

        fuelLeft.sizeDelta = new Vector2(x, y);
    }

    private void HandleOutOfFuel()
    {
        if (playerStats.fuel <= 0)
        {
            SceneManager.LoadScene("GameOverScene");
        }
    }

    // private void HandleNoActionPoints()
    // {
    //     if (playerStats.currentActionPoints == 0)
    //     {
    //         MovementCost();
    //     }
    // }

    // private void MovementCost()
    // {
    //     playerStats.ConsumeFuel();
    //     Debug.Log("Minus 1 fuel");
    // }

    public void UseConsumable(int amount)
    {
        playerStats.Refuel(amount);
    }

    public void HitByEnemy()
    {
        playerStats.ConsumeFuel(3);
    }
}