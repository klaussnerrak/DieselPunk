
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

        //#TODO: Check if selectedTrack is null before accessing its properties and verify if it's a special resource or not

        if (selectedTrack == null)
        {
            Debug.Log("selectedTrack: " + selectedTrack.name + " dieselCost: " + selectedTrack.dieselCost);
            // return;
        }
        if (ShopManager.instance == null)
        {
            Debug.Log("Player Diesel: " + ShopManager.instance.playerDiesel);
        }
        // if (ShopManager.instance.playerDiesel >= selectedTrack.dieselCost)
        // {
        //     if (selectedTrack != null)
        //     {
        //         // Agora o UpdateTrackShop vai ler o preço correto do Prefab!
        //         UpdateTrackShop();

        //         Instantiate(
        //             selectedTile,
        //             shopButton.position,
        //             Quaternion.identity
        //         );
        //     }
        // }
        // else
        // {
        //     Debug.Log("num vai criar prefab coisa nenhuma");
        // }

    }

    private void UpdateTrackShop()
    {
        ShopManager.instance.playerDiesel = ShopManager.instance.playerDiesel - selectedTrack.dieselCost;
        ShopManager.instance.SetShopAmount();
    }

}