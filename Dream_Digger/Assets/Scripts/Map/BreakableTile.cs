using System;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]   
public class BreakableTile : TileBase
{
    public Sprite DefaultSprite;
    [SerializeField] private Tile.ColliderType _colliderType;

    public int MaxBites; // 최대 내구도

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        tileData.sprite = DefaultSprite;
        tileData.colliderType = _colliderType;
    }
}
