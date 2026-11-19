using System;
using System.Collections.Generic;
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

    private void SetYRange()
    {
        foreach (AreaData data in AreaSetter)
        {
            YRangeSetter += data.Range;
        }
    }

    public void GenerateMap() // 타일맵 생성
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
    [Header("구간 내 등장 기억 등급")]
    public List<MemoryWeight> TypeList;

    [Header("구간 별 기억 밀도")]
    [Range(0, 100)] public int Density;
    
    [Header("타일 별 세로 길이")]
    public int Range;
    
    [Header("설치할 타일")]
    public BreakableTile Tile;
}

[Serializable]
public struct MemoryWeight
{
    [Header("가중치")]
    [Range(0.0f, 10.0f)] public float Weight;
    
    [Header("기억 종류")]
    public MemoryType Type;
}