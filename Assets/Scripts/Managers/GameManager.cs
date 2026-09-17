using NavMeshPlus.Components;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private NavMeshSurface navSurface;
    [SerializeField] private GameObject player;
    [SerializeField] public MapManager mapManager;
    public static GameManager instance;

    void Start()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);

            if (navSurface != null)
            {
                mapManager.GeneratePlayer(player); 
                navSurface.BuildNavMesh();
            }
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    void Update()
    {

    }
}

