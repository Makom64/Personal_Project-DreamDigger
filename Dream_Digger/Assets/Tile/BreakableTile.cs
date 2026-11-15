using System;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]   
public class BreakableTile : TileBase
{
    [SerializeField] private Sprite _tempSprite;
    [SerializeField] private Tile.ColliderType _colliderType;

    public int RemainBites; // 블록의 내구도

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        tileData.sprite = _tempSprite;
        tileData.colliderType = _colliderType;
    }

    public void BiteTile(Vector3Int cellposition, int count)
    {
        Debug.Log(RemainBites);
        if (RemainBites > 0)
        {
            return;
        }
        else
        {
            MapController.instance._tilemap.SetTile(cellposition, null);
        }
    }
}
