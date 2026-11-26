using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public int MaxHealth;
    [Header("최대 잠식 보호막")] public int MaxSinked;
    [Header("고도 0 이상인지")] public bool IsSafe => CurrentAltitude.Value >= 0;
    [Header("잠식 중")] public bool IsSinking => CurrentSinked.Value == 0;
    [Header("사망함")] public bool IsDead => CurrentHealth.Value < 0;
    
    public ObservableProperty<int> CurrentHealth;
    public ObservableProperty<int> CurrentAltitude;
    [Header("잠식까지 남은 수치")] public ObservableProperty<int> CurrentSinked;

    private void BindObServeEvents()
    {
        CurrentHealth.AddListener(TakeDamage);
    }
    
    public void TakeDamage(int damage)
    {
        CurrentHealth.Value -= damage;
    }
}
