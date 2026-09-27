using NavMeshPlus.Components;
//using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class DraggableItem : TrainTrack
{
    //public GridManager gridManager;
    public bool isTrainMoving = false;
    private bool isDragging = false;
    private Vector3 offset;
    private Grid targetGrid;
    private Tilemap tilemap;
    private Vector3 itemLastPosition;
   // public PlayerTrainScript playerScript;
    private int trackIndex;

   // private bool trackColider = false;

    void Start()
    {
        targetGrid = FindFirstObjectByType<Grid>();
        //playerScript = FindFirstObjectByType<PlayerTrainScript>();
        
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

        /*if (gridManagerObject != null)
        {
            gridManager = gridManagerObject.GetComponent<GridManager>();
        }
        else
        {
            Debug.LogError("GridManager not found");
        }*/
        

        itemLastPosition = transform.position;

        //adiciona o objeto na lista do gridmanager
        GridManager.instance.AddToTrack(this);
        trackIndex = GridManager.instance.TrackTiles.Count - 1;
    }

    void Update()
    {
        if (isDragging)
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
    }

    private void OnMouseUp()
    {
        isDragging = false;
        SnapTileToGrid();
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
        transform.Rotate(0f, 0f, 90f);

        if (TryGetComponent<BoxCollider2D>(out var collider))
        {
            collider.enabled = false;
            collider.enabled = true;
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

        if (IsValidPlace(cellPosition))

        {
            Vector3 snappedPosition = targetGrid.CellToWorld(cellPosition);            

            snappedPosition.x += targetGrid.cellSize.x / 2;
            snappedPosition.y += targetGrid.cellSize.y / 2;
            snappedPosition.z = 0f;

            transform.position = snappedPosition;
            itemLastPosition = transform.position;
           
            GridManager.instance.TrackTiles[trackIndex].transform.position = itemLastPosition;
           
        }
        else
        {
            transform.position = itemLastPosition;
        }
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
            if (hit.CompareTag("TrainTrackPiece"))
            {
                return false;
            }        
        }

         return true;
    }
}