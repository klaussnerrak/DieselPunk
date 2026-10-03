using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapScript : MonoBehaviour
{
    public static MapScript instance;
    private int levelIndex;
    [SerializeField] private Button Level2Button;
    [SerializeField] private Button Level3Button;
    [SerializeField] private Button Level4Button;
    [SerializeField] private Button Level5Button;
    
    void Start()
    {
        AddLevel(GameController.levelIndex);
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
        SceneManager.LoadScene("Scenes/Level2");             
        AudioManager.instance.PlayMusic("PlayMusic");
        //ShopManager.instance.playerDiesel = ShopManager.instance.playerDiesel + 150;     
        
    }
    public void StartLevel3()
    {
        //SceneManager.LoadScene("Scenes/StartScene"); 
        SceneManager.LoadScene("Scenes/Level3");             
        AudioManager.instance.PlayMusic("PlayMusic");
        
    }
    public void StartLevel4()
    {
        //SceneManager.LoadScene("Scenes/StartScene"); 
        SceneManager.LoadScene("Scenes/Level4");             
        AudioManager.instance.PlayMusic("PlayMusic");
        
    }
    public void StartLevel5()
    {
        //SceneManager.LoadScene("Scenes/StartScene"); 
        SceneManager.LoadScene("Scenes/Level5");             
        AudioManager.instance.PlayMusic("PlayMusic");
        
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    private void AddLevel(int level)
    {
        switch (level)
            {
                case  2:
                    Level2Button.gameObject.SetActive(true);
                    break;

                case  3:
                    Level2Button.gameObject.SetActive(true);
                    Level3Button.gameObject.SetActive(true);
                    break;

                case  4:
                    Level2Button.gameObject.SetActive(true);
                    Level3Button.gameObject.SetActive(true);
                    Level4Button.gameObject.SetActive(true);
                    break;

                case  5:
                    Level2Button.gameObject.SetActive(true);
                    Level3Button.gameObject.SetActive(true);
                    Level4Button.gameObject.SetActive(true);
                    Level5Button.gameObject.SetActive(true);
                    break;            
            }
    }
}
