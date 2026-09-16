using UnityEngine;
using NavMeshPlus.Components;  


public class GridManager : MonoBehaviour
{
    [SerializeField] private NavMeshSurface navSurface;
 
    public void UpdateNavMeshAfterDrop()
    {  
        navSurface.UpdateNavMesh(navSurface.navMeshData);
    }
}