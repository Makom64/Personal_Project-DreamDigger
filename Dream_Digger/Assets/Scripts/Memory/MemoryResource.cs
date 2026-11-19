using System;
using UnityEngine;

public class MemoryResource : MonoBehaviour
{
    public MemoryData MemoryData;
    public int Score;
    public bool CanSell;

    private SpriteRenderer _spriteRenderer;
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = MemoryData.Sprite;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        { 
            PlayerController.Instance._memoryCount += Score;
            Debug.Log($"플레이어와 접촉함");
            if (MemoryData.Type == MemoryType.Special)
            {
                Debug.Log($"{MemoryData.Name}을 획득했습니다!");
            }
            else
            {
                Debug.Log($"{MemoryData.Name} 획득!");
            }
            Destroy(gameObject);
        }
    }
}

public enum MemoryType
{
    Enjoy,
    Angry,
    Happy,
    Sad,
    Special,
}

[Serializable]
public struct MemoryData
{
    [Header("이미지")]
    public Sprite Sprite;
    
    [Header("기억 이름")]
    public string Name;
    
    [Header("기억 종류")]
    public MemoryType Type;
}
