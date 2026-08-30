using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public int gridSize;
    [HideInInspector]public Transform parentAfterDrag;

    public void OnBeginDrag(PointerEventData eventData)
    { 
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End drag"); 
        transform.position = new Vector2(Mathf.Round(transform.position.x / gridSize) * gridSize, Mathf.Round(transform.position.y / gridSize) * gridSize);
    }
 
}
