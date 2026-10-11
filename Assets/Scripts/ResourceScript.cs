using UnityEngine;

public class ResourceScript : MonoBehaviour
{
    [SerializeField] private int dieselPoints;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.name == "Train")
        {
           ShopManager.instance.playerDiesel = ShopManager.instance.playerDiesel + dieselPoints;
            ShopManager.instance.SetShopAmount();
            
            Destroy(gameObject);
        }
    }
}
