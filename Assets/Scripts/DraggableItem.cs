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
    }

    public void Configure(MapManager mapManager)
    {
        board = mapManager;
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