using UnityEngine;

public class ResourceScript : MonoBehaviour
{
    [SerializeField] private Sprite backGroundSprite;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.name == "Train")
        {
            SpriteRenderer myResource = GetComponent<SpriteRenderer>();
            myResource.sprite = backGroundSprite;
        }
    }
}
