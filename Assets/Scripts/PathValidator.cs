using System.Collections.Generic;
using UnityEngine;

public class PathValidator : MonoBehaviour
{
    [SerializeField] private MapManager board;
    [SerializeField] private Vector3Int startCell;
    [SerializeField] private Vector3Int endCell;

    public MapManager Board => board;
    public Vector3Int StartCell => startCell;
    public Vector3Int EndCell => endCell;

    public void Configure(MapManager configuredBoard, Vector3Int start, Vector3Int end)
    {
        board = configuredBoard;
        ConfigureCells(start, end);
    }

    public void ConfigureCells(Vector3Int start, Vector3Int end)
    {
        startCell = start;
        endCell = end;
    }

    private static readonly Vector2Int[] Directions =
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

    public bool HasValidPath()
    {
        if (board == null)
            throw new MissingReferenceException("PathValidator requires a MapManager.");

        if (!board.TryGetPiece(startCell, out TrackPiece startPiece))
            return false;

        var visited = new HashSet<Vector3Int> { startCell };
        var pending = new Queue<Vector3Int>();
        pending.Enqueue(startCell);

        while (pending.Count > 0)
        {
            Vector3Int currentCell = pending.Dequeue();
            if (currentCell == endCell)
                return true;

            if (!board.TryGetPiece(currentCell, out TrackPiece currentPiece))
                continue;

            foreach (Vector2Int direction in Directions)
            {
                if (!currentPiece.HasConnection(direction))
                    continue;

                Vector3Int neighbourCell = currentCell + new Vector3Int(direction.x, direction.y, 0);
                if (visited.Contains(neighbourCell) ||
                    !board.TryGetPiece(neighbourCell, out TrackPiece neighbourPiece) ||
                    !neighbourPiece.HasConnection(-direction))
                    continue;

                visited.Add(neighbourCell);
                pending.Enqueue(neighbourCell);
            }
        }

        return false;
    }

    public bool TryGetPath(List<Vector3Int> path)
    {
        path.Clear();
        if (board == null || !board.TryGetPiece(startCell, out TrackPiece startPiece))
            return false;

        var previous = new Dictionary<Vector3Int, Vector3Int>();
        var pending = new Queue<Vector3Int>();
        var visited = new HashSet<Vector3Int> { startCell };
        pending.Enqueue(startCell);

        while (pending.Count > 0)
        {
            Vector3Int current = pending.Dequeue();
            if (current == endCell)
            {
                while (current != startCell)
                {
                    path.Add(current);
                    current = previous[current];
                }
                path.Add(startCell);
                path.Reverse();
                return true;
            }

            if (!board.TryGetPiece(current, out TrackPiece currentPiece))
                continue;

            foreach (Vector2Int direction in Directions)
            {
                if (!currentPiece.HasConnection(direction))
                    continue;

                Vector3Int neighbour = current + new Vector3Int(direction.x, direction.y, 0);
                if (visited.Contains(neighbour) ||
                    !board.TryGetPiece(neighbour, out TrackPiece neighbourPiece) ||
                    !neighbourPiece.HasConnection(-direction))
                    continue;

                visited.Add(neighbour);
                previous[neighbour] = current;
                pending.Enqueue(neighbour);
            }
        }

        return false;
    }
}
