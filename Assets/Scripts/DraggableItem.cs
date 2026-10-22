using NavMeshPlus.Components;
//using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class DraggableItem : TrainTrack
{
    //public GridManager gridManager;
    [SerializeField] private GameObject selectedTrack;
    public bool isTrainMoving = false;
    private bool isDragging = false;
    private Vector3 offset;
    private Grid targetGrid;
    private Tilemap tilemap;
    private Vector3 initialPosition;
    private Vector3 itemLastPosition;
    private int trackIndex;
    private TrainTrackShop trainTrackShop;



    void Start()
    {
        trainTrackShop = FindFirstObjectByType<TrainTrackShop>();
        targetGrid = FindFirstObjectByType<Grid>();
        GameObject tilemapObject = GameObject.Find("Tilemap");
        GameObject gridManagerObject = GameObject.Find("Grid");


        if (tilemapObject != null)
        {
            tilemap = tilemapObject.GetComponent<Tilemap>();
        }
        else
        {
            Debug.LogError("Tilemap not found");
        }

        initialPosition = this.transform.position;
        itemLastPosition = transform.position;
    }

    void Update()
    {
        if (isDragging && TrackLocked == false)
        {
            Vector3 mousePosition = GetMouseWorldPosition();
            transform.position = mousePosition + offset;

        }
    }

    private void OnMouseDown()
    {
        if (isTrainMoving) return;

        isDragging = true;
        offset = transform.position - GetMouseWorldPosition();
        GridManager.instance.Track.Remove(this);
        upConnected = false;
        downConnected = false;
        leftConnected = false;
        rightConnected = false;
        // Debug.Log("Dragging");
    }

    private void OnMouseUp()
    {

        //adiciona o objeto na lista do gridmanager
        isDragging = false;
        GridManager.instance.AddToTrack(this);
        trackIndex = GridManager.instance.TrackTiles.Count - 1;

        if (!CheckTrackDeleteArea())
        {
            SnapTileToGrid();
        }

    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1) && !isDragging && !isTrainMoving)
        {
            RotateTrack();
        }
    }

    private void RotateTrack()
    {
        if (!TrackLocked)
        {
            transform.Rotate(0f, 0f, 90f);

            if (TryGetComponent<BoxCollider2D>(out var collider))
            {
                Debug.Log(type);
                collider.enabled = false;
                collider.enabled = true;
            }
            GridManager.instance.updateRotation(this);

        }

    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = Mathf.Abs(Camera.main.transform.position.z);
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void SnapTileToGrid()
    {
        if (targetGrid == null) return;

        Vector3Int cellPosition = targetGrid.WorldToCell(transform.position);

        if (IsValidPlace(cellPosition) && TrackLocked == false)
        {
            Vector3 snappedPosition = targetGrid.CellToWorld(cellPosition);

            snappedPosition.x += targetGrid.cellSize.x / 2;
            snappedPosition.y += targetGrid.cellSize.y / 2;
            snappedPosition.z = 0f;

            transform.position = snappedPosition;
            itemLastPosition = transform.position;

            GridManager.instance.TrackTiles[trackIndex].transform.position = itemLastPosition;

            if (!this.isBought)
            {
                trainTrackShop.BuyTrack(selectedTrack, initialPosition);
                if(type == TrackType.Bomb)
                {
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            transform.position = itemLastPosition;
        }
    }

    private bool CheckTrackDeleteArea()
    {
        Vector3 mousePosition = GetMouseWorldPosition();
        foreach (Collider2D hit in Physics2D.OverlapPointAll(new Vector2(mousePosition.x, mousePosition.y)))
        {
            if (hit.CompareTag("DeleteTrackArea") && TrackLocked == false)
            {
                Debug.Log("Delete track area detected");

                if (this.isBought == true)
                {
                    GridManager.instance.TrackTiles.Remove(this);
                    GridManager.instance.Track.Remove(this);
                    ShopManager.instance.SellTrack(this);
                    Destroy(gameObject);
                    return true;
                }
            }
        }
        return false;
    }

    private bool IsValidPlace(Vector3Int cellPosition)
    {
        if (!tilemap.HasTile(cellPosition))
        {

            return false;
        }

        Vector3 cellWorldPos = targetGrid.CellToWorld(cellPosition);
        cellWorldPos.x += targetGrid.cellSize.x / 2;
        cellWorldPos.y += targetGrid.cellSize.y / 2;

        Vector2 checkSize = new Vector2(targetGrid.cellSize.x * 0.8f, targetGrid.cellSize.y * 0.8f);
        Collider2D[] hits = Physics2D.OverlapBoxAll(cellWorldPos, checkSize, 0f);

        foreach (Collider2D hit in hits)
        {

            if (hit.transform == transform || hit.transform.IsChildOf(transform))
            {

                continue;
            }
            if(type != TrackType.Bomb)
            {
                if (hit.CompareTag("TrainTrackPiece"))
                {
                    return false;
                }
                else if(hit.CompareTag("Obstacle"))
                {
                    if (type == TrackType.Bridge)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                Destroy(hit.gameObject);
            }
            
        }

        return true;
    }

}