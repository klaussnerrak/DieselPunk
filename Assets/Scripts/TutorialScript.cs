using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class TutorialScript : MonoBehaviour

{
    [SerializeField] private Button ExitButton;

    void Start()
    {
        ExitButton.onClick.AddListener(BackToMap);
        Debug.Log("test");
    }
    public void BackToMap()
    {
        Debug.Log("Quit");        
        SceneManager.LoadScene("Scenes/MapScene");        
        AudioManager.instance.PlayMusic("MapMusic");  
               
    }
}
