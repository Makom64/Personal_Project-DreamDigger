using UnityEditor;
using UnityEngine;

[CreateAssetMenu]
public class MemoryData : ScriptableObject
{
    public int Cost; // 가격
    public MemoryType Type; // 종류
    public Sprite Sprite; // 스프라이트
}
