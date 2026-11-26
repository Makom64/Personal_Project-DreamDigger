using System;
using UnityEngine;

[Serializable]
public class ObservableProperty<T>
{
    // 옵저버블 프로퍼티 해야되는거
    // 값이 있음
    // 값이 바뀌면 호출되는 메서드 (신호를 쏘는 애)
    // 값이 바뀌면 동작하는 액션???
    
    private Action<T> _onValueChanged;

    [SerializeField] private T _value;

    public T Value
    {
        get => _value; // 접근자, _value를 반환함
        set
        {
            _value = value; // 암시적 매개변수인 value를 _value에 넣는 접근자 메서드
            Notify();
        }
    }

    public ObservableProperty(T initialValue) // 클래스 생성자
    {
        _value = initialValue;
    }

    public void Notify() // 알림 신호
    {
        _onValueChanged?.Invoke(_value);
    }

    public void AddListener(Action<T> onValueChanged)
    {
        _onValueChanged = onValueChanged;
    }

    public void RemoveListener(Action<T> onValueChanged)
    {
        _onValueChanged -= onValueChanged;
    }

    public void RemoveAllListeners()
    {
        _onValueChanged = null;
    }
}
