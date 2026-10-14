
using UnityEngine;

public class TrainTrackShop : MonoBehaviour
{
    private TrainTrack selectedTrack;
    private SpecialTrack specialTrack;
    [SerializeField] private Transform shopButton;

    private Camera mainCamera;
    void Start()
    {
        mainCamera = Camera.main;

        GameObject gridManagerObject = GameObject.Find("Grid");
    }
    public void BuyTrack(GameObject selectedTile)
    {

        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;


        if (selectedTile != null)
        {
            if (selectedTile.CompareTag("SpecialResource"))
            {
                specialTrack = selectedTile.GetComponent<SpecialTrack>(); 
            }
            else
            {
                selectedTrack = selectedTile.GetComponent<TrainTrack>(); 
            }
        }


        if (selectedTrack != null)
        {
            if (ShopManager.instance.playerDiesel >= selectedTrack.dieselCost)
            {
                UpdateTrackShop(selectedTrack.dieselCost);

                Instantiate(
                    selectedTile,
                    shopButton.position,
                    Quaternion.identity
                );
            }
            else
            {
                Debug.Log("can't generate train track");
            }
        }
        else if (specialTrack != null)
        {
            if (ShopManager.instance.playerDiesel >= specialTrack.dieselCost)
            {
                UpdateTrackShop(specialTrack.dieselCost);

                switch (specialTrack.type)
                {
                    case SpecialTrackType.TIME:
                        {
                            specialTrack.IncreaseTime();
                        }
                        break;
                    case SpecialTrackType.SPEED:
                        {
                            specialTrack.IncreasePlayerSpeed();
                        }
                        break;
                }
            }
            else
            {
                Debug.Log("Can't use special resource");
            }
        }

    }


    private void UpdateTrackShop(int dieselCost)
    {
        ShopManager.instance.playerDiesel = ShopManager.instance.playerDiesel - dieselCost;
        ShopManager.instance.SetShopAmount();
    }

}