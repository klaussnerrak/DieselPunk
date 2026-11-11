using TMPro;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager instance;
    public int levelReward = 0;
    private int usedTrainTracks = 0;
    private int destroyedObstacles = 0;
    private int collectedResources = 0;
    private int timeResource = 0;
    private int speedUpResource = 0;
    private bool hasUsedTime = false;
    private bool hasUsedSpeedUp = false;

    public TMP_Text usedTrainTracksText;
    public TMP_Text destroyedObstaclesText;
    public TMP_Text collectedResourcesText;
    public TMP_Text timeResourceText;
    public TMP_Text speedUpResourceText;
    public TMP_Text levelRewardText;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void CalculateScore()
    {
        Debug.Log("I will calculate the score");
        /*
            Calculo:
            Trilhos = 10 diesel cada
            Obstaculos destruidos = +5 diesel cada
            Recursos coletados = +15 diesel cada
            Usou velocidade = tira 20% do valor total
            Usou tempo = tira 15% do valor total
        */
        double totalScore = (GridManager.instance.Track.Count * 10)
                            + (destroyedObstacles * 5)
                            + (collectedResources * 15);

        if (hasUsedTime) totalScore = totalScore - (totalScore * .2);

        if (hasUsedSpeedUp) totalScore = totalScore - (totalScore * .15);


        Debug.Log("track amount:" + GridManager.instance.Track.Count);
        
        Debug.Log("total score" + totalScore);

        usedTrainTracks = GridManager.instance.Track.Count; 
        destroyedObstaclesText.SetText($"{destroyedObstacles}");
        collectedResourcesText.SetText($"{collectedResources}");
        timeResourceText.SetText($"{timeResource}");
        speedUpResourceText.SetText($"{speedUpResource}");
        levelRewardText.SetText($"{totalScore}");


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

    public void ResetPlayerScore()
    {
        levelReward = 0;
        destroyedObstacles = 0;
        collectedResources = 0;
        timeResource = 0;
        speedUpResource = 0;
        hasUsedTime = false;
        hasUsedSpeedUp = false;
    }
}