using UnityEngine;
using NavMeshPlus.Components;  
using System.Collections.Generic;


public class GridManager : MonoBehaviour
{
    //[SerializeField] private NavMeshSurface navSurface;
    public List <TrainTrack> TrackTiles = new List <TrainTrack>();
    public List <TrainTrack> Track = new List <TrainTrack>();
    public static GridManager instance;
    
    public void Start()
    { 
        instance = this;
        //UpdateTrackStack();
    }

    public void AddToTrack(TrainTrack tile)
    {        
        TrackTiles.Add(tile);
    }

    public void UpdateTrack()
    {
       Track.Clear();
       Track.Add(TrackTiles[0]);
        
        //Debug.Log(TrackTiles.Count);
        for(int i = 0; i<Track.Count; i++)
        {
            TrainTrack currentTrack = Track[i];
            for(int j = 0; j<TrackTiles.Count; j++)
            {
                TrainTrack nextTrack = TrackTiles[j];
                if(nextTrack!=currentTrack){
                    if(IsConnected(currentTrack, nextTrack))
                    {
                    //Debug.Log(TrackTiles[j].transform.position);
                        Track.Add(TrackTiles[j]);                   
                    
                    }
                }               
            }
           
        }
         //Debug.Log(Track.Count);
    }   

    private bool IsConnected(TrainTrack track1, TrainTrack track2)
    {
        Debug.Log(track1 + "right " +track1.right +" "+track1.rightConnected);
        /*Debug.Log(track1 + "left " +track1.leftConnected);
        Debug.Log(track1 + "up " +track1.upConnected);
        Debug.Log(track1 + "down " +track1.downConnected);*/
        //Up check********************************************************************************************                   
    
        if(track1.up != null && track1.upConnected==false){
            if(track2.up != null 
                && track2.upConnected == false
                && Vector3.Distance(track1.up.transform.position,track2.up.transform.position)<0.1)
            {
                track1.upConnected = true;
                track2.upConnected = true;
                return true;
            }
            else if(track2.down != null 
                && track2.downConnected == false
                && Vector3.Distance(track1.up.transform.position,track2.down.transform.position)<0.1)
            {
                track1.upConnected = true;
                track2.downConnected = true;
                return true;
            }
            else if(track2.left != null 
                && track2.leftConnected == false
                && Vector3.Distance(track1.up.transform.position,track2.left.transform.position)<0.1)
            {
                track1.upConnected = true;
                track2.leftConnected = true;
                return true;
            }
            else if(track2.right != null 
                && track2.rightConnected == false
                && Vector3.Distance(track1.up.transform.position,track2.right.transform.position)<0.1)
            {
                track1.upConnected = true;
                track2.rightConnected = true;
                return true;
            }                
            
        //Down check********************************************************************************************                   
    
        }else if(track1.down != null && track1.downConnected==false){
            if(track2.up != null 
                && track2.upConnected == false
                && Vector3.Distance(track1.down.transform.position,track2.up.transform.position)<0.1)
            {
                track1.downConnected = true;
                track2.upConnected = true;
                return true;
            }
            else if(track2.down != null 
                && track2.downConnected == false
                && Vector3.Distance(track1.down.transform.position,track2.down.transform.position)<0.1)
            {
                track1.downConnected = true;
                track2.downConnected = true;
                return true;
            }
            else if(track2.left != null 
                && track2.leftConnected == false
                && Vector3.Distance(track1.down.transform.position,track2.left.transform.position)<0.1)
            {
                track1.downConnected = true;
                track2.leftConnected = true;
                return true;
            }
            else if(track2.right != null 
                && track2.rightConnected == false
                && Vector3.Distance(track1.down.transform.position,track2.right.transform.position)<0.1)
            {
                track1.downConnected = true;
                track2.rightConnected = true;
                return true;
            } 

        //Right check********************************************************************************************                   
        }else if(track1.right != null && track1.rightConnected==false){   
            Debug.Log(track1 + "teste");         
            if(track2.up != null 
                && track2.upConnected == false
                && Vector3.Distance(track1.right.transform.position,track2.up.transform.position)<0.1)
            {
                track1.rightConnected = true;
                track2.upConnected = true;
                return true;
            }
            else if(track2.down != null 
                && track2.downConnected == false
                && Vector3.Distance(track1.right.transform.position,track2.down.transform.position)<0.1)
            {
                track1.rightConnected = true;
                track2.downConnected = true;
                return true;
            }
            else if(track2.left != null 
                && track2.leftConnected == false
                && Vector3.Distance(track1.right.transform.position,track2.left.transform.position)<0.1)
            {
               
                track1.rightConnected = true;
                track2.leftConnected = true;
                return true;
            }
            else if(track2.right != null 
                && track2.rightConnected == false
                && Vector3.Distance(track1.right.transform.position,track2.right.transform.position)<0.1)
            {
                track1.rightConnected = true;
                track2.rightConnected = true;
                return true;
            } 

        //Left check********************************************************************************************                   
                   
        }else if(track1.left != null && track1.leftConnected==false){
            if(track2.up != null 
                && track2.upConnected == false
                && Vector3.Distance(track1.left.transform.position,track2.up.transform.position)<0.1)
            {
                track1.leftConnected = true;
                track2.upConnected = true;
                return true;
            }
            else if(track2.down != null 
                && track2.downConnected == false
                && Vector3.Distance(track1.left.transform.position,track2.down.transform.position)<0.1)
            {
                track1.leftConnected = true;
                track2.downConnected = true;
                return true;
            }
            else if(track2.left != null 
                && track2.leftConnected == false
                && Vector3.Distance(track1.left.transform.position,track2.left.transform.position)<0.1)
            {
                track1.leftConnected = true;
                track2.leftConnected = true;
                return true;
            }
            else if(track2.right != null 
                && track2.rightConnected == false
                && Vector3.Distance(track1.left.transform.position,track2.right.transform.position)<0.1)
            {
                track1.leftConnected = true;
                track2.rightConnected = true;
                return true;
            }                
        } 
        //Debug.Log(track1.name);
        return false;
    }

    public void UpdateTrackStack()
    {
        TrainTrack[] UpdateTrack = FindObjectsByType<TrainTrack>(FindObjectsSortMode.None);        
        TrackTiles.AddRange(UpdateTrack); 
        for(int i = 0; i<TrackTiles.Count; i++)
        {
            if(TrackTiles[i].name == "(Fixed) Tile Straight")
            {
                TrainTrack auxTile = TrackTiles[0];
                TrackTiles[0] = TrackTiles[i];
                TrackTiles[i] = auxTile;
            }

        }                
    }
}    
    