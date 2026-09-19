using UnityEngine;
using NavMeshPlus.Components;  
using System.Collections.Generic;


public class GridManager : MonoBehaviour
{
    //[SerializeField] private NavMeshSurface navSurface;
    public List <TrainTrack> Track = new List <TrainTrack>();
 
    /*public void UpdateNavMeshAfterDrop()
    { 
        Debug.Log("baked");
        navSurface.UpdateNavMesh(navSurface.navMeshData);
    }*/

    public void AddToTrack(TrainTrack tile)
    {
        Debug.Log(tile.type);
        Track.Add(tile);
        
    }

}    
    