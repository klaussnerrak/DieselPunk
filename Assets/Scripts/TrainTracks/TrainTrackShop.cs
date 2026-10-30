
using UnityEngine;

public class TrainTrackShop : MonoBehaviour
{
    [SerializeField] private Transform shopButton;
    [SerializeField] private Vector3 trackStorePosition;

    private Camera mainCamera;
    void Start()
    {
        mainCamera = Camera.main;

        GameObject gridManagerObject = GameObject.Find("Grid");
    }
 
    public void BuyItem(GameObject selectedItem)
    {
        BuyItem(selectedItem, trackStorePosition);
    }
 
    public void BuyItem(GameObject selectedItem, Vector3 trackStorePosition)
    {
        if (selectedItem == null) return;

        TrainTrack track = selectedItem.GetComponent<TrainTrack>();
        SpecialTrack specialTrack = selectedItem.GetComponent<SpecialTrack>();

        if (track != null)
        {
            if (CheckDieselAmount(track.dieselCost))
            {
                GameObject purchasedTrack = Instantiate(selectedItem, trackStorePosition, Quaternion.identity);
                purchasedTrack.transform.localScale = Vector3.one;
                track.isBought = true;
            }
            else
            { 
                track.transform.position = trackStorePosition;
                Debug.Log("Not enough diesel");
            }
        }
        else if (specialTrack != null)
        {
            if (CheckDieselAmount(specialTrack.dieselCost))
            {
                switch (specialTrack.type)
                {
                    case SpecialTrackType.TIME:
                        specialTrack.IncreaseTime();
                        break;
                    case SpecialTrackType.SPEED:
                        specialTrack.IncreasePlayerSpeed();
                        break;
                }
            }
            else
            {
                Debug.Log("Can't use special resource - not enough diesel");
            }
        }
        else
        {
            Debug.Log("Selected item has no Track or SpecialTrack component");
        }
    }
 
    private bool CheckDieselAmount(int dieselCost)
    {
        if (ShopManager.instance.playerDiesel >= dieselCost)
        {
            UpdateTrackShop(dieselCost);
            return true;
        }
        return false;
    }

    private void UpdateTrackShop(int dieselCost)
    {
        ShopManager.instance.playerDiesel = ShopManager.instance.playerDiesel - dieselCost;
        ShopManager.instance.SetShopAmount();
    }

}