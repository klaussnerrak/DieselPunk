
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
        Debug.Log("Shop button Z: "+shopButton.position.z);
        //     if (index < 0 || index >= availablePieces.Length)
        //         throw new System.ArgumentOutOfRangeException(nameof(index));

        //     TrainTrackData data = availablePieces[index];
        //     if (data == null || data.prefab == null)
        //         throw new MissingReferenceException("The selected track has no prefab.");

        //     if (coins < data.price || price != data.price)
        //         return;

        //     if (piecesContainer == null || board == null)
        //         throw new MissingReferenceException("TrainTrackShop requires a container and a MapManager.");

        //     coins -= data.price;
        // Transform selectedWorldPosition = Input.mousePosition;
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        
        selectedPrefab = Instantiate(
            selectedTile,
            shopButton.position,
            Quaternion.identity
        );
        // pieceObject.SetActive(true);
        //     DraggableItem piece = pieceObject.GetComponent<DraggableItem>();
        //     if (piece == null)
        //         throw new MissingComponentException("Track prefabs require DraggableItem.");

        //     piece.Configure(board);
        //     board.Register(piece); 
        // selectedPrefab.transform.position = mainCamera.ScreenToWorldPoint(mousePosition);  
    }


}