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
        Debug.Log($"{transform.name} 위치 : {_debuggerPosition.x}, {_debuggerPosition.y}");
    }
}
