using System;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]   
public class BreakableTile : TileBase
{
    [SerializeField] private Sprite _tempSprite;
    [SerializeField] private Tile.ColliderType _colliderType;

    public int Durability;

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        tileData.sprite = _tempSprite;
        tileData.colliderType = _colliderType;
    }
}
