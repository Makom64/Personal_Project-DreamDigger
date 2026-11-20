using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

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
        List<int> tempYValueList = new(); // 임시 구역 별 좌표 Y값 리스트
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
                
                tempYValueList.Add(tempVector3Int.y);
                Debug.Log($"키 {a}에 담은 y좌표: {tempVector3Int.y}");
                tempVector3Int.y --;
                loopCount++;
            }
            Debug.Log($"{a} 구역 디버그합니다.");
            /*임시 구역 별 좌표 Y값 리스트가 아래에서 초기화 되기 때문에
            딕셔너리에 바로 추가하면 참조 형식이라 딕셔너리의 값도 초기화가 되버린다.
            따라서 깊은 복사를 거쳐 딕셔너리에 담는다.*/
            List<int> listKeeper = new List<int>(tempYValueList); // 구역 내 좌표 Y값 깊은 복사 리스트
            MapController.Instance.AreaLengthDict.Add(a, listKeeper);
            Debug.Log($"{a} 구역 세로 배열 길이: {tempYValueList.Count}");
            Debug.Log($"딕셔너리 키 : {a} 값 길이 :{MapController.Instance.AreaLengthDict[a].Count}");
            tempYValueList.Clear();
        }

        // Debug.Log($"{MapController.Instance.AreaLengthDict.ContainsKey(0)}");
        // Debug.Log($"딕셔너리 키 0값 길이 : {MapController.Instance.AreaLengthDict[0].Count}");
        // Debug.Log($"딕셔너리 키 0의 1번 인덱스 : {MapController.Instance.AreaLengthDict[0][1]}");
        // MemorySpawner.Instance.ShuffleAreaPositions();
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