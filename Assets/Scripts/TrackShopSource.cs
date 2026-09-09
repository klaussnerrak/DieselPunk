using UnityEngine;
using UnityEngine.EventSystems;

public class TrackShopSource : MonoBehaviour, IPointerClickHandler
{
    private MapManager board;
    private TrackType type;
    private int price;
    private bool north;
    private bool south;
    private bool east;
    private bool west;

    public void Configure(MapManager map, TrackType pieceType, int piecePrice,
        bool connectsNorth, bool connectsSouth, bool connectsEast, bool connectsWest)
    {
        board = map;
        type = pieceType;
        price = piecePrice;
        north = connectsNorth;
        south = connectsSouth;
        east = connectsEast;
        west = connectsWest;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject purchased = new GameObject(type + " Track");
        purchased.transform.position = board.SnapToGrid(eventData.position);
        purchased.transform.rotation = transform.rotation;

        SpriteRenderer sourceRenderer = GetComponent<SpriteRenderer>();
        SpriteRenderer renderer = purchased.AddComponent<SpriteRenderer>();
        renderer.sprite = sourceRenderer.sprite;
        renderer.color = new Color(0.95f, 0.78f, 0.25f);
        renderer.sortingOrder = 3;

        BoxCollider2D collider = purchased.AddComponent<BoxCollider2D>();
        collider.size = Vector2.one * 0.9f;
        TrackPiece track = purchased.AddComponent<TrackPiece>();
        track.Configure(type, north, south, east, west);
        DraggableItem draggable = purchased.AddComponent<DraggableItem>();
        draggable.Configure(board);
        draggable.SetPosition(purchased.transform.position);

        RailVisual visual = purchased.AddComponent<RailVisual>();
        visual.Configure(type, north, south, east, west, renderer.sortingOrder + 1);

    }
}
