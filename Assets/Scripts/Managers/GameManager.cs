using NavMeshPlus.Components;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
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

    void Update()
    {

    }
}

