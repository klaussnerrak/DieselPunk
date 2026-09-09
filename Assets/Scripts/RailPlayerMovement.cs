using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailPlayerMovement : MonoBehaviour
{
    [SerializeField] private MapManager board;
    [SerializeField] private Transform mapEnd;
    [SerializeField] private Vector3Int startCell;
    [SerializeField] private float speed = 3f;

    private bool moving;

    public void Configure(MapManager map, Transform end, Vector3Int start)
    {
        board = map;
        mapEnd = end;
        startCell = start;
        transform.position = board.Grid.GetCellCenterWorld(startCell);
    }

    public bool TryStart()
    {
        if (moving || board == null || mapEnd == null)
            return false;

        Vector3Int endCell = board.Grid.WorldToCell(mapEnd.position);
        if (!TryFindPath(startCell, endCell, out List<Vector3Int> path))
            return false;

        StartCoroutine(FollowPath(path));
        return true;
    }

    private IEnumerator FollowPath(List<Vector3Int> path)
    {
        moving = true;
        for (int i = 1; i < path.Count; i++)
        {
            Vector3 target = board.Grid.GetCellCenterWorld(path[i]);
            while (Vector3.Distance(transform.position, target) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
                yield return null;
            }
        }

        transform.position = mapEnd.position;
        moving = false;
    }

    private bool TryFindPath(Vector3Int start, Vector3Int end, out List<Vector3Int> path)
    {
        path = new List<Vector3Int>();
        var previous = new Dictionary<Vector3Int, Vector3Int>();
        var pending = new Queue<Vector3Int>();
        pending.Enqueue(start);
        previous[start] = start;
        Vector2Int[] directions = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };

        while (pending.Count > 0)
        {
            Vector3Int current = pending.Dequeue();
            if (current == end)
                break;

            if (!board.TryGetPiece(current, out TrackPiece currentPiece))
                continue;

            foreach (Vector2Int direction in directions)
            {
                if (!currentPiece.HasConnection(direction))
                    continue;

                Vector3Int next = current + new Vector3Int(direction.x, direction.y, 0);
                if (previous.ContainsKey(next) ||
                    !board.TryGetPiece(next, out TrackPiece nextPiece) ||
                    !nextPiece.HasConnection(-direction))
                    continue;

                previous[next] = current;
                pending.Enqueue(next);
            }
        }

        if (!previous.ContainsKey(end))
            return false;

        for (Vector3Int current = end; current != start; current = previous[current])
            path.Add(current);
        path.Add(start);
        path.Reverse();
        return true;
    }
}
