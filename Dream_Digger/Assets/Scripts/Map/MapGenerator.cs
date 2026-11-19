using System;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : SingletonBase<MapGenerator>
{
    // 맵의 총 가로 길이
    public int XRangeSetter;
    
    // 생성할 타일들
    public List<AreaData> AreaSetter;
    
    public void GenerateMap() // 타일맵 생성
    {
        MapController.Instance.CurrentTileMap.ClearAllTiles(); // 타일맵 소거
        Vector3Int tempVector3Int = new(); // 임시 Vector3Int
        List<int> tempYValueArray = new(); // 임시 구역 별 좌표 Y값 배열
        int loopCount = 0; // 반복문 루프 횟수 체크
        for (int a = 0; a < AreaSetter.Count; a++) // a = 구역 인덱스
        {
            for (int b = 0; b < AreaSetter[a].Range; b++) // b = 구역 세로길이 인덱스
            {
                
                for (int c = 0; c < XRangeSetter; c++) // c = 구역 가로 길이 인덱스
                {
                    tempVector3Int.x = c;
                    MapController.Instance.CurrentTileMap.SetTile(tempVector3Int, AreaSetter[a].Tile);
                }
                
                tempYValueArray.Add(tempVector3Int.y);
                Debug.Log($"키 {a}에 담은 y좌표: {tempVector3Int.y}");
                tempVector3Int.y --;
                loopCount++;
            }
            Debug.Log($"{a} 구역 디버그합니다.");
            MapController.Instance.AreaLengthDict.Add(a, tempYValueArray);
            Debug.Log($"{a} 구역 세로 배열 길이: {tempYValueArray.Count}");
            tempYValueArray.Clear();
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