using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum MapAssetType
{
    TrainTrack,
    Scenario
}

public class MapManager : MonoBehaviour
{
    [Header("Tilemap config")]
    BoundsInt bounds;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Grid layoutGrid;
    private int xMin;

    [SerializeField] private GameObject mapEnd; 
    [SerializeField] private GameObject firstTrack;


    void Start()
    {
        tilemap.CompressBounds();
        bounds = tilemap.cellBounds;
        xMin = bounds.xMin;
        GenerateEndMap();
    }


    public void GeneratePlayer(GameObject player)
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
            Instantiate(firstTrack, startWorldPos, rotation);
        }
        else
        {
            Debug.Log("Espaço ocupado, rodando novamente");
            GeneratePlayer(player);
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
        mapEnd.name = "MapEnd";
    }
}
