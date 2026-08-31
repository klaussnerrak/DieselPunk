using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    [SerializeField] private  GameObject winPanel;
    [SerializeField] private  GameObject losePanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {        
        instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WinCondition()
    {
        winPanel.SetActive(true);
        AudioManager.instance.PlaySFX("Supla");
    }
    public void LoseCondition()
    {
        losePanel.SetActive(true);
        AudioManager.instance.PlaySFX("HAHAHA");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Finish");
        
    }
}
    
