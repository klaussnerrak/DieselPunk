using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;
    public int playerDiesel;
    [SerializeField] private TMP_Text playerDieselText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //playerDiesel = 100;
            playerDieselText.SetText($"Diesel \n{playerDiesel}");
        }
    }

    void Start()
    {
        if (playerDieselText != null)
        {
            playerDieselText.SetText($"Diesel \n{playerDiesel}");
        }
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void SetShopAmount()
    {
        playerDieselText.SetText($"Diesel \n{playerDiesel}");
    }

      public void SellTrack(TrainTrack selectedTrainTrack)
    {
        playerDiesel += (selectedTrainTrack.dieselCost / 2);  
        playerDieselText.SetText($"Diesel \n{playerDiesel}");
    }
}
