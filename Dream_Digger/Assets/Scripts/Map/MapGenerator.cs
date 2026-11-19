using System;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

public class MapGenerator : SingletonBase<MapGenerator>
{
    // 가로 길이
    public int XRangeSetter;
    // 세로 길이
    public int YRangeSetter { get; private set; }
    // 생성할 타일들
    public List<AreaData> AreaSetter;
    public Dictionary<int, AreaData> AreaDict;

    protected override void Awake()
    {
        base.Awake();
        SetYRange();
    }

    private void OnEnable()
    {
        
    }

    private void Start()
    {
        Debug.Log($"가로 / 세로 : {XRangeSetter} / {YRangeSetter}");
    }

    private void SetYRange()
    {
        foreach (AreaData data in AreaSetter)
        {
            YRangeSetter += data.Range;
        }
    }

    public void GenerateMap() // 타일맵 생성 함수
    {
        MapController.Instance.CurrentTileMap.ClearAllTiles(); // 타일맵 소거
        Vector3Int temp = new();
        for (int a = 0; a < AreaSetter.Count; a++)
        {
            for (int b = 0; b < AreaSetter[a].Range; b++)
            {
                for (int c = 0; c < XRangeSetter; c++)
                {
                    temp.x = c;
                    MapController.Instance.CurrentTileMap.SetTile(temp, AreaSetter[a].Tile);
                }

                temp.y --;
            }
        }
    }
}

[Serializable]
public struct AreaData
{
    public int Range;
    public BreakableTile Tile;
}