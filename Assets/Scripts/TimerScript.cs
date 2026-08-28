using UnityEngine;
using TMPro;
public class TimerScript : MonoBehaviour
{
    public float timeCounter = 10f;
    [SerializeField] private TMP_Text timerText;
    public static TimerScript instance;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        
    }

    void Update()
    {
        if(timeCounter>1)
        {
            timeCounter -= Time.deltaTime;
            updateText(timeCounter);
        }
        
    }

    void updateText(float currentTime)
    {
        float seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("00:{0:00}",seconds);
    }
}
