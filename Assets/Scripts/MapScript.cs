using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartLevel1()
    {
        SceneManager.LoadScene("Scenes/StartScene");        
        //AudioManager.instance.PlayMusic("MenuTrainSound");
        
    }
}
