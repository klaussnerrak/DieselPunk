using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class DraggableItem : MonoBehaviour
{
    private GameObject selectedTrainTrack;
    private bool isDragging = false;
    private Vector3 offset;
    private Vector3 startPosition;
    private Grid targetGrid;
    private Tilemap tilemap;

    void Start()
    {
        targetGrid = FindFirstObjectByType<Grid>();

        if (tilemap == null)
        {
            tilemap = FindFirstObjectByType<Tilemap>();
        }

        targetGrid = FindFirstObjectByType<Grid>();

        GameObject tilemapObject = GameObject.Find("Tilemap");

        if (tilemapObject != null)
        {
            tilemap = tilemapObject.GetComponent<Tilemap>();
        }
        else
        {
            Debug.LogError("Tilemap not found");
        }
    }

    void Update()
    {
        Vector3 mousePosition = GetMouseWorldPosition();
        if (isDragging)
        {
            transform.position = mousePosition + offset;
        }
        // Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePosition);

        // // Cast a 2D ray directly at the point
        // RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);

        // if (hit.collider != null)
        // {
        //     Debug.Log("Selected 2D Object: " + hit.collider.gameObject.name);
        // }

        if (Input.GetMouseButtonDown(1))
        {
            RotatePiece(mousePosition);
        }
    }

    void FixedUpdate()
    {

    }

    public void RotatePiece(Vector3 mousePosition)
    {
        Debug.Log("Right mouse clicked");
         
        Debug.DrawLine(mousePosition + Vector3.left, mousePosition + Vector3.right, Color.red, 2f);
        Debug.DrawLine(mousePosition + Vector3.up, mousePosition + Vector3.down, Color.red, 2f);

        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        if (hit.collider != null)
        {
            selectedTrainTrack = hit.collider.gameObject;
            selectedTrainTrack.transform.Rotate(0f, 0f, 90f); 
        }
    }


    private void OnMouseDown()
    {
        isDragging = true;
        startPosition = transform.position;
        offset = transform.position - GetMouseWorldPosition();
    }

    private void OnMouseUp()
    {
        isDragging = false;
        SnapTileToGrid();
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void SnapTileToGrid()
    {
        if (targetGrid == null || tilemap == null) return;

        Vector3Int cellPosition = targetGrid.WorldToCell(transform.position);

        if (IsValidPlace(cellPosition))
        {
            Vector3 snappedPosition = targetGrid.CellToWorld(cellPosition);

            snappedPosition.x += targetGrid.cellSize.x / 2;
            snappedPosition.y += targetGrid.cellSize.y / 2;
            snappedPosition.z = transform.position.z;

            transform.position = snappedPosition;
        }
        else
        {
            transform.position = startPosition;
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
        Collider2D hit = Physics2D.OverlapBox(cellWorldPos, checkSize, 0f);

        if (hit != null && hit.gameObject != gameObject)
        {
            Debug.Log("this place is already in use");
            return false;
        }

        return true;
    }




}