using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxActionPoints = 3;
    public int currentActionPoints;
    public int fuel = 10;
    public int amountToRefill = 10;

    private void Start()
    {
        currentActionPoints = maxActionPoints;
    }

    public void ConsumeActionPoint()
    {
        // Something else comment here
        if (currentActionPoints > 0)
        {
            currentActionPoints--;
        }
    }

    public void ResetActionPoints()
    {
        currentActionPoints = maxActionPoints;
    }

    public void ConsumeFuel(int amount = 1)
    {
        fuel -= amount;
        if (fuel < 0)
        {
            fuel = 0;
        }
    }

    public void Refuel(int amount)
    {
        fuel = amount;
    }
}
