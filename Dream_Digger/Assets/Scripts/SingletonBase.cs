using UnityEngine;

public abstract class SingletonBase<T> : MonoBehaviour where T : SingletonBase<T>
{
    [SerializeField] private bool _destroyOnLoad; // 인스펙터 표시
    
    private static T _instance;

    protected virtual void Awake()
    {
        SetSingleton();
    }

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<T>();
            }
                return _instance;
        }
    }

    private void SetSingleton()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this as T;
            DestroyOnLoad();
        }
    }

    private void DestroyOnLoad()
    {
        if (_destroyOnLoad) return;
        DontDestroyOnLoad(gameObject);
    }
}
