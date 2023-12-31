using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    public float cellSize = 1;
    public float spacing = 0.25f;
    public Transform[,] grid; // matrice bidimensionale che rappresenta la griglia
    public int gridSizeX { get; private set; }
    public int gridSizeY { get; private set; }
    private int offsetX;
    private int offsetY;
    private Transform bottomLeftSquare;
    public bool[,] occupiedGrid; // new addition


    private void Start()
    {
        bottomLeftSquare = GetBottomLeftSquare();
        if (bottomLeftSquare == null)
        {
            Debug.LogError("No bottom left square found!");
            return; // esce dal metodo Start
        }

        if (bottomLeftSquare != null)
        {
            // Set cellSize based on the scale of the detected square
            cellSize = bottomLeftSquare.localScale.x;

            // Find the nearest neighbor of the bottomLeftSquare to determine spacing
            SquareStatus[] squares = FindObjectsOfType<SquareStatus>();
            float minDistance = float.MaxValue;
            foreach (var square in squares)
            {
                if (square.transform != bottomLeftSquare)
                {
                    float distance = Vector3.Distance(square.transform.position, bottomLeftSquare.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                    }
                }
            }
            // Set spacing as difference between minDistance and cellSize
            spacing = minDistance - cellSize;
        }
        DetectAllSquaresInScene();
    }

    private void DetectAllSquaresInScene()
    {

        SquareStatus[] squares = FindObjectsOfType<SquareStatus>();

        int maxX = int.MinValue;
        int maxY = int.MinValue;
        int minX = int.MaxValue;
        int minY = int.MaxValue;

        foreach (var square in squares)
        {
            Vector2Int gridPos = GetGridPositionWithoutOffset(square.transform.position); // Usiamo una versione modificata di GetGridPosition 
            maxX = Mathf.Max(maxX, gridPos.x);
            maxY = Mathf.Max(maxY, gridPos.y);
            minX = Mathf.Min(minX, gridPos.x);
            minY = Mathf.Min(minY, gridPos.y);
        }

        gridSizeX = maxX - minX + 1;
        gridSizeY = maxY - minY + 1;

        offsetX = minX;
        offsetY = minY;

        grid = new Transform[gridSizeX, gridSizeY];

        for (int i = 0; i < gridSizeX; i++)
            for (int j = 0; j < gridSizeY; j++)
                grid[i, j] = null;

        occupiedGrid = new bool[gridSizeX, gridSizeY]; // new addition

        foreach (var square in squares)
        {
            Vector2Int gridPos = GetGridPosition(square.transform.position);
            if (gridPos.x >= 0 && gridPos.x < gridSizeX && gridPos.y >= 0 && gridPos.y < gridSizeY)
            {
                grid[gridPos.x, gridPos.y] = square.transform;
                occupiedGrid[gridPos.x, gridPos.y] = square.isOccupied; // new addition
            }
            else
            {
                Debug.LogWarning("Posizione non valida: " + gridPos + " per l'oggetto " + square.name);
            }
        }

    }

    public Transform GetBottomLeftSquare()
    {
        SquareStatus[] squares = FindObjectsOfType<SquareStatus>();
        if (squares.Length == 0) return null;

        Transform bottomLeftSquare = squares[0].transform;
        foreach (var square in squares)
        {
            if (square.transform.position.x <= bottomLeftSquare.position.x &&
                square.transform.position.z <= bottomLeftSquare.position.z)
            {
                bottomLeftSquare = square.transform;
            }
        }
        return bottomLeftSquare;
    }

    public Vector2Int GetGridPositionWithoutOffset(Vector3 worldPosition)
    {
        if (bottomLeftSquare == null)
        {
            bottomLeftSquare = GetBottomLeftSquare();
        }
        Vector3 relativePosition = worldPosition - bottomLeftSquare.position;

        // Arrotonda i calcoli per evitare decimali piccolissimi
        int x = Mathf.RoundToInt(relativePosition.x / (cellSize + spacing));
        int y = Mathf.RoundToInt((relativePosition.z - 2 * (cellSize + spacing)) / (cellSize + spacing));

        return new Vector2Int(x, y);
    }

    public Vector3 GetWorldPosition(Vector2Int gridPosition)
    {
        // Arrotonda i calcoli per evitare decimali piccolissimi
        float x = Mathf.Round(gridPosition.x * (cellSize + spacing) + bottomLeftSquare.position.x);
        float y = 0; // Altezza rimane la stessa
        float z = Mathf.Round(gridPosition.y * (cellSize + spacing) + bottomLeftSquare.position.z);

        return new Vector3(x, y, z);
    }

    public Vector2Int GetGridPosition(Vector3 worldPosition)
    {
        Vector2Int posWithoutOffset = GetGridPositionWithoutOffset(worldPosition);
        return posWithoutOffset - new Vector2Int(offsetX, offsetY);
    }


    void OnDrawGizmos()
    {
        if (grid == null) return;

        for (int i = 0; i < gridSizeX; i++)
        {
            for (int j = 0; j < gridSizeY; j++)
            {
                if (grid[i, j] != null)
                {
                    Vector3 worldPos = GetWorldPosition(new Vector2Int(i, j));
                    Gizmos.DrawWireCube(worldPos, new Vector3(cellSize, 0.1f, cellSize)); // Cambia l'altezza (0.1f) se necessario
                }
            }
        }
    }

}