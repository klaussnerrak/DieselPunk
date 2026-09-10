using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width;
    [SerializeField] private int _height;
    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private Transform cameraTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateGrid();
    }

    // // Update is called once per frame
    // void Update()
    // {

    // }

    void GenerateGrid()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";

                CheckOffset(x, y);
            }
        }

        cameraTransform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10);

    }

    private bool CheckOffset(int x, int y)
    {
        var isGridOffset = (x + y) % 2 == 1;
        return isGridOffset;
    }
}
