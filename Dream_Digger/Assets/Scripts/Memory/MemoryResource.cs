using UnityEngine;

public class MemoryResource : MonoBehaviour
{
    public MemoryType Type;
    public int Score;
    public bool CanSell;

    private void Awake()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        { 
            PlayerController.Instance._memoryCount += Score;
            Debug.Log($"플레이어와 부딪힘");
            if (Type == MemoryType.Special)
            {
                Debug.Log($"특별한 기억을 획득했습니다!");
            }
            else
            {
                Debug.Log($"기억 획득!");
            }
            Destroy(gameObject);
        }
    }
}
