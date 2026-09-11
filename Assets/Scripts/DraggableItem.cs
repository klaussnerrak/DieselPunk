using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour,
    IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // [SerializeField] private MapManager board;

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
        {
            Debug.Log("I will return");
            return;
        }

        Camera camera = Camera.main;
        Collider2D collider = GetComponent<Collider2D>();
        if (camera == null || collider == null)
            return;

        Vector3 mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
        // if (collider.OverlapPoint(mousePosition))
        //     GetComponent<TrackPiece>()?.RotatePiece();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }

}