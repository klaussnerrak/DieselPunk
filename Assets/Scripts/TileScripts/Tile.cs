using UnityEngine;

public abstract class Tile : MonoBehaviour
{
 
    [SerializeField] private GameObject highlight; 
    [SerializeField] protected SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Init(int x, int y)
    {
        
    }

    void OnMouseEnter()
    {
        highlight.SetActive(true); 
    }

    void OnMouseExit()
    {  
        highlight.SetActive(false);
    }
 
}
