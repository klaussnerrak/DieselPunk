
using UnityEngine;

public class TrainTrackShop : MonoBehaviour
{
    private GameObject selectedPrefab;
    [SerializeField] private Transform shopButton;
    // [SerializeField] private Transform piecesContainer;
    [SerializeField] private int coins = 10;
    private DraggableItem selectedPiece;

    private Camera mainCamera;
    void Start()
    {
        mainCamera = Camera.main;
    }
    public void BuyTrack(GameObject selectedTile)
    {
        Debug.Log("PIECE BOUGHT"); 
        
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        selectedPrefab = Instantiate(
            selectedTile,
            shopButton.position,
            Quaternion.identity
        );
    }


}