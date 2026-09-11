using UnityEngine;

public class FloorTile : Tile
{
    [SerializeField] private Color baseColor, offsetColor;

    public override void Init(int x, int y)
    {
        bool isOffset = (x + y) % 2 == 1;
        spriteRenderer.color = isOffset ? offsetColor : baseColor;
    }

}
