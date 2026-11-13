using UnityEngine;
using UnityEngine.Tilemaps;

public class MapController : MonoBehaviour
{
    private Tilemap _tilemap;
    
    public static MapController instance; // 클래스 전체에 하나

    private void Awake()
    {
        _tilemap = GetComponent<Tilemap>();
        if (instance == null)
        {
            instance = this;
        }
    }

    public Vector3Int ReadCellPosition(Vector3 worldPosition)
    {
        Vector3Int _requestedPosition = _tilemap.WorldToCell(worldPosition);
        return _requestedPosition;
    }
}
