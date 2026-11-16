using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapController : MonoBehaviour
{
    private Dictionary<Vector3Int, int> _bitePerCell; // 키, 값 : 좌표, 내구도
    private Vector3Int[] _cellPositions; // 셀 좌표 배열

    public static MapController instance; // 클래스 전체에 하나
    public Tilemap CurrentTileMap;

    private void Awake()
    {
        CurrentTileMap = GetComponent<Tilemap>();
        InitFields();
        if (instance == null)
        {
            instance = this;
        }
    }

    private void OnEnable()
    {
        InitCellBites();
    }

    private void InitFields()
    {
        _bitePerCell = new();
        CurrentTileMap.CompressBounds();
        _cellPositions = new Vector3Int[CurrentTileMap.cellBounds.size.x * CurrentTileMap.cellBounds.size.y];
    }

    private void InitCellBites() // 모든 파괴 가능 타일에 내구도를 부여
    {
        int temp = 0;
        for (int y = CurrentTileMap.cellBounds.yMax - 1; y >= CurrentTileMap.cellBounds.yMin; --y)
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
