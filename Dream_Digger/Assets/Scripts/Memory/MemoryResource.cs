using UnityEngine;

public class MemoryResource : MonoBehaviour
{
    public MemoryType Type;
    public int Score;

    private void Awake()
    {
        
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
