using UnityEngine;
public enum SpecialTrackType
{
    TIME,
    SPEED
}
public class SpecialTrack : MonoBehaviour
{

    public SpecialTrackType type;
    public int dieselCost;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void IncreasePlayerSpeed()
    { 
    }


    public void IncreaseTime()
    {
        Debug.Log("called increase time being called by special track click");
        TimerScript.instance.AddBonusSeconds(15f); 
    }
}
