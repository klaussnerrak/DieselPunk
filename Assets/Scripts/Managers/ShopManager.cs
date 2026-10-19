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
            playerDiesel = 200; 
        }
    }
    
    public void SetShopAmount()
    {
        //Debug.Log(playerDiesel);
        playerDieselText.SetText($"Diesel \n{playerDiesel}");
    }

    public void SellTrack(TrainTrack selectedTrainTrack)
    {
        playerDiesel += (selectedTrainTrack.dieselCost / 2);
        playerDieselText.SetText($"Diesel \n{playerDiesel}"); 
    }






}
