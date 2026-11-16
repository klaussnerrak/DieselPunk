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
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); 
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

        if (hasUsedTime) totalScore -= totalScore * .15;
        if (hasUsedSpeedUp) totalScore -= totalScore * .20;

        levelReward = (int)totalScore;
        usedTrainTracks = GridManager.instance.Track.Count;
        if (usedTrainTracksText != null) usedTrainTracksText.SetText($"{usedTrainTracks}");
        if (destroyedObstaclesText != null) destroyedObstaclesText.SetText($"{destroyedObstacles}");
        if (collectedResourcesText != null) collectedResourcesText.SetText($"{collectedResources}");
        if (timeResourceText != null) timeResourceText.SetText($"{timeResource}");
        if (speedUpResourceText != null) speedUpResourceText.SetText($"{speedUpResource}");
        if (levelRewardText != null) levelRewardText.SetText($"{levelReward}");
    }

    public void SaveScoreLog(GameObject item)
    {
        if (item == null) { return; }
        switch (item.tag)
        {
            case "CollectableResource":
                collectedResources++;
                Debug.Log("[ScoreManager] collectedResources: " + collectedResources);
                break;
            case "Obstacle":
                destroyedObstacles++;
                Debug.Log("[ScoreManager] destroyedObstacles: " + destroyedObstacles);
                break;
            case "SpecialResource":
                ValidateSpecialResource(item);
                break;
        }
    }

    private void ValidateSpecialResource(GameObject specialResource)
    {
        SpecialTrack specialTrack = specialResource.GetComponent<SpecialTrack>();
        if (specialTrack == null) return;

        if (specialTrack.type == SpecialTrackType.SPEED)
        {
            hasUsedSpeedUp = true;
            speedUpResource++; 
        }
        else if (specialTrack.type == SpecialTrackType.TIME)
        {
            hasUsedTime = true;
            timeResource++; 
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


    public void SetScoreText(
        TMP_Text tracksText,
        TMP_Text obstaclesText,
        TMP_Text resourcesText,
        TMP_Text timeText,
        TMP_Text speedText,
        TMP_Text rewardText)
    {
        usedTrainTracksText = tracksText;
        destroyedObstaclesText = obstaclesText;
        collectedResourcesText = resourcesText;
        timeResourceText = timeText;
        speedUpResourceText = speedText;
        levelRewardText = rewardText;
    }
}