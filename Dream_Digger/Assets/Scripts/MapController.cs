using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapController : MonoBehaviour
{
    private Dictionary<Vector3Int, int> _bitePerCell; // 셀 당 내구도
    private Vector3Int[] _cellPositions; // 셀 위치 배열

    public static MapController instance; // 클래스 전체에 하나
    public Tilemap CurrentTileMap;

    private void Awake()
    {
        CurrentTileMap = GetComponent<Tilemap>();
        _bitePerCell = new();
        if (instance == null)
        {
            instance = this;
        }
    }

    private void OnEnable()
    {
    }

    private void Start()
    {
        
    }
}
