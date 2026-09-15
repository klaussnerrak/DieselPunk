using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum MapAssetType
{
    TrainTrack,
    Scenario
}

public class MapAssetsManager : MonoBehaviour
{
    [Header("Tilemap config")]
    BoundsInt bounds;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Grid layoutGrid;
    [Range(0f, 100f)]
    [SerializeField] private float spawnChanceTrainTracks;
    [Range(0f, 100f)]
    [SerializeField] private float spawnChanceMapAssets;
    private int xMin;

    [Header("Track Prefabs")]
    [SerializeField] private GameObject[] trainTracks;
    [SerializeField] private GameObject[] mapAssets;

    [SerializeField] private GameObject mapEnd;
    [SerializeField] private GameObject player;


    void Start()
    {
        tilemap.CompressBounds();
        bounds = tilemap.cellBounds;
        xMin = bounds.xMin;
        // GenerateScenario(MapAssetType.TrainTrack);
        // GenerateScenario(MapAssetType.Scenario);
        GeneratePlayer();
        GenerateEndMap();
    }

    void GenerateScenario(MapAssetType type)
    {
        foreach (var pos in bounds.allPositionsWithin)
        {
            if (tilemap.HasTile(pos))
            {
                float randomizer = Random.Range(0f, 100f);
                Vector3 worldPosition = layoutGrid.CellToWorld(pos);

                worldPosition += new Vector3(layoutGrid.cellSize.x / 2f, layoutGrid.cellSize.y / 2f, 0);
                worldPosition.z = 0;

                if (type == MapAssetType.TrainTrack)
                {
                    if (randomizer > spawnChanceTrainTracks)
                        continue;

                    GameObject randomPrefab = trainTracks[Random.Range(0, trainTracks.Length)];

                    Instantiate(randomPrefab, worldPosition, Quaternion.identity, transform);
                }

                if (type == MapAssetType.Scenario)
                {
                    if (randomizer > spawnChanceMapAssets)
                        continue;

                    GameObject randomPrefab = mapAssets[Random.Range(0, mapAssets.Length)];

                    Instantiate(randomPrefab, worldPosition, Quaternion.identity, transform);
                }
            }
        }
    }

    void GeneratePlayer()
    {
        int startY = Random.Range(bounds.yMin, bounds.yMax);
        Vector3Int startCellPos = new Vector3Int(xMin, startY, 0);
        Vector3 startWorldPos = tilemap.GetCellCenterWorld(startCellPos);

        Collider2D hit = Physics2D.OverlapBox(startWorldPos, new Vector2(xMin, startY), 0f);


        if (hit == null)
        {
            Debug.Log("Espaço vazio encontrado!");
            Quaternion rotation = Quaternion.Euler(0, 0, -90f);
            Instantiate(player, startWorldPos, rotation); 
        }
        else
        {
            Debug.Log("Espaço ocupado, rodando novamente");
            GeneratePlayer();
        }

    }

    void GenerateEndMap()
    {
        int endX = Random.Range(bounds.xMax - 2, bounds.xMax); // Sorteia entre as duas últimas colunas
        int endY = Random.Range(bounds.yMin, bounds.yMax);
        Vector3Int endCellPos = new Vector3Int(endX, endY, 0);

        Vector3 endWorldPos = tilemap.GetCellCenterWorld(endCellPos);

        Quaternion endRotation = Quaternion.identity;

        if (endY == (bounds.yMax - 1))
        {
            endRotation = Quaternion.Euler(0, 0, -90f);
        }

        Instantiate(mapEnd, endWorldPos, endRotation);
    }
}
