using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour,
    IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private MapManager board;

    private Vector3 previousPosition;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        if (GetComponent<Collider2D>() == null && GetComponent<SpriteRenderer>() != null)
            gameObject.AddComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(1))
            return;

        Camera camera = Camera.main;
        Collider2D collider = GetComponent<Collider2D>();
        if (camera == null || collider == null)
            return;

        Vector3 mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
        if (collider.OverlapPoint(mousePosition))
            GetComponent<TrackPiece>()?.RotatePiece();
    }

    public void Configure(MapManager mapManager)
    {
        board = mapManager;
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
        if (board != null)
            board.Register(this);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        MapManager map = RequireBoard();
        previousPosition = transform.position;
        map.Remove(this);

        if (canvasGroup != null)
            canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = RequireBoard().SnapToGrid(eventData.position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (canvasGroup != null)
            canvasGroup.blocksRaycasts = true;

        MapManager map = RequireBoard();
        if (!map.TryPlace(this, eventData.position, out Vector3 snappedPosition))
        {
            transform.position = previousPosition;
            map.Register(this);
            return;
        }

        transform.position = snappedPosition;
    }

    private MapManager RequireBoard()
    {
        if (board == null)
            throw new MissingReferenceException("DraggableItem requires a MapManager.");

        return board;
    }
}