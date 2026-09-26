
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    [SerializeField] private  GameObject winPanel;
    [SerializeField] private  GameObject losePanel;
    [SerializeField] private  GameObject gamePanel;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {        
        instance = this;
    }

    void Start()
    {
        
    }
    
    public void WinCondition()
    {        
        gamePanel.SetActive(false);
        winPanel.SetActive(true);
        // AudioManager.instance.PlaySFX("Supla");
    }

    public void LoseCondition()
    {
        losePanel.SetActive(true);
        gamePanel.SetActive(false);
        AudioManager.instance.PlaySFX("HAHAHA");
    }   

    public void BackToMapScene()
    {
        Debug.Log("Go");
        SceneManager.LoadScene("Scenes/MapScene");        
        AudioManager.instance.PlayMusic("MapMusic");
        
    }
}
    
