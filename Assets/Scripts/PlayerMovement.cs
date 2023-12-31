using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool mouseDebug = false;             // Show Raycast of the mouse
    private RaycastHit hitInfo;
    private GridSystem gridSystem;
    public PlayerStats playerStats;
    private GameStateManager gameStateManager;
    public Vector2Int CurrentGridPosition { get; private set; }


    private void Start()
    {
        gameStateManager = GetComponent<GameStateManager>();
        gridSystem = FindObjectOfType<GridSystem>();
        playerStats = GetComponent<PlayerStats>();
    }


    private void Update()
    {
        HandleMouseRaycast();
        HandleMouseInput();
    }

    private void HandleMouseRaycast()      // Raycast of the mouse
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layerMask) && mouseDebug)
        {
            Debug.DrawRay(ray.origin, ray.direction * hitInfo.distance, Color.red);
        }
    }

    private void HandleMouseInput()         // Input of the mouse
    {
        if (Input.GetMouseButtonDown(0) && hitInfo.collider != null && 
        IsValidMove(hitInfo.collider.transform.position) && playerStats.currentActionPoints > 0)
        {
            SquareStatus squareStatus = hitInfo.collider.GetComponent<SquareStatus>();
            if (squareStatus != null && !squareStatus.isOccupied)
            {
                MoveTo(hitInfo.collider.transform.position);
                playerStats.ConsumeActionPoint();
            }
        }
    }

    private bool IsValidMove(Vector3 targetPosition)
    {
        float distanceBetweenCells = gridSystem.cellSize + gridSystem.spacing;

        float xDifference = Mathf.Abs(targetPosition.x - transform.position.x);
        float zDifference = Mathf.Abs(targetPosition.z - transform.position.z);

        // Controlla se la mossa è valida
        bool isAdjacentMove = (xDifference == distanceBetweenCells && zDifference == 0f)
                              || (zDifference == distanceBetweenCells && xDifference == 0f);

        if (!isAdjacentMove)
            return false;

        // Verify if the target tile isOccupied
        RaycastHit hit;
        if (Physics.Raycast(targetPosition + Vector3.up * 5f, Vector3.down, out hit)) // Raycast towards target tile
        {
            if (hit.collider.CompareTag("Square"))
            {
                SquareStatus squareStatus = hit.collider.GetComponent<SquareStatus>();
                if (squareStatus && squareStatus.isOccupied)
                    return false; // isOccupied = true
            }
        }
        return true; // isOccupied = false
    }

    private void MoveTo(Vector3 targetPosition)
    {
        Occupier occupier = GetComponent<Occupier>();
        if (occupier)
        {
            occupier.MoveToSquare(targetPosition);
        }

        // Update GridStatus
        Vector2Int previousPos = gridSystem.GetGridPosition(transform.position);
        Vector2Int newPos = gridSystem.GetGridPosition(targetPosition);

        // Aggiorna la proprietà con la nuova posizione
        CurrentGridPosition = newPos;
    }
}