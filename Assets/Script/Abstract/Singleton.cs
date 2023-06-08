using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (IsInstanceSet) return _instance;
            InitInstance();
            return !IsInstanceSet ? null : _instance;
        }
    }

    public static bool IsInstanceSet
    {
        get { return _instance != null; }
    }

    private static void InitInstance()
    {
        if (IsInstanceSet) return;
        var instance = (T) FindObjectOfType(typeof(T));
        if (instance != null) SetInstance(instance);
    }

    private static void SetInstance(T instance)
    {
        _instance = instance;
        if (!_instance.IsDestroyedOnLoad) DontDestroyOnLoad(_instance.gameObject);
        _instance.Initialize();
    }

    public virtual bool IsDestroyedOnLoad
    {
        get { return true; }
    }

    private void Awake()
    {
        if (_instance == null) SetInstance(this as T);
        else if(_instance != this)
        {
            Debug.LogError("Multiple Singleton Intance : " + this);
            Destroy(this);
        }
    }

    private void OnDestroy()
    {
        if (_instance == this) _instance = null;
    }

    protected virtual void Initialize()
    {
    }
}