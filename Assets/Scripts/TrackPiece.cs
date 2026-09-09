using UnityEngine;

public enum TrackType
{
    Straight,
    Curve,
    T,
    Cross
}

public class TrackPiece : MonoBehaviour
{
    [SerializeField] private TrackType type;
    [SerializeField] private bool north;
    [SerializeField] private bool south;
    [SerializeField] private bool east;
    [SerializeField] private bool west;

    public TrackType Type => type;

    public bool HasConnection(Vector2Int direction)
    {
        int rotation = Mathf.RoundToInt(transform.eulerAngles.z / 90f) % 4;
        Vector2Int localDirection = direction;

        for (int i = 0; i < rotation; i++)
            localDirection = new Vector2Int(localDirection.y, -localDirection.x);

        if (localDirection == Vector2Int.up) return north;
        if (localDirection == Vector2Int.right) return east;
        if (localDirection == Vector2Int.down) return south;
        if (localDirection == Vector2Int.left) return west;

        return false;
    }

    public void RotatePiece()
    {
        transform.Rotate(0f, 0f, 90f);
    }
}
