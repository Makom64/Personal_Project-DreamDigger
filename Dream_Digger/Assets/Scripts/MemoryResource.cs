using UnityEngine;

public class MemoryResource : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    
    public MemoryData memoryData;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = memoryData.Sprite;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        { 
            Debug.Log($"플레이어와 부딪힘");
            PlayerController.Instance._memoryCount++;
            Destroy(gameObject);
        }
    }
}
