using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnemyPathfinding : MonoBehaviour
{
    public GridSystem gridSystem;
    private List<Vector2Int> path;
    private Vector2Int playerPos;
    public PlayerMovement player;
    public bool shouldFollowPlayer = false;
    public int enemyMoves = 1; // Default to 3 moves per turn, but you can change this value in the Unity editor.

    void Start()
    {
        path = new List<Vector2Int>();
    }

    void Update()
    {
        if (shouldFollowPlayer)
        {
            FindPathToPlayer(player.CurrentGridPosition);
            shouldFollowPlayer = false;
        }
    }


    public void FindPathToPlayer(Vector2Int playerPos)
    {
        HashSet<Vector2Int> visited = new HashSet<Vector2Int>();

        this.playerPos = playerPos;
        Vector2Int startPos = gridSystem.GetGridPosition(transform.position);
        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        Dictionary<Vector2Int, Vector2Int> cameFrom = new Dictionary<Vector2Int, Vector2Int>();

        queue.Enqueue(playerPos);
        while (queue.Count > 0)
        {
            Vector2Int current = queue.Dequeue();
            if (current == startPos)
            {
                CreatePath(cameFrom);
                return;
            }
           
            foreach (Vector2Int neighbor in GetNeighbors(current))
            {
                if (!cameFrom.ContainsKey(neighbor) && !visited.Contains(neighbor))
                {
                    queue.Enqueue(neighbor);
                    cameFrom[neighbor] = current;
                    visited.Add(neighbor);
                }
            }
        }
    }

    private List<Vector2Int> GetNeighbors(Vector2Int pos)
    {
        List<Vector2Int> neighbors = new List<Vector2Int>();

        // Prioritize horizontal movement
        Vector2Int[] potentialNeighbors =
        {
        new Vector2Int(pos.x + 1, pos.y),
        new Vector2Int(pos.x - 1, pos.y),
        new Vector2Int(pos.x, pos.y + 1),
        new Vector2Int(pos.x, pos.y - 1)
    };

        foreach (var potential in potentialNeighbors)
        {
            if (IsValidDestination(potential))
            {
                neighbors.Add(potential);
            }
        }

        return neighbors;
    }

    private bool IsValidDestination(Vector2Int gridPosition)
    {
        if (gridPosition.x < 0 || gridPosition.y < 0 || gridPosition.x >= gridSystem.gridSizeX || gridPosition.y >= gridSystem.gridSizeY)
            return false;

        Transform potentialSquare = gridSystem.grid[gridPosition.x, gridPosition.y];

        if (potentialSquare != null && potentialSquare.CompareTag("Square"))
        {
            SquareStatus squareStatus = potentialSquare.GetComponent<SquareStatus>();
            if (squareStatus != null && !squareStatus.isOccupied)
            {
                return true;
            }
        }

        return false;
    }

    private void CreatePath(Dictionary<Vector2Int, Vector2Int> cameFrom)
    {
        Vector2Int current = gridSystem.GetGridPosition(transform.position);
        List<Vector2Int> reversedPath = new List<Vector2Int>();

        while (current != playerPos)
        {
            reversedPath.Add(current);
            current = cameFrom[current];
        }
        path = reversedPath;
        StartCoroutine(FollowPath());
    }

    IEnumerator FollowPath()
    {
        int movesThisTurn = Mathf.Min(path.Count, enemyMoves+1); // Limit the moves to either the path length or enemyMoves, +1 because so it works.

        for (int i = 0; i < movesThisTurn; i++)
        {
            Vector2Int pos = path[i];
            if (!IsValidDestination(pos))
            {
                Debug.LogWarning("Attempting to move to an invalid position: " + pos);
                continue; // Skip this iteration and continue with the next grid position
            }
            // Interpolazione lineare per un movimento più fluido
            Vector3 startPosition = transform.position;
            Vector3 targetPosition = gridSystem.GetWorldPosition(pos);
            float journeyLength = Vector3.Distance(startPosition, targetPosition);
            float startTime = Time.time;
            float distanceCovered = (Time.time - startTime) * journeyLength;
            float fractionOfJourney = distanceCovered / journeyLength;

            while (fractionOfJourney < 1)
            {
                distanceCovered = (Time.time - startTime) * journeyLength;
                fractionOfJourney = distanceCovered / journeyLength;
                transform.position = Vector3.Lerp(startPosition, targetPosition, fractionOfJourney);
                yield return null;
            }
            yield return new WaitForSeconds(0.5f); // puoi cambiare la durata dell'attesa per far muovere il nemico più velocemente o lentamente
            //TryAttackPlayer();
        }
        GameStateManager.Instance.EndEnemyTurn();
    }

    //private void TryAttackPlayer()
    //{
    //    int rayCount = 8;
    //    float spreadAngle = 360.0f;
    //    float attackRange = 2.0f;
    //    int playerLayerMask = 1 << LayerMask.NameToLayer("Box Collider");

    //    for (int i = 0; i < rayCount; i++)
    //    {
    //        float angle = (i / (float)rayCount) * spreadAngle - (spreadAngle / 2.0f);
    //        Vector3 rayDirection = Quaternion.Euler(0, angle, 0) * transform.forward;

    //        RaycastHit hit;
    //        if (Physics.Raycast(transform.position, rayDirection, out hit, attackRange, playerLayerMask))
    //        {
    //            if (hit.collider.CompareTag("Player"))
    //            {
    //                EnemyAttack enemyAttackComponent = GetComponent<EnemyAttack>();
    //                if (enemyAttackComponent != null)
    //                {
    //                    enemyAttackComponent.AttackPlayer(hit.collider);
    //                }
    //                return; // Esce dal ciclo, dato che vogliamo attaccare solo una volta.
    //            }
    //        }
    //    }
    //}


}