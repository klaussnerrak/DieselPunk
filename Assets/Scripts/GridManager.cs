using UnityEngine;
using NavMeshPlus.Components;  
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.Rendering;


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


    public void AddToTrackTiles
    (TrainTrack tile)
    {        
        TrackTiles.Add(tile);
    }

    /*public void UpdateTrack()
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
                    if(currentTrack.type == TrackType.Cross)
                    {
                        if(IsCrossConnected(currentTrack, nextTrack))
                        {
                            //Debug.Log(j);
                            Track.Add(TrackTiles[j]);
                            i++;
                            currentTrack = Track[i];
                        }                        
                        
                    }else if(IsConnected(currentTrack, nextTrack))
                    {
                        Track.Add(TrackTiles[j]);
                        i++;
                        currentTrack = Track[i];
                                           
                    }
                }               
            }
           
        }
         //Debug.Log(Track.Count);
    } */

    public void UpdateMovingTrack(TrainTrack track)
    {
       //Track.Clear();
       //Track.Add(TrackTiles[0]);
        
        //Debug.Log(TrackTiles.Count);
        //for(int i = 0; i<Track.Count; i++)
        {
            TrainTrack currentTrack = track;
            for(int j = 0; j<TrackTiles.Count; j++)
            {
                TrainTrack nextTrack = TrackTiles[j];
                if(nextTrack!=currentTrack){
                    if(currentTrack.type == TrackType.Cross)
                    {
                        if(IsCrossConnected(currentTrack, nextTrack))
                        {
                            //Debug.Log(j);
                            Track.Add(TrackTiles[j]);
                            TrackTiles[j].TrackLocked = true;
                            //i++;
                            //currentTrack = Track[i];
                        }                        
                        
                    }else if(IsConnected(currentTrack, nextTrack))
                    {
                        Track.Add(TrackTiles[j]);
                        TrackTiles[j].TrackLocked = true;
                        //i++;
                        //currentTrack = Track[i];
                                           
                    }
                }               
            }
        }         
    }

    public void updateRotation(TrainTrack track)
    {        
        if(Track !=null && Track.Count >=1)
        {
            if(track == Track[Track.Count-1])
            {
                if(track.leftConnected)
                {
                    track.leftConnected = false;
                    Track[Track.Count-2].rightConnected = false;               

                }else if(track.upConnected)
                {
                    track.upConnected = false;
                    Track[Track.Count-2].downConnected = false;

                }else if(track.downConnected)
                {
                    track.downConnected = false;
                    Track[Track.Count-2].upConnected = false;

                }else if(track.rightConnected)
                {
                    track.rightConnected = false;
                    Track[Track.Count-2].leftConnected = false;
                }

                //IsConnected(Track[Track.Count-2], track);

            }


        }
        
    }     

    public bool IsConnected(TrainTrack track1, TrainTrack track2)
    {    
        if(track2.isDragging==false)
        {   
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
    
        }
         if(track1.down != null && track1.downConnected==false){
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
        }
         if(track1.right != null && track1.rightConnected==false){   
               
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
                   
        }
         if(track1.left != null && track1.leftConnected==false){
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
        }
        
        //Debug.Log(track1.name);
        return false;
    }

   /* public void UpdateTrackStack()
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
    }*/

    public bool IsCrossConnected(TrainTrack track1, TrainTrack track2)
    {
        if(track2.isDragging == false)
        {
            
       
        // CROSS TILE ############################################################################################
       
        if(track2.type == TrackType.Cross){        
            if(track1.leftConnected && !track1.rightConnected)
            {
                if(track2.left.transform.position == track1.right.transform.position)
                {
                    track2.leftConnected = true;
                    track1.rightConnected = true;
                    return true;
                }else if(track2.right.transform.position == track1.right.transform.position)
                {
                    track2.rightConnected = true;
                    track1.rightConnected = true;
                    return true;
                
                }else if(track2.up.transform.position == track1.right.transform.position)
                {
                    track2.upConnected = true;
                    track1.rightConnected = true;
                    return true;
                }else if(track2.down.transform.position == track1.right.transform.position)
                {
                    track2.downConnected = true;
                    track1.rightConnected = true;
                    return true;
                }
            }else if(track1.rightConnected && !track1.leftConnected)
            {
                if(track2.left.transform.position == track1.left.transform.position)
                {
                    track2.leftConnected = true;
                    track1.leftConnected = true;
                    return true;

                }else if(track2.right.transform.position == track1.left.transform.position)
                {
                    track2.rightConnected = true;
                    track1.leftConnected = true;
                    return true;
                
                }else if(track2.up.transform.position == track1.left.transform.position)
                {
                    track2.upConnected = true;
                    track1.leftConnected = true;
                    return true;

                }else if(track2.down.transform.position == track1.left.transform.position)
                {
                    track2.downConnected = true;
                    track1.leftConnected = true;
                    return true;
                }

            }else if(track1.upConnected && !track1.downConnected)
            {
                if(track2.left.transform.position == track1.down.transform.position)
                {
                    track2.leftConnected = true;
                    track1.downConnected = true;
                    return true;

                }else if(track2.right.transform.position == track1.down.transform.position)
                {
                    track2.rightConnected = true;
                    track1.downConnected = true;
                    return true;
                
                }else if(track2.up.transform.position == track1.down.transform.position)
                {
                    track2.upConnected = true;
                    track1.downConnected = true;
                    return true;

                }else if(track2.down.transform.position == track1.down.transform.position)
                {
                    track2.downConnected = true;
                    track1.downConnected = true;
                    return true;
                }

            }else if(track1.downConnected && !track1.upConnected)
            {
                if(track2.left.transform.position == track1.up.transform.position)
                {
                    track2.leftConnected = true;
                    track1.upConnected = true;
                    return true;

                }else if(track2.right.transform.position == track1.up.transform.position)
                {
                    track2.rightConnected = true;
                    track1.upConnected = true;
                    return true;
                
                }else if(track2.up.transform.position == track1.up.transform.position)
                {
                    track2.upConnected = true;
                    track1.upConnected = true;
                    return true;

                }else if(track2.down.transform.position == track1.up.transform.position)
                {
                    track2.downConnected = true;
                    track1.upConnected = true;
                    return true;
                }
            }

        // STRAIGHT TILE ############################################################################################

        }else if(track2.type == TrackType.Straight)
        {
            if(track1.leftConnected && !track1.rightConnected)
            {
                if(track2.left.transform.position == track1.right.transform.position)
                {
                    track2.leftConnected = true;
                    track1.rightConnected = true;
                    return true;
                }else if(track2.right.transform.position == track1.right.transform.position)
                {
                    track2.rightConnected = true;
                    track1.rightConnected = true;
                    return true;                
                }
            }else if(track1.rightConnected && !track1.leftConnected)
            {
                if(track2.left.transform.position == track1.left.transform.position)
                {
                    track2.leftConnected = true;
                    track1.leftConnected = true;
                    return true;

                }else if(track2.right.transform.position == track1.left.transform.position)
                {
                    track2.rightConnected = true;
                    track1.leftConnected = true;
                    return true;              
                }
            }else if(track1.upConnected && !track1.downConnected)
            {
                if(track2.left.transform.position == track1.down.transform.position)
                {
                    track2.leftConnected = true;
                    track1.downConnected = true;
                    return true;

                }else if(track2.right.transform.position == track1.down.transform.position)
                {
                    track2.rightConnected = true;
                    track1.downConnected = true;
                    return true;
                }
                

            }else if(track1.downConnected && !track1.upConnected)
            {
                if(track2.left.transform.position == track1.up.transform.position)
                {
                    track2.leftConnected = true;
                    track1.upConnected = true;
                    return true;

                }else if(track2.right.transform.position == track1.up.transform.position)
                {
                    track2.rightConnected = true;
                    track1.upConnected = true;
                    return true;
                                
                }
            }
        
        // CURVE TILE ############################################################################################

        }else if(track2.type == TrackType.Curve)
        {
            if(track1.leftConnected && !track1.rightConnected)
            {
                
                if(track2.left.transform.position == track1.right.transform.position)
                {
                    track2.leftConnected = true;
                    track1.rightConnected = true;
                    return true;
                
                }else if(track2.down.transform.position == track1.right.transform.position)
                {
                    track2.downConnected = true;
                    track1.rightConnected = true;
                    return true;
                }
            }else if(track1.rightConnected && !track1.leftConnected)
            {
                if(track2.left.transform.position == track1.left.transform.position)
                {
                    track2.leftConnected = true;
                    track1.leftConnected = true;
                    return true;

                

                }else if(track2.down.transform.position == track1.left.transform.position)
                {
                    track2.downConnected = true;
                    track1.leftConnected = true;
                    return true;
                }

            }else if(track1.upConnected && !track1.downConnected)
            {
                if(track2.left.transform.position == track1.down.transform.position)
                {
                    track2.leftConnected = true;
                    track1.downConnected = true;
                    return true;

                

                }else if(track2.down.transform.position == track1.down.transform.position)
                {
                    track2.downConnected = true;
                    track1.downConnected = true;
                    return true;
                }

            }else if(track1.downConnected && !track1.upConnected)
            {
                if(track2.left.transform.position == track1.up.transform.position)
                {
                    track2.leftConnected = true;
                    track1.upConnected = true;
                    return true;

                

                }else if(track2.down.transform.position == track1.up.transform.position)
                {
                    track2.downConnected = true;
                    track1.upConnected = true;
                    return true;
                }
            

             }

        // END TILE ############################################################################################

        }else if(track2.type == TrackType.End)
        {
            
            if(track1.leftConnected && !track1.rightConnected)
            {                
               if(Vector3.Distance(track1.right.transform.position,track2.down.transform.position)<0.01)
                {
                    
                    track2.downConnected = true;
                    track1.rightConnected = true;
                    return true;
                }                

            }else if(track1.rightConnected && !track1.leftConnected)
            {
                if(Vector3.Distance(track1.left.transform.position,track2.down.transform.position)<0.01)
                {
                    track2.downConnected = true;
                    track1.leftConnected = true;
                    return true;
                }

            }else if(track1.upConnected && !track1.downConnected)
            {
                if(Vector3.Distance(track1.down.transform.position,track2.down.transform.position)<0.01)
                {
                    track2.downConnected = true;
                    track1.downConnected = true;
                    return true;
                }

            }else if(track1.downConnected && !track1.upConnected)
            {
                if(Vector3.Distance(track1.up.transform.position,track2.down.transform.position)<0.01)
                {
                    track2.downConnected = true;
                    track1.upConnected = true;
                    return true;
                }
            }
        }   

        }
        return false;
    }

      

    public bool IsNewTrack(TrainTrack track)
    {
        for(int i = 0; i<TrackTiles.Count; i++)
        {
            if(TrackTiles[i]==track)
                {
                    return false;
                }
        }

        return true;
    }
}    
    