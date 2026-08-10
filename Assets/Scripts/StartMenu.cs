using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button optionsButton;
    
    //[SerializeField] private  GameObject CreditsPanel;
    [SerializeField] private  GameObject OptionsPanel;
    [SerializeField] private  GameObject MenuPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startButton.onClick.AddListener(StartGame);
        exitButton.onClick.AddListener(ExitGame);
        optionsButton.onClick.AddListener(OptionsScreen);
        //creditsButton.onClick.AddListener(CreditsScreen);
       // CreditsPanel.SetActive(false);
        OptionsPanel.SetActive(false);
        MenuPanel.SetActive(true);
        AudioManager.instance.PlayMusic("MenuTrainSound");
        

    }

    public void StartGame()
    {
        //SceneManager.LoadScene("Introducao");
        //CreditsPanel.SetActive(false);
        //AudioManager.instance.PlayMusic("MenuTrainSound");
        
    }
    public void OptionsScreen()
    {
        OptionsPanel.SetActive(true);
    }
    /*public void CreditsScreen()
    {
        CreditsPanel.SetActive(true);
    }*/
    public void ExitGame()
    {
        Application.Quit();
    }

    public void BackToMenu()
    {
        //CreditsPanel.SetActive(false);
        OptionsPanel.SetActive(false);
        MenuPanel.SetActive(true);
    }

    
}

