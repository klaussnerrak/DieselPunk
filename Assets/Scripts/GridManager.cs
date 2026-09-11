using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager instance;
    private Dictionary<Vector2, Tile> tiles;
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private int width, height;
    [SerializeField] private Transform gameCamera;
    [SerializeField] private Tile floorTile, rockTile;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        
    } 

    // Update is called once per frame
    void Update()
    {

    }

    public void GenerateGrid()
    {
        tiles = new Dictionary<Vector2, Tile>();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var randomTile = Random.Range(0, 6) == 3?  rockTile : floorTile;
                Tile spawnedTile = Instantiate(randomTile, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}"; 

                spawnedTile.Init(x, y);

                tiles[new Vector2(x, y)] = spawnedTile;
            }
        }

        gameCamera.transform.position = new Vector3((float)width / 2 - 0.5f, (float)height / 2 - 0.5f, -10f);
    
        // GameManager.Instance.ChangeState(GameState.SpawnHeroes);
    }
 

    public Tile GetTileAtPosition(Vector2 position)
    {
        if (tiles.TryGetValue(position, out var tile))
        {
            return tile;
        }

        return null;
    }
}
