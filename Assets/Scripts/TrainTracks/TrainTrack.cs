using UnityEngine;

public enum TrackType
{
    Straight,
    Curve,
    Cross
}

public class TrainTrack : MonoBehaviour
{
    public TrackType type; 
    public int dieselCost;
    public GameObject up;
    public GameObject down;
    public GameObject right;
    public GameObject left;

    public bool upConnected = false;
    public bool downConnected = false;
    public bool rightConnected = false;
    public bool leftConnected = false;
       


    /*public void Configure(TrackType pieceType, bool connectsNorth, bool connectsSouth,
        bool connectsEast, bool connectsWest)
    {
        type = pieceType;
        north = connectsNorth;
        south = connectsSouth;
        east = connectsEast;
        west = connectsWest;
    }*/
    

}