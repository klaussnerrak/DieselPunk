using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class MechanicsDragAndDropSetup : MonoBehaviour
{
    [Header("Scene references")]
    [SerializeField] private MapManager board;
    [SerializeField] private TrainScript train;
    [SerializeField] private Sprite floorSprite;
    [SerializeField] private Sprite straightSprite;
    [SerializeField] private Sprite curveSprite;
    [SerializeField] private Sprite playerSprite;
    [SerializeField] private Sprite endSprite;

    [Header("Grid")]
    [SerializeField] private int columns = 7;
    [SerializeField] private int rows = 5;
    [SerializeField] private float cellSize = 1.5f;
    [SerializeField] private Vector3 gridOrigin = new Vector3(-4.5f, -3f, 0f);

    private readonly List<TrackType> shopInventory = new()
    {
        TrackType.Straight,
        TrackType.Curve,
        TrackType.T,
        TrackType.Cross
    };

    private Grid grid;
    private PathValidator pathValidator;
    private Transform piecesRoot;
    private int coins = 12;
    private bool initialized;

    private void Awake()
    {
        if (board == null)
            board = GetComponent<MapManager>();
        if (train == null)
            train = FindFirstObjectByType<TrainScript>();

        CreateGrid();
        CreateBoardVisuals();
        CreateEndpoints();

        pathValidator = GetComponent<PathValidator>();
        if (pathValidator == null)
            pathValidator = gameObject.AddComponent<PathValidator>();

        Vector3Int start = new Vector3Int(0, 2, 0);
        Vector3Int end = new Vector3Int(columns - 1, 2, 0);
        pathValidator.Configure(board, start, end);
        CreateMapEnd(end);

        if (train != null)
            train.Configure(pathValidator, GameObject.Find("MapEnd").transform);

        initialized = true;
    }

    private void CreateGrid()
    {
        GameObject gridObject = new GameObject("PuzzleGrid");
        gridObject.transform.SetParent(transform);
        gridObject.transform.position = gridOrigin;
        grid = gridObject.AddComponent<Grid>();
        grid.cellSize = new Vector3(cellSize, cellSize, 1f);
        board.Configure(grid, Camera.main);
        piecesRoot = new GameObject("PlacedTrackPieces").transform;
        piecesRoot.SetParent(transform);
    }

    private void CreateBoardVisuals()
    {
        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                GameObject cell = new GameObject($"Cell_{x}_{y}");
                cell.transform.SetParent(grid.transform);
                cell.transform.position = grid.GetCellCenterWorld(new Vector3Int(x, y, 0));
                SpriteRenderer renderer = cell.AddComponent<SpriteRenderer>();
                renderer.sprite = floorSprite;
                renderer.color = (x + y) % 2 == 0
                    ? new Color(0.16f, 0.19f, 0.22f)
                    : new Color(0.2f, 0.23f, 0.26f);
                renderer.sortingOrder = -10;
                cell.transform.localScale = Vector3.one * (cellSize / 1.6f);
            }
        }
    }

    private void CreateEndpoints(Vector3Int start, Vector3Int end)
    {
        CreateTrack(TrackType.Straight, start, straightSprite, false, false, true, false);
        CreateTrack(TrackType.Straight, end, straightSprite, false, false, false, true);

        GameObject player = new GameObject("Player");
        player.transform.position = grid.GetCellCenterWorld(start);
        SpriteRenderer playerRenderer = player.AddComponent<SpriteRenderer>();
        playerRenderer.sprite = playerSprite;
        playerRenderer.sortingOrder = 5;
        player.AddComponent<CircleCollider2D>();
        if (train == null)
            train = player.AddComponent<TrainScript>();
        else
            train.transform.SetPositionAndRotation(player.transform.position, Quaternion.identity);
    }

    private void CreateEndpoints()
    {
        CreateEndpoints(new Vector3Int(0, 2, 0), new Vector3Int(columns - 1, 2, 0));
    }

    private void CreateMapEnd(Vector3Int end)
    {
        GameObject mapEnd = new GameObject("MapEnd");
        mapEnd.transform.position = grid.GetCellCenterWorld(end);
        SpriteRenderer renderer = mapEnd.AddComponent<SpriteRenderer>();
        renderer.sprite = endSprite;
        renderer.sortingOrder = 4;
    }

    private void CreateTrack(TrackType type, Vector3Int cell, Sprite sprite, bool north, bool south, bool east, bool west)
    {
        GameObject pieceObject = new GameObject($"{type}Track_{cell.x}_{cell.y}");
        pieceObject.transform.SetParent(piecesRoot);
        pieceObject.transform.position = grid.GetCellCenterWorld(cell);
        SpriteRenderer renderer = pieceObject.AddComponent<SpriteRenderer>();
        renderer.sprite = sprite;
        renderer.sortingOrder = 1;
        pieceObject.AddComponent<BoxCollider2D>();
        DraggableItem draggable = pieceObject.AddComponent<DraggableItem>();
        TrackPiece trackPiece = pieceObject.AddComponent<TrackPiece>();
        trackPiece.Configure(type, north, south, east, west);
        draggable.Configure(board);
        board.Register(draggable);
    }

    private void AddPiece(int index)
    {
        if (!initialized || index < 0 || index >= shopInventory.Count || coins <= 0)
            return;

        TrackType type = shopInventory[index];
        Sprite sprite = type == TrackType.Curve ? curveSprite : straightSprite;
        GameObject pieceObject = new GameObject($"Purchased_{type}");
        pieceObject.transform.SetParent(piecesRoot);
        pieceObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(120f + index * 75f, 90f, 10f));
        SpriteRenderer renderer = pieceObject.AddComponent<SpriteRenderer>();
        renderer.sprite = sprite;
        renderer.sortingOrder = 2;
        pieceObject.AddComponent<BoxCollider2D>();
        DraggableItem draggable = pieceObject.AddComponent<DraggableItem>();
        TrackPiece trackPiece = pieceObject.AddComponent<TrackPiece>();
        bool north = true;
        bool south = type == TrackType.Straight || type == TrackType.Cross;
        bool east = type == TrackType.Curve || type == TrackType.T || type == TrackType.Cross;
        bool west = type == TrackType.T || type == TrackType.Cross;
        trackPiece.Configure(type, north, south, east, west);
        draggable.Configure(board);
        board.Register(draggable);
        coins--;
    }

    private void OnGUI()
    {
        if (!initialized)
            return;

        GUI.Box(new Rect(12f, 12f, 300f, 155f), "MECHANICS - DRAG AND DROP");
        GUI.Label(new Rect(28f, 42f, 260f, 24f), $"Peças disponíveis: {coins}");
        GUI.Label(new Rect(28f, 68f, 260f, 24f), "Arraste | Clique direito: girar");

        for (int i = 0; i < shopInventory.Count; i++)
        {
            if (GUI.Button(new Rect(28f + i * 66f, 98f, 60f, 32f), shopInventory[i].ToString()))
                AddPiece(i);
        }

        if (GUI.Button(new Rect(28f, 135f, 250f, 25f), "INICIAR TREM"))
            train.StartMovement();
    }
}
