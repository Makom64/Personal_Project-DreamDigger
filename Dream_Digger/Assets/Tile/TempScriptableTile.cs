using System;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]   
public class TempScriptableTile : TileBase
{
    [SerializeField] private Sprite _tempSprite;
    
    [SerializeField] private Tile.ColliderType _colliderType;

    public bool GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        tileData.sprite = _tempSprite;
        tileData.colliderType = _colliderType;
        return true;
    }
}
