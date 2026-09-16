
using UnityEngine;

public class TrainTrackShop : MonoBehaviour
{
    private GameObject selectedPrefab;
    [SerializeField] private Transform shopButton; 
    // [SerializeField] private int coins = 10; 

    private Camera mainCamera;
    void Start()
    {
        mainCamera = Camera.main;
    }
    public void BuyTrack(GameObject selectedTile)
    { 
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        selectedPrefab = Instantiate(
            selectedTile,
            shopButton.position,
            Quaternion.identity
        );
    }


}