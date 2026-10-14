using UnityEngine;
using TMPro;
using System;
public class TimerScript : MonoBehaviour
{
    public float timeCounter = 10f;
    public float playCounter = 50f;
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
        if (timeCounter > 1 && pauseTimer == false)
        {
            timeCounter -= Time.deltaTime;
            UpdateText(timeCounter);
        }

    }

    public void StartPlayCounter()
    {
        timeCounter = playCounter;
        if (pauseTimer == false)
        {
            // AudioManager.instance.PlaySFX("TrainHorn");
        }
    }

    void UpdateText(float currentTime)
    {
        float displayTime = Mathf.Max(0, currentTime);
        TimeSpan time = TimeSpan.FromSeconds(displayTime);

        timerText.text = string.Format("{0}:{1:D2}", (int)time.TotalMinutes, time.Seconds);
    }

    public void AddBonusSeconds(float secondsToAdd)
    {
        timeCounter += secondsToAdd;
        UpdateText(timeCounter);
    }
}
