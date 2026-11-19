using System;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    // 가로 길이
    public int XRangeSetter;
    // 세로 길이
    public int YRangeSetter { get; private set; }
    // 생성할 타일들
    public List<AreaData> AreaSetter;

    private void Awake()
    {
        foreach (AreaData data in AreaSetter)
        {
            YRangeSetter += data.Range;
        }
    }

    private void Start()
    {
        Debug.Log($"가로 / 세로 : {XRangeSetter} / {YRangeSetter}");
    }

    private void DivideArea()
    {
        
    }

    private void GenerateMap()
    {
        
    }
}

[Serializable]
public struct AreaData
{
    public int Range;
    public BreakableTile Tile;
}