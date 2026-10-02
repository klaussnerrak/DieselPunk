
using UnityEngine;

public class TrainTrackShop : MonoBehaviour
{
    private TrainTrack selectedTrack;
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
            selectedTrack = selectedTile.GetComponent<TrainTrack>();
        }

        if (selectedTrack != null)
        {
            // Agora o UpdateTrackShop vai ler o preço correto do Prefab!
            UpdateTrackShop();

            Instantiate(
                selectedTile,
                shopButton.position,
                Quaternion.identity
            );
        } 

    }

    private void UpdateTrackShop()
    { 
        ShopManager.instance.playerDiesel = ShopManager.instance.playerDiesel - selectedTrack.dieselCost;
        ShopManager.instance.setShopAmount();  
    }

}