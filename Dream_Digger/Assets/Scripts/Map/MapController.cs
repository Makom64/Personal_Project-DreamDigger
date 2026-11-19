using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapController : SingletonBase<MapController>
{
    private Dictionary<Vector3Int, int> _bitePerCell; // 좌표 당 내구도
    private Dictionary<Vector3Int, MemoryResource> _memoryPerCell; // 좌표 당 기억
    private Vector3Int[] _cellPositions; // 셀 좌표 배열

    public Tilemap CurrentTileMap;

    protected override void Awake()
    {
        base.Awake();
        CurrentTileMap = GetComponent<Tilemap>();
        
    }

    private void Start()
    {
        MapGenerator.Instance.GenerateMap();
        InitFields();
        InitCellBites();
    }

    public void InitFields() // 타일맵 넓이를 압축
    {
        _bitePerCell = new();
        CurrentTileMap.CompressBounds();
        _cellPositions = new Vector3Int[CurrentTileMap.cellBounds.size.x * CurrentTileMap.cellBounds.size.y];
    }

    public void InitCellBites() // 모든 파괴 가능 타일에 내구도를 부여
    {
        int temp = 0;
        for (int y = CurrentTileMap.cellBounds.yMax - 1; y >= CurrentTileMap.cellBounds.yMin; y--)
        {
            for (int x = CurrentTileMap.cellBounds.xMin; x < CurrentTileMap.cellBounds.xMax; x++)
            {
                if (!CurrentTileMap.HasTile(new Vector3Int(x, y, 0)))
                {
                    continue;
                }
                _cellPositions[temp] = new Vector3Int(x, y, 0);
                _bitePerCell.Add(_cellPositions[temp], CurrentTileMap.GetTile<BreakableTile>(_cellPositions[temp]).MaxBites);
                temp++;
            }
        }
    }

    public void TakeBite(Vector3Int position, int bite) // 타일 파괴 식
    {
        if (_bitePerCell.ContainsKey(position))
        {
            _bitePerCell[position] -= bite;
            Debug.Log($"남은 내구도 {_bitePerCell[position]}");
            if (_bitePerCell[position] <= 0)
            {
                CurrentTileMap.SetTile(position, null);
                _bitePerCell.Remove(position);
            }
        }
        else
        {
            Debug.Log($"캘 블록이 없습니다!");
        }
    }
}
