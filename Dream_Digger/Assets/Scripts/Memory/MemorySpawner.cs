using System;
using System.Collections.Generic;
using UnityEngine;

public class MemorySpawner : SingletonBase<MemorySpawner>
{
    // 생성할 자원 종류 / 자원 당 개수
    public List<MemoryData> MemoryList;
    public Dictionary<MemoryType, MemoryResource> MemoryDict;
    public MemoryType Type;

    protected override void Awake()
    {
        base.Awake();
        MemoryDict = new();
    }

    private void Start()
    {
        for (int a = 0; a < MapGenerator.Instance.AreaSetter.Count; a++)
        {
            for (int b = 0; b < MapGenerator.Instance.AreaSetter[a].TypeList.Count; b++)
            {
                Debug.Log($"{a}구간 등장 광물은 {MapGenerator.Instance.AreaSetter[a].TypeList[b].Type}");
            }
        }

        int areaSize = MapGenerator.Instance.AreaSetter[0].Range * MapGenerator.Instance.XRangeSetter;
        Debug.Log($"0번 구역 크기는 {areaSize}");
        float spawnRate = MapGenerator.Instance.AreaSetter[0].Density / 100.0f;
        Debug.Log($"광물 등장 밀도는 {spawnRate}");
        int totalSpawnCount = (int)(areaSize * spawnRate);
        Debug.Log($"0번 구역 등장 광물 갯수는 {totalSpawnCount}");
    }
}

[Serializable]
public struct MemoryData
{
    public int Count;
    public MemoryResource Memory;
}
