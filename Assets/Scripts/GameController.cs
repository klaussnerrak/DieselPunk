
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private TMP_Text playerDieselText;

    [SerializeField] private TMP_Text usedTrainTracksText;
    [SerializeField] private TMP_Text destroyedObstaclesText;
    [SerializeField] private TMP_Text collectedResourcesText;
    [SerializeField] private TMP_Text collectedResources2Text;
    [SerializeField] private TMP_Text timeResourceText;
    [SerializeField] private TMP_Text speedUpResourceText;
    [SerializeField] private TMP_Text levelRewardText;

    public static int levelIndex = 1;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (ShopManager.instance != null)
        {
            ShopManager.instance.playerDieselText = playerDieselText;
            ShopManager.instance.SetShopAmount();
            ScoreManager.instance.ResetPlayerScore();
        }


        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.ResetPlayerScore();
        }
    }


    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.ResetPlayerScore();
        }
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }



    public void WinCondition()
    {
        gamePanel.SetActive(false);
        winPanel.SetActive(true);
        ScoreManager.instance.CalculateScore();
        SetGameScore();
        RemoveTrainTracks();
        ShopManager.instance.playerDiesel = ShopManager.instance.playerDiesel + ScoreManager.instance.levelReward;
        levelIndex++;
        AudioManager.instance.PlaySFX("Supla");
        ShopManager.instance.SetShopAmount();
    }

    public void LoseCondition()
    {
        gamePanel.SetActive(false);
        losePanel.SetActive(true); 
        RemoveTrainTracks(); 
        AudioManager.instance.PlaySFX("HAHAHA");
    }

    public void BackToMapScene()
    {
        Debug.Log("Quit");
        SceneManager.LoadScene("Scenes/MapScene");
        AudioManager.instance.PlayMusic("MapMusic");


    }
    public void RestartScene()
    {
        SceneManager.LoadScene("Scenes/Level1");
        ScoreManager.instance.ResetPlayerScore();
        levelIndex = 1;
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    private void SetGameScore()
    {
        ScoreManager.instance.SetScoreText(
            usedTrainTracksText,
            destroyedObstaclesText,
            collectedResourcesText,
            collectedResources2Text,
            timeResourceText,
            speedUpResourceText,
            levelRewardText
        );

    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (ShopManager.instance != null)
        {
            ShopManager.instance.playerDieselText = playerDieselText;
            ShopManager.instance.SetShopAmount();
        }
    }

    void RemoveTrainTracks()
    {
        TrainTrack[] tracks = FindObjectsByType<TrainTrack>(FindObjectsSortMode.None);
        foreach (var track in tracks)
        {
            // Destroy unplaced or bought tracks, or whatever your design requires:
            if (track.isBought)
            {
                // Destroy(track.gameObject);
                track.gameObject.SetActive(false);
            }
        }
    }
}

