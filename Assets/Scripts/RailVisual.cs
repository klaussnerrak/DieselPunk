using UnityEngine;

public class RailVisual : MonoBehaviour
{
    private TrackType type;
    private bool north;
    private bool south;
    private bool east;
    private bool west;
    private int sortingOrder;

    public void Configure(TrackType pieceType, bool connectsNorth, bool connectsSouth,
        bool connectsEast, bool connectsWest, int order)
    {
        type = pieceType;
        north = connectsNorth;
        south = connectsSouth;
        east = connectsEast;
        west = connectsWest;
        sortingOrder = order;
        BuildLines();
    }

    private void BuildLines()
    {
        Vector2[] directions =
        {
            north ? Vector2.up : Vector2.zero,
            south ? Vector2.down : Vector2.zero,
            east ? Vector2.right : Vector2.zero,
            west ? Vector2.left : Vector2.zero
        };

        foreach (Vector2 direction in directions)
        {
            if (direction == Vector2.zero)
                continue;

            GameObject lineObject = new GameObject("Rail Connection");
            lineObject.transform.SetParent(transform, false);
            LineRenderer line = lineObject.AddComponent<LineRenderer>();
            line.material = new Material(Shader.Find("Sprites/Default"));
            line.startColor = new Color(0.08f, 0.09f, 0.11f);
            line.endColor = line.startColor;
            line.startWidth = 0.14f;
            line.endWidth = 0.14f;
            line.sortingOrder = sortingOrder;
            line.positionCount = 2;
            line.useWorldSpace = false;
            line.SetPosition(0, Vector3.zero);
            line.SetPosition(1, direction * 0.48f);
        }
    }
}
