
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created

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
        SetGameScore();
        ScoreManager.instance.CalculateScore();
        ShopManager.instance.playerDiesel = ShopManager.instance.playerDiesel + ScoreManager.instance.levelReward;
        levelIndex++;
        AudioManager.instance.PlaySFX("Supla");
        ShopManager.instance.SetShopAmount();
    }

    public void LoseCondition()
    {
        losePanel.SetActive(true);
        gamePanel.SetActive(false);
        SetGameScore();
        ScoreManager.instance.CalculateScore();
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
}

