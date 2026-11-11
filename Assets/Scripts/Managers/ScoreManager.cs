using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager instance;
    public int  playerExtraDiesel = 0;
    public int  destroyedObstacles = 0;
    public int  collectedResources = 0;
    public int  timeResource = 0;
    public int  speedUpResource = 0;
    private bool hasUsedTime = false; 
    private bool hasUsedSpeedUp = false; 


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void CalculateScore()
    {

    }

    public void SaveScoreLog(GameObject item)
    {
        Debug.Log("item tag:" + item.tag);
        if (item != null)
        {
            switch (item.tag)
            {
                case "TrainWagon":
                    collectedResources++;
                    break;
                case "Obstacle":
                    destroyedObstacles++;
                    break;
                case "SpecialResource":
                    ValidateSpecialResource(item);
                    break;
            }
        }

    }

    private void ValidateSpecialResource(GameObject specialResource)
    {
        if (specialResource.name.Equals("SpeedButton"))
        {
            hasUsedSpeedUp = true;
            speedUpResource++;
            Debug.Log("has used speed up set to true");
        }
        else if (specialResource.name.Equals("TimeButton"))
        {
            hasUsedTime = true;
            timeResource++;
            Debug.Log("has used time increase set to true");
        }
    }
}
