using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]   
public class MemoryTile : TileBase
{
    public Sprite DefaultSprite;
    [SerializeField] private Tile.ColliderType _colliderType;
    
    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        tileData.sprite = DefaultSprite;
        tileData.colliderType = _colliderType;
    }
}
