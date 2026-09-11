using UnityEngine;

public enum TrackType
{
    Straight,
    Curve, 
    Cross
}

public class TrainTrack : MonoBehaviour
{
    [SerializeField] protected TrackType type;
    [SerializeField] private bool north;
    [SerializeField] private bool south;
    [SerializeField] private bool east;
    [SerializeField] private bool west;
 
    public void Configure(TrackType pieceType, bool connectsNorth, bool connectsSouth,
        bool connectsEast, bool connectsWest)
    {
        type = pieceType;
        north = connectsNorth;
        south = connectsSouth;
        east = connectsEast;
        west = connectsWest;
    }
  
    public void RotatePiece()
    {
        transform.Rotate(0f, 0f, 90f);
    }
}