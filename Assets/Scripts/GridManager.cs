using UnityEngine;
using NavMeshPlus.Components;


public class GridManager : MonoBehaviour
{
    [SerializeField] private NavMeshSurface navSurface; 
    [SerializeField] private GameObject player;
    [SerializeField] private MapManager mapManager;

    void Start()
    {
        navSurface.BuildNavMesh();
        if (navSurface != null)
        {
            mapManager.GeneratePlayer(player);
        }
    }
    public void UpdateNavMeshAfterDrop()
    {
        navSurface.UpdateNavMesh(navSurface.navMeshData);
    }
}