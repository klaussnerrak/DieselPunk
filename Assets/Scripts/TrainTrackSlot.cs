using UnityEngine;
using UnityEngine.EventSystems;

public class TrainTrackSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedTrack = eventData.pointerDrag;
        DraggableItem draggableItem = droppedTrack.GetComponent<DraggableItem>();
        draggableItem.parentAfterDrag = transform;
    }
 
}
