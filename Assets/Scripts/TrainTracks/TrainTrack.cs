using UnityEngine;

public enum TrackType
{
    Straight,
    Curve,
    Cross,
    Bridge,
    End,
    Bomb
}

public class TrainTrack : MonoBehaviour
{
    public TrackType type; 
    public int dieselCost;
    public GameObject up;
    public GameObject down;
    public GameObject right;
    public GameObject left;

    public bool isBought = false;
    public bool upConnected = false;
    public bool downConnected = false;
    public bool rightConnected = false;
    public bool leftConnected = false;

    public bool TrackLocked = false;

    public bool isDragging = false;

       


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