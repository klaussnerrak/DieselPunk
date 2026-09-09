using UnityEngine;

[CreateAssetMenu(menuName = "Train Puzzle/Track Piece")]
public class TrainTrackData : ScriptableObject
{
    public GameObject prefab;
    public int price;
    public TrackType type;
}