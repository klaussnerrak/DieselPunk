using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] private Grid grid;
    [SerializeField] private Camera mainCamera;

    private readonly Dictionary<Vector3Int, DraggableItem> occupiedCells = new();

    public Grid Grid => grid;

    public void Configure(Grid mapGrid, Camera cameraToUse)
    {
        grid = mapGrid;
        mainCamera = cameraToUse;
    }

    private void Awake()
    {
        if (grid == null)
            grid = GetComponentInChildren<Grid>();

        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    public Vector3Int ScreenToCell(Vector2 screenPosition)
    {
        if (grid == null || mainCamera == null)
            throw new MissingReferenceException("MapManager requires a Grid and a camera.");

        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(
            new Vector3(screenPosition.x, screenPosition.y, -mainCamera.transform.position.z));
        return grid.WorldToCell(worldPosition);
    }

    public Vector3 SnapToGrid(Vector2 screenPosition)
    {
        return grid.GetCellCenterWorld(ScreenToCell(screenPosition));
    }

    public bool TryPlace(DraggableItem item, Vector2 screenPosition, out Vector3 snappedPosition)
    {
        Vector3Int cell = ScreenToCell(screenPosition);

        if (occupiedCells.TryGetValue(cell, out DraggableItem occupant) && occupant != item)
        {
            snappedPosition = item.transform.position;
            return false;
        }

        Remove(item);
        occupiedCells[cell] = item;
        snappedPosition = grid.GetCellCenterWorld(cell);
        return true;
    }

    public void Register(DraggableItem item)
    {
        if (item == null || grid == null)
            throw new MissingReferenceException("MapManager requires a Grid and a valid item.");

        Vector3Int cell = grid.WorldToCell(item.transform.position);
        if (!occupiedCells.TryGetValue(cell, out DraggableItem occupant) || occupant == item)
            occupiedCells[cell] = item;
    }

    public void Remove(DraggableItem item)
    {
        Vector3Int cell = grid.WorldToCell(item.transform.position);
        if (occupiedCells.TryGetValue(cell, out DraggableItem occupant) && occupant == item)
            occupiedCells.Remove(cell);
    }

    public bool IsCellAvailable(Vector3 position)
    {
        Vector3Int cell = grid.WorldToCell(position);
        return !occupiedCells.ContainsKey(cell);
    }

    public bool TryGetPiece(Vector3Int cell, out TrackPiece piece)
    {
        if (occupiedCells.TryGetValue(cell, out DraggableItem item))
        {
            piece = item.GetComponent<TrackPiece>();
            return piece != null;
        }

        piece = null;
        return false;
    }
}