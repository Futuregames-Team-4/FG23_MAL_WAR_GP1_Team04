using UnityEngine;

[DefaultExecutionOrder(-1)] // Load this first
public class PlayerStats : MonoBehaviour
{
    public int maxActionPoints = 3;
    public int currentActionPoints;
    public bool useActionPoints = false;   // Set to false by default
    public int fuel = 10;
    public int amountToRefill = 10;

    private void Start()
    {
        currentActionPoints = maxActionPoints;
    }

    public void ResetActionPoints()
    {
        currentActionPoints = maxActionPoints;
    }

    public void ConsumeFuel(int amount)
    {
        fuel = Mathf.Max(fuel - amount, 0);
    }

    public void Refuel(int amount)
    {
        fuel = Mathf.Min(fuel + amount, amountToRefill);
    }

    public void ConsumeActionPoint()
    {
        if (useActionPoints && currentActionPoints > 0)
        {
            currentActionPoints--;
        } else 
        {
            return;
        }
    }

}
