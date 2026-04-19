using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _intance;
    private static readonly object _intanceLock = new();
    private static bool _quitting = false;

    public static T Instance
    {
        get
        {
            lock (_intanceLock)
            {
                if (_intance == null && !_quitting)
                {
                    _intance = GameObject.FindAnyObjectByType<T>();
                    if (_intance == null)
                    {
                        GameObject go = new GameObject(typeof(T).ToString());
                        _intance = go.AddComponent<T>();

                        DontDestroyOnLoad(_intance.gameObject);
                    }
                }

                return _intance;
            }
        }
    }

    protected virtual void Awake()
    {
        if (_intance == null) { _intance = gameObject.GetComponent<T>(); }
        
        else if (_intance.GetEntityId() != GetEntityId())
        {
            Destroy(gameObject);
            throw new System.Exception(string.Format("Instance of {0} already exists, removing {1}", GetType().FullName, ToString()));
        }

        Init();
    }

    protected virtual void OnApplicationQuit() { _quitting = true; }

    protected virtual void Init() { }
}