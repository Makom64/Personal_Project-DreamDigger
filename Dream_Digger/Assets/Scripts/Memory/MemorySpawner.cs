using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = System.Random;

public class MemorySpawner : SingletonBase<MemorySpawner>
{
    public Tilemap MemoryLayer;
    public List<MemoryResource> MemoryResources;
    public Dictionary<MemoryType, GameObject> MemoryDict;
    public Dictionary<MemoryType, MemoryTile> TileDict;

    public int[] DiceArray = new int [6];

    private void temp()
    {
        DiceArray[0] = 1;
        DiceArray[1] = 2;
        DiceArray[2] = 3;
        DiceArray[3] = 4;
        DiceArray[4] = 5;
        DiceArray[5] = 6;
    }

    public void temptemp(AreaData areaData)
    {
        areaData = MapGenerator.Instance.AreaSetter[0];
    }

    public void ShuffleDice()
    {
        Random random = new Random();
        for (int a = 0; a < DiceArray.Length; a++)
        {
            int b = random.Next(a + 1, DiceArray.Length);

            int temp = DiceArray[a];
            DiceArray[a] = DiceArray[b];
            DiceArray[b] = temp;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        MemoryDict = new();
        TileDict = new();
        for (int i = 0; i < MemoryResources.Count; i++)
        {
            MemoryDict.Add(MemoryResources[i].MemoryData.Type, MemoryResources[i].gameObject);
            TileDict.Add(MemoryResources[i].MemoryData.Type, MemoryResources[i].MemoryData.Tile);
        }
        MemoryLayer = GetComponent<Tilemap>();
    }

    // 어떻게 그릴 것인가?
    // 광물을 그리는 타일맵
    // 좌표 받은거 있고 스프라이트
    // 스프라이트는 어떤 기준으로 판단? 열거형으로 일치 ㅇㅇ

    public void SetMemoryOnTile()
    {
        // a번째 구역을 검사한다.
        for (int a = 0; a < MapController.Instance.PositionsPerAreaDict.Count; a++)
        {
            // a번 구역의 넓이
            int areaSize = MapController.Instance.PositionsPerAreaDict[a].Count;
            // a번 구역의 기억 밀도
            int density = MapGenerator.Instance.AreaSetter[a].Density;
            // 넓이 * (밀도 / 100): a번 구역 내 설치될 기억 수
            float totalAreaMemory = areaSize * (density / 100f);
            // a번 구역에 등장하는 기억 리스트의 b번을 더한다.
            float totalMemoryWeight = 0.0f;
            for (int b = 0; b < MapGenerator.Instance.AreaSetter[a].TypeList.Count; b++)
            {
                totalMemoryWeight += MapGenerator.Instance.AreaSetter[a].TypeList[b].Weight;
            }
            // 최대 기억 개수 / 가중치 합 = 평균 기억 생성 개수
            float averageMemoryCount = (totalAreaMemory / totalMemoryWeight);
            // (b번 기억 가중치) * (평균 기억 생성 개수) = b번 기억 생성 개수
            int positionIndex = 0;
            for (int b = 0; b < MapGenerator.Instance.AreaSetter[a].TypeList.Count; b++)
            {
                int countPerMemory = (int)(MapGenerator.Instance.AreaSetter[a].TypeList[b].Weight * averageMemoryCount);
                
                // 기억 종류가 Special이면 b번 기억 생성 개수 1개로 고정
                if (MapGenerator.Instance.AreaSetter[a].TypeList[b].Type == MemoryType.Special)
                {
                    countPerMemory = 1;
                }

                
                // b번 기억 생성 개수(c)만큼 좌표에 그린다.
                for (int c = 0; c < countPerMemory; c++)
                {
                    // 좌표 리스트의 맨 앞부터 채운다.
                    Debug.Log($"{a} 구역에 기억 배치함");
                    // b 생성 갯수가 3이였다면
                    // b++ 번은 4번부터 시작해야됨
                    MapController.Instance.SpawnedMemoryDict.Add(
                        MapController.Instance.PositionsPerAreaDict[a][positionIndex],
                        MapGenerator.Instance.AreaSetter[a].TypeList[b].Type);
                    // 스프라이트 배치
                    if (MapGenerator.Instance.AreaSetter[a].TypeList[b].Type != MemoryType.Special)
                    {
                        MemoryLayer.SetTile(
                            MapController.Instance.PositionsPerAreaDict[a][positionIndex],
                            TileDict[MapGenerator.Instance.AreaSetter[a].TypeList[b].Type]
                        );
                    }
                    positionIndex++;
                }
                // b번 끝나면 b+1번 기억 차례
            }
        }
        
    }
    
    // Fisher Yates Shuffle로 구역 별 좌표 리스트를 섞는다.
    public void ShuffleAreaPositionList()
    {
        Random random = new();
        for (int a = 0; a < MapGenerator.Instance.AreaSetter.Count; a++)
        {
            for (int b = MapController.Instance.PositionsPerAreaDict[a].Count - 1; b > 0; b--)
            {
                // 0번째 요소부터 최대치(a+1) 전까지 랜덤을 넣어준다.
                int c = random.Next(0, b + 1);

                // a번째 값을 따로 빼둔다.
                Vector3Int temp = MapController.Instance.PositionsPerAreaDict[a][b];
            
                // 랜덤한 b번 요소를 a번에 넣는다.
                MapController.Instance.PositionsPerAreaDict[a][b] =
                    MapController.Instance.PositionsPerAreaDict[a][c];
            
                // b에는 따로 빼둔 a번째 값을 넣는다.
                MapController.Instance.PositionsPerAreaDict[a][c] = temp;
            }
        }
    }
    
    // 앞에서부터 하나씩 기억 정보 묶음에 담아준다.
}