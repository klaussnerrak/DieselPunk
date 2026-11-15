using UnityEngine;

public class ResourceScript : MonoBehaviour
{
    [SerializeField] private int dieselPoints; 
    [SerializeField] private GameObject trainWagonPrefab;
    [SerializeField] private float wagonSpawnDistance = 1f;

    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("colision");
        if (col.gameObject.name == "Train")
        {
            ShopManager.instance.playerDiesel = ShopManager.instance.playerDiesel + dieselPoints;
            ShopManager.instance.SetShopAmount();
 
            SpawnTrainWagon(col.gameObject);

            Destroy(gameObject);
        }
    }

    private void SpawnTrainWagon(GameObject train)
    {
        if (trainWagonPrefab == null)
        {
            Debug.LogWarning($"[ResourceScript] Train Wagon prefab is not assigned on {gameObject.name}.", this);
            return;
        }
 
       // Vector3 spawnPosition = train.transform.position - train.transform.right * wagonSpawnDistance;
        PlayerTrainScript.instance.wagonNumber++;
        int trackIndex = PlayerTrainScript.instance.trackIndex - PlayerTrainScript.instance.wagonNumber;
        Vector3 spawnPosition = GridManager.instance.Track[trackIndex].transform.position;
        GameObject wagon = Instantiate(
            trainWagonPrefab,
            spawnPosition,
            train.transform.rotation
        );
        
       
 
        wagon.tag = "TrainWagon"; 
        TrainWagonScript wagonScript = wagon.GetComponent<TrainWagonScript>();
        if (wagonScript != null)
        {
            wagonScript.SetTrain(train.transform);
            wagonScript.wagonIndex = PlayerTrainScript.instance.wagonNumber;
        }
    }
}
