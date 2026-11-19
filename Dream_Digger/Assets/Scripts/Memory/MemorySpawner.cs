using System;
using System.Collections.Generic;
using UnityEngine;

public class MemorySpawner : SingletonBase<MemorySpawner>
{
    // 0번째 구역을 검사한다.
    // 0번째 구역의 넓이를 검사한다. 넓이는 총 칸 수다.
    // 0번째 구역의 밀도를 검사한다. 넓이 * (밀도 / 100)은 0번째 구역에 설치할 수 있는 최대 기억 개수이다.
    // 0번째 구역의 설정된 등장 기억 리스트를 검사한다.
    // 최대 기억 개수 / 가중치 합 = 평균 기억 생성 개수
    // 각 기억별 가중치 * (평균 기억 생성 개수) = 각 기억별 생성 개수
    // 최대 기억 개수 - (각 기억 별 생성 개수의 합) = 남은 개수
    // 남은 개수는 가중치가 가장 높은 광물 생성 개수에 더한다.
    // 
}

[Serializable]
public struct MemoryData
{
    public int Count;
    public MemoryResource Memory;
}
