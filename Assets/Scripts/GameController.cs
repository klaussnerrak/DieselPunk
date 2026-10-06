
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    [SerializeField] private  GameObject winPanel;
    [SerializeField] private  GameObject losePanel;
    [SerializeField] private  GameObject gamePanel;
    [SerializeField] private TMP_Text playerDieselText;

    public static int levelIndex = 1;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {        
        instance = this;
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
   
    
    public void WinCondition()
    {        
        gamePanel.SetActive(false);
        winPanel.SetActive(true);
        AudioManager.instance.PlaySFX("Supla"); 
          
    }

    public void LoseCondition()
    {
        losePanel.SetActive(true);
        gamePanel.SetActive(false);
        AudioManager.instance.PlaySFX("HAHAHA");
    }   

    public void BackToMapScene()
    {
        levelIndex++;            
        SceneManager.LoadScene("Scenes/MapScene");        
        AudioManager.instance.PlayMusic("MapMusic");
         
               
    }
    public void RestartScene()
    {
        SceneManager.LoadScene("Scenes/Level1");
        levelIndex = 1;
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Loaded scene: " + scene.name);
        //playerDieselText.SetText($"Diesel \n{ShopManager.instance.playerDiesel}");
        ShopManager.instance.playerDieselText = playerDieselText;
        ShopManager.instance.SetShopAmount();
    }
}
    
