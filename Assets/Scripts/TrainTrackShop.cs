using UnityEngine;

public class TrainTrackShop : MonoBehaviour
{
    [SerializeField] private TrainTrackData[] availablePieces;
    [SerializeField] private Transform piecesContainer;
    [SerializeField] private MapManager board;
    [SerializeField] private int coins = 10;
    private DraggableItem selectedPiece;

    public void BuyTrack(int index)
    {
        if (index < 0 || index >= availablePieces.Length)
            throw new System.ArgumentOutOfRangeException(nameof(index));

        BuyTrack(index, availablePieces[index].price);
    }

    public void BuyTrack(int index, int price)
    {
        if (index < 0 || index >= availablePieces.Length)
            throw new System.ArgumentOutOfRangeException(nameof(index));

        TrainTrackData data = availablePieces[index];
        if (data == null || data.prefab == null)
            throw new MissingReferenceException("The selected track has no prefab.");

        if (coins < data.price || price != data.price)
            return;

        if (piecesContainer == null || board == null)
            throw new MissingReferenceException("TrainTrackShop requires a container and a MapManager.");

        coins -= data.price;

        GameObject pieceObject = Instantiate(
            data.prefab,
            piecesContainer.position,
            Quaternion.identity,
            piecesContainer
        );

        DraggableItem piece = pieceObject.GetComponent<DraggableItem>();
        if (piece == null)
            throw new MissingComponentException("Track prefabs require DraggableItem.");

        piece.Configure(board);
        board.Register(piece);
        pieceObject.SetActive(true);
        selectedPiece = piece;
    }

    public void RotateSelectedPiece()
    {
        if (selectedPiece == null)
            return;

        TrackPiece track = selectedPiece.GetComponent<TrackPiece>();
        if (track == null)
            throw new MissingComponentException("Track prefabs require TrackPiece.");

        track.RotatePiece();
    }
}