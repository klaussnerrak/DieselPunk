using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;
    public int playerDiesel;
    public TMP_Text playerDieselText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            playerDiesel = 100;
            //playerDieselText.SetText($"Diesel \n{playerDiesel}");
        }
    }

    

    /*void Start()
    {
        if (playerDieselText != null)
        {
            playerDieselText.SetText($"Diesel \n{playerDiesel}");
        }
    }  */  

    public void SetShopAmount()
    {
        Debug.Log(playerDiesel);
        playerDieselText.SetText($"Diesel \n{playerDiesel}");
    }

      public void SellTrack(TrainTrack selectedTrainTrack)
    {
        playerDiesel += (selectedTrainTrack.dieselCost / 2);  
        playerDieselText.SetText($"Diesel \n{playerDiesel}");
    }

    



   
}
