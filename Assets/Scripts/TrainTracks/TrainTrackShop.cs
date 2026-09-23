
using UnityEngine;

public class TrainTrackShop : MonoBehaviour
{
    private TrainTrack selectedTrack;
    [SerializeField] private Transform shopButton;
    [SerializeField] private int playerDiesel = 100;

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

        selectedTrack = GameObject.FindObjectOfType<TrainTrack>();

        playerDiesel -= selectedTrack.dieselCost;
        Debug.Log("selected tile cost: " + selectedTrack.dieselCost);
        // if(selectedTile.tileCost)

        Instantiate(
                    selectedTile,
                    shopButton.position,
                    Quaternion.identity
                );
    }


}