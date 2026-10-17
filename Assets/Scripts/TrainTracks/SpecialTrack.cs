using UnityEngine;
public enum SpecialTrackType
{
    TIME,
    SPEED
}
public class SpecialTrack : MonoBehaviour
{

    public SpecialTrackType type;
    public PlayerTrainScript player;
    public int dieselCost;
    [SerializeField] private float velocity = 1.5f;
    [SerializeField] private float extraTime = 15f;
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
        player = FindFirstObjectByType<PlayerTrainScript>();
        if (player != null)
        { 
            player.AddPlayerSpeed(velocity);
        }

    }


    public void IncreaseTime()
    {
        //Debug.Log("called increase time being called by special track click");
        TimerScript.instance.AddBonusSeconds(extraTime);
    }
}
