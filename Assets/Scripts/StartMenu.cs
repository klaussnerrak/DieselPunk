using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button tutorialButton;
    
    [SerializeField] private  GameObject CreditsPanel;
    [SerializeField] private  GameObject OptionsPanel;
    [SerializeField] private  GameObject MenuPanel;
    [SerializeField] private  GameObject TutorialPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startButton.onClick.AddListener(StartGame);
        exitButton.onClick.AddListener(ExitGame);
        optionsButton.onClick.AddListener(OptionsScreen);
        creditsButton.onClick.AddListener(CreditsScreen);
        tutorialButton.onClick.AddListener(TutorialScreen);
        CreditsPanel.SetActive(false);
        OptionsPanel.SetActive(false);
        MenuPanel.SetActive(true);
        TutorialPanel.SetActive(false);
        AudioManager.instance.PlayMusic("MenuTrainSound");
        

    }

    public void StartGame()
    {
        SceneManager.LoadScene("Scenes/MapScene");        
        AudioManager.instance.PlayMusic("MenuTrainSound");
        
    }
    public void OptionsScreen()
    {
        OptionsPanel.SetActive(true);
        CreditsPanel.SetActive(false);
        TutorialPanel.SetActive(false);
    }

    public void CreditsScreen()
    {
        CreditsPanel.SetActive(true);
        OptionsPanel.SetActive(false);
    }

    public void TutorialScreen()
    {
        TutorialPanel.SetActive(true);
        OptionsPanel.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void BackToMenu()
    {
        CreditsPanel.SetActive(false);
        OptionsPanel.SetActive(false);        
    }

    

    
}

