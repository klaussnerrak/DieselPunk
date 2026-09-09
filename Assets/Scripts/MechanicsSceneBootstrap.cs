using UnityEngine;
using UnityEngine.EventSystems;

public class MechanicsSceneBootstrap : MonoBehaviour
{
    private const int Width = 7;
    private const int Height = 5;
    private const float CellSize = 1f;

    private MapManager board;
    private RailPlayerMovement player;
    private Transform mapEnd;
    private Vector3Int startCell = new Vector3Int(0, 2, 0);

    private Sprite squareSprite;

    private void Awake()
    {
        Camera camera = Camera.main;
        if (camera == null)
        {
            GameObject cameraObject = new GameObject("Main Camera");
            camera = cameraObject.AddComponent<Camera>();
            cameraObject.tag = "MainCamera";
        }

        camera.orthographic = true;
        camera.orthographicSize = 4.4f;
        camera.transform.position = new Vector3(0.5f, 0f, -10f);
        camera.backgroundColor = new Color(0.08f, 0.1f, 0.14f);
        if (camera.GetComponent<Physics2DRaycaster>() == null)
            camera.gameObject.AddComponent<Physics2DRaycaster>();

        if (FindFirstObjectByType<EventSystem>() == null)
        {
            GameObject eventSystem = new GameObject("EventSystem");
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>();
        }

        GameObject gridObject = new GameObject("Rail Grid");
        Grid grid = gridObject.AddComponent<Grid>();
        grid.cellSize = Vector3.one * CellSize;
        grid.transform.position = new Vector3(-3f, -2f, 0f);

        board = gameObject.GetComponent<MapManager>();
        if (board == null)
            board = gameObject.AddComponent<MapManager>();
        board.Configure(grid, camera);
        squareSprite = CreateSquareSprite();

        CreateBoard();
        CreateShop();
        CreatePlayer();
    }

    private void CreateBoard()
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                Vector3 position = board.Grid.GetCellCenterWorld(new Vector3Int(x, y, 0));
                GameObject tile = CreateSpriteObject("Grid Cell", position, new Color(0.16f, 0.2f, 0.24f), 0);
                tile.transform.localScale = Vector3.one * 0.96f;
            }
        }

        CreateFixedPiece("Start Track", startCell, TrackType.Straight, false, false, true, false);
        CreateFixedPiece("Start Track 2", new Vector3Int(1, 2, 0), TrackType.Straight, false, false, true, true);

        mapEnd = CreateSpriteObject("MapEnd", board.Grid.GetCellCenterWorld(new Vector3Int(Width - 1, 2, 0)),
            new Color(0.25f, 0.85f, 0.45f), 4).transform;
        mapEnd.localScale = Vector3.one * 0.72f;
        CreateFixedPiece("End Track", new Vector3Int(Width - 1, 2, 0), TrackType.Straight, false, false, false, true);
    }

    private void CreateShop()
    {
        CreateShopSource("Straight", new Vector3(5.1f, 3f), TrackType.Straight, 1, false, false, true, true);
        CreateShopSource("Curve", new Vector3(6.2f, 3f), TrackType.Curve, 2, true, false, true, false);
        CreateShopSource("T Junction", new Vector3(7.3f, 3f), TrackType.T, 3, true, false, true, true);
    }

    private void CreatePlayer()
    {
        GameObject playerObject = CreateSpriteObject("Player", board.Grid.GetCellCenterWorld(startCell),
            new Color(0.95f, 0.35f, 0.25f), 5);
        playerObject.transform.localScale = Vector3.one * 0.55f;
        player = playerObject.AddComponent<RailPlayerMovement>();
        player.Configure(board, mapEnd, startCell);
    }

    private void CreateShopSource(string name, Vector3 position, TrackType type, int price,
        bool north, bool south, bool east, bool west)
    {
        GameObject source = CreateSpriteObject(name + " Shop Piece", position, new Color(0.95f, 0.78f, 0.25f), 3);
        source.AddComponent<BoxCollider2D>().size = Vector2.one * 0.8f;
        source.AddComponent<TrackShopSource>().Configure(board, type, price, north, south, east, west);
    }

    private void CreateFixedPiece(string name, Vector3Int cell, TrackType type,
        bool north, bool south, bool east, bool west)
    {
        GameObject piece = CreateSpriteObject(name, board.Grid.GetCellCenterWorld(cell),
            new Color(0.65f, 0.68f, 0.72f), 2);
        TrackPiece track = piece.AddComponent<TrackPiece>();
        track.Configure(type, north, south, east, west);
        DraggableItem draggable = piece.AddComponent<DraggableItem>();
        draggable.Configure(board);
        board.Register(draggable);
        piece.AddComponent<RailVisual>().Configure(type, north, south, east, west, 3);
    }

    private GameObject CreateSpriteObject(string name, Vector3 position, Color color, int sortingOrder)
    {
        GameObject result = new GameObject(name);
        result.transform.position = position;
        SpriteRenderer renderer = result.AddComponent<SpriteRenderer>();
        renderer.sprite = squareSprite;
        renderer.color = color;
        renderer.sortingOrder = sortingOrder;
        return result;
    }

    private Sprite CreateSquareSprite()
    {
        Texture2D texture = new Texture2D(32, 32);
        texture.filterMode = FilterMode.Point;
        Color[] pixels = new Color[32 * 32];
        for (int i = 0; i < pixels.Length; i++)
            pixels[i] = Color.white;
        texture.SetPixels(pixels);
        texture.Apply();
        return Sprite.Create(texture, new Rect(0, 0, 32, 32), Vector2.one * 0.5f, 32f);
    }

    private void OnGUI()
    {
        GUI.color = Color.white;
        GUI.Label(new Rect(20, 20, 500, 30), "DRAG AND DROP - conecte os trilhos");
        GUI.Label(new Rect(20, 48, 500, 24), "Clique numa peca da loja para comprar, depois arraste-a para o grid.");
        GUI.Label(new Rect(20, 76, 500, 24), "Loja: reta $1 | curva $2 | T $3");
        if (GUI.Button(new Rect(20, 110, 180, 36), "Partir trem"))
            player.TryStart();
    }
}
