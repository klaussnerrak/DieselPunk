using UnityEngine;

public class ResourceScript : MonoBehaviour
{
    [SerializeField] private int dieselPoints; 
    [SerializeField] private GameObject trainWagonPrefab;
    [SerializeField] private float wagonSpawnDistance = 1f;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Train")
        {
            ShopManager.instance.playerDiesel = ShopManager.instance.playerDiesel + dieselPoints;
            ShopManager.instance.SetShopAmount();
 
            SpawnTrainWagon(col.transform);

            Destroy(gameObject);
        }
    }

    private void SpawnTrainWagon(Transform trainTransform)
    {
        if (trainWagonPrefab == null)
        {
            Debug.LogWarning($"[ResourceScript] Train Wagon prefab is not assigned on {gameObject.name}.", this);
            return;
        }
 
        Vector3 spawnPosition = trainTransform.position - trainTransform.right * wagonSpawnDistance;
        GameObject wagon = Instantiate(
            trainWagonPrefab,
            spawnPosition,
            trainTransform.rotation
        );
 
        wagon.tag = "TrainWagon"; 
        TrainWagonScript wagonScript = wagon.GetComponent<TrainWagonScript>();
        if (wagonScript != null)
        {
            wagonScript.SetTrain(trainTransform);
        }
    }
}
