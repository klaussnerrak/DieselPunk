using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapScript : MonoBehaviour
{
    public static MapScript instance;

    void Awake()
    {        
        instance = this;
    }
    
    public void StartLevel1()
    {
        //SceneManager.LoadScene("Scenes/StartScene"); 
        SceneManager.LoadScene("Scenes/Level1");             
        AudioManager.instance.PlayMusic("PlayMusic");
        
    }
    public void StartLevel2()
    {
        //SceneManager.LoadScene("Scenes/StartScene"); 
        SceneManager.LoadScene("Scenes/HudScene");             
        AudioManager.instance.PlayMusic("PlayMusic");
        
    }
    public void StartLevel3()
    {
        //SceneManager.LoadScene("Scenes/StartScene"); 
        SceneManager.LoadScene("Scenes/HudScene");             
        AudioManager.instance.PlayMusic("PlayMusic");
        
    }
    public void StartLevel4()
    {
        //SceneManager.LoadScene("Scenes/StartScene"); 
        SceneManager.LoadScene("Scenes/HudScene");             
        AudioManager.instance.PlayMusic("PlayMusic");
        
    }
    public void StartLevel5()
    {
        //SceneManager.LoadScene("Scenes/StartScene"); 
        SceneManager.LoadScene("Scenes/HudScene");             
        AudioManager.instance.PlayMusic("PlayMusic");
        
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
