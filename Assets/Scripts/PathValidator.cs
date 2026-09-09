using System.Collections.Generic;
using UnityEngine;

public class PathValidator : MonoBehaviour
{
    [SerializeField] private MapManager board;
    [SerializeField] private Vector3Int startCell;
    [SerializeField] private Vector3Int endCell;

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
}
