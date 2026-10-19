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
    private int xMin;
  
    [SerializeField] private GameObject mapEnd;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject firstTrack;


    void Start()
    {
        tilemap.CompressBounds();
        bounds = tilemap.cellBounds;
        xMin = bounds.xMin;  
    } 
}
