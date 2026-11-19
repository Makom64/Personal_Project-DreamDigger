using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapController : SingletonBase<MapController>
{
    private Dictionary<Vector3Int, int> _bitePerCell; // 좌표 당 내구도
    private Dictionary<Vector3Int, MemoryResource> _memoryPerCell; // 좌표 당 기억
    public Dictionary<int, List<int>> AreaLengthDict; // 구역 번호(키) : 좌표 Y값 배열(값)

    public Tilemap CurrentTileMap;

    protected override void Awake()
    {
        base.Awake();
        CurrentTileMap = GetComponent<Tilemap>();
        _bitePerCell = new();
        AreaLengthDict = new();
    }

    private void OnEnable()
    {
        MapGenerator.Instance.GenerateMap(); // 맵을 생성한다.
        CurrentTileMap.CompressBounds(); // 타일맵의 넓이를 생성한 범위만큼 압축한다. 압축하지 않으면 최소/최대치를 구할 수 없다.
    }

    private void Start()
    {
        
        //InitFields();
        // MapGenerator.Instance.CellPositionShuffle();
        //InitCellBites();
        
    }

    // position : 플레이어 상호작용 목표 지점 / bite : 플레이어 파괴력
    public void TakeBite(Vector3Int position, int bite) // 타일 파괴 식
    {
        if (CurrentTileMap.GetTile<BreakableTile>(position) == null) // 파괴 가능 타일이 아니거나, 타일이 없는 경우
        {
            Debug.Log($"먹을 수 없는 꿈이에요!");
        }
        else
        {
            if (!_bitePerCell.ContainsKey(position)) // 상호작용 한적 없는 셀이면
            {
                // 해당 위치(키), 해당 타일 내구도(값) 을 저장한다.
                _bitePerCell.Add(position, CurrentTileMap.GetTile<BreakableTile>(position).MaxBites);
                // 그리고 파괴력만큼 깐다.
                _bitePerCell[position] -= bite;
            }
            else if (_bitePerCell.ContainsKey(position)) // 상호작용 한적 있는 셀이면
            {
                _bitePerCell[position] -= bite;
                
                // 해당 좌표 내구도가 0이하가 되면
                if (_bitePerCell[position] <= 0)
                {
                    CurrentTileMap.SetTile(position, null);
                }
            }
        }
    }
}
