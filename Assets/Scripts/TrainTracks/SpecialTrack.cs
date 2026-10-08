using UnityEngine;
public enum SpecialTrackType
{
    Time,
    Speed 
}
public class SpecialTrack : MonoBehaviour
{

    [SerializeField] private SpecialTrackType type;
    [SerializeField] private int dieselCost;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
