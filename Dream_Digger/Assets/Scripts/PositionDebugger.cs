using UnityEngine;
using UnityEngine.Tilemaps;

public class PositionDebugger : MonoBehaviour
{
    [SerializeField] private Tilemap _tilemap;
    
    private Vector3Int _debuggerPosition;

    private void Awake()
    {
        _debuggerPosition = _tilemap.WorldToCell(transform.position);
    }
    
    private void Start()
    {
        Debug.Log($"셀 좌표 디버거 위치 : {_debuggerPosition}");
    }
}
