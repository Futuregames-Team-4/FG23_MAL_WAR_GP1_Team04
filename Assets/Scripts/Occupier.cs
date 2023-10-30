using UnityEngine;

public class Occupier : MonoBehaviour
{
    private Transform currentSquare; // Referred to the actual square
    public GridSystem gridSystem; // Reference to the Grid System

    private void Start()
    {
        SetSquareBeneathAsOccupied();
    }

    public void MoveToSquare(Vector3 newPosition)
    {
        FreeCurrentSquare();
        transform.position = newPosition;
        SetSquareBeneathAsOccupied();
    }

    private void FreeCurrentSquare()
    {
        if (currentSquare)
        {
            SquareStatus cubeStatus = currentSquare.GetComponent<SquareStatus>();
            if (cubeStatus)
            {
                cubeStatus.isOccupied = false;
            }
            currentSquare = null;
            
        }
    }

    private void SetSquareBeneathAsOccupied()
    {
        Vector2Int gridPos = gridSystem.GetGridPosition(transform.position);
        //Debug.Log("Grid Position: " + gridPos); // Log the grid position

        if (gridPos.x >= 0 && gridPos.x < gridSystem.gridSizeX && gridPos.y >= 0 && gridPos.y < gridSystem.gridSizeY)
        {
            currentSquare = gridSystem.grid[gridPos.x, gridPos.y];
            if (currentSquare)
            {
                SquareStatus cubeStatus = currentSquare.GetComponent<SquareStatus>();
                if (cubeStatus)
                {
                    cubeStatus.isOccupied = true;
                }

            }
        }
    }

}
