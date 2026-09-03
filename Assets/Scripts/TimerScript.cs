using UnityEngine;
using TMPro;
public class TimerScript : MonoBehaviour
{
    public float timeCounter = 10f;
    public float playCounter = 10f;
    [SerializeField] private TMP_Text timerText;
    public static TimerScript instance;
    public bool pauseTimer = false;
    
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
        if(timeCounter>1 && pauseTimer==false)
        {
            timeCounter -= Time.deltaTime;
            updateText(timeCounter);
        }
        
    }

    public void StartPlayCounter()
    {    
        timeCounter = playCounter;
        if(pauseTimer==false)
        {          
            AudioManager.instance.PlaySFX("TrainHorn");
        }  
    }

    void updateText(float currentTime)
    {
        float seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("00:{0:00}",seconds);
    }
}
